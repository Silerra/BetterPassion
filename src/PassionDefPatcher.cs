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
        [HarmonyPatch(typeof(SkillUI), nameof(SkillUI.Reset))]
        [HarmonyPostfix]
        public static void Postfix()
        {
            // 2. Ersetze die Vanilla-Texturen mit deinen eigenen
            SkillUI.PassionMinorIcon = ContentFinder<Texture2D>.Get("UI/Icons/Passion/MinorPassion");
            SkillUI.PassionMajorIcon = ContentFinder<Texture2D>.Get("UI/Icons/Passion/MajorPassion");
        }
    }

/*     [StaticConstructorOnStartup]
    public static class ModInit
    {
        static ModInit()
        {
            new Harmony("de.Silerra.BetterPassionIcons").PatchAll();
        }
    } */
}