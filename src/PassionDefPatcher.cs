using System;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace BetterPassionIcons
{
    [HarmonyPatch(typeof(WidgetsWork))]
    public static class WidgetsWorkPatch
    {
        private static bool fieldsReplaced = false;

        [HarmonyPatch("DrawWorkBoxFor")]
        public static void Prefix()
        {
            if (!fieldsReplaced)
            {
                Log.Message("WidgetsWork DrawWorkBoxFor patched");

                // Ersetze die Vanilla-Texturen mit deinen eigenen
                ReplaceField(typeof(WidgetsWork), "PassionWorkboxMinorIcon", ContentFinder<Texture2D>.Get("UI/Icons/PassionMinorCustom"));
                ReplaceField(typeof(WidgetsWork), "PassionWorkboxMajorIcon", ContentFinder<Texture2D>.Get("UI/Icons/PassionMajorCustom"));

                fieldsReplaced = true;
            }
        }

        private static void ReplaceField(Type type, string fieldName, object value)
        {
            Log.Message($"Attempting to replace field {fieldName} in type {type.FullName}");
            FieldInfo field = type.GetField(fieldName, BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (field == null)
            {
                Log.Error($"Field {fieldName} not found in type {type.FullName}");
                LogAllFields(type);
                return;
            }

            // Entferne die readonly-Einschränkung
            typeof(FieldInfo).GetField("m_isReadOnly", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(field, false);

            // Setze den neuen Wert
            field.SetValue(null, value);
            Log.Message("Replaced " + fieldName);
        }

        private static void LogAllFields(Type type)
        {
            Log.Message($"Listing all fields in type {type.FullName}:");
            FieldInfo[] fields = type.GetFields(BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (var field in fields)
            {
                Log.Message($"Field: {field.Name}, Type: {field.FieldType}");
            }
        }
    }
}