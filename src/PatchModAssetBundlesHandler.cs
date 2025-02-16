using System;
using System.Reflection;
using HarmonyLib;
using Verse;

namespace BetterPassionIcons
{
    [HarmonyPatch(typeof(ModAssetBundlesHandler))]
    public static class PatchModAssetBundlesHandler
    {
        private static ModLog Log = Mod.Log;

        // Patch the constructor of ModAssetBundlesHandler
        [HarmonyPatch(MethodType.Constructor)]
        [HarmonyPatch(new Type[] { typeof(ModContentPack) })]
        [HarmonyPrefix]
        public static void Prefix(ModAssetBundlesHandler __instance)
        {
            Log.Message("ModAssetBundlesHandler constructor patched");

            ReplaceField(typeof(ModAssetBundlesHandler), "TextureExtensions", new string[5] { ".png", ".psd", ".jpg", ".jpeg", ".dds" });
        }

        private static void ReplaceField(Type type, string fieldName, string[] strings)
        {
            Log.Message($"Attempting to replace field {fieldName} in type {type.FullName}");
            FieldInfo field = type.GetField(fieldName, BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (field == null)
            {
                Log.Error($"Field {fieldName} not found in type {type.FullName}");
                LogAllFields(type);
                return;
            }

            // Remove the readonly restriction
            typeof(FieldInfo).GetField("m_isReadOnly", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(field, false);

            // Set the new value
            field.SetValue(null, strings);
            Log.Message("Replaced " + fieldName);

            // Log the new value to verify the change
            string[] newValue = (string[])field.GetValue(null);
            Log.Message($"New value of {fieldName}: {string.Join(", ", newValue)}");
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