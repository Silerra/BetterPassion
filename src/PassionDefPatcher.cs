using System;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace BetterPassionIcons
{
    [HarmonyPatch]
    public static class SkillUIPatch
    {
        // 1. Patch den Klassenkonstruktor von SkillUI
        [HarmonyPatch(typeof(SkillUI), nameof(SkillUI.DrawSkillsOf))]
        [HarmonyPostfix]
        public static void Postfix()
        {
            // 2. Ersetze die Vanilla-Texturen mit deinen eigenen
            SkillUI.PassionMinorIcon = ContentFinder<Texture2D>.Get("UI/Icons/Passion/MinorPassion");
            SkillUI.PassionMajorIcon = ContentFinder<Texture2D>.Get("UI/Icons/Passion/MajorPassion");
            // Log.Message("Draw new icons");
        }
    }
}