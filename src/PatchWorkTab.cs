using HarmonyLib;
using RimWorld;
using System.Linq;
using UnityEngine;
using Verse;

namespace BetterPassionIcons
{
    [HarmonyPatch(typeof(PawnColumnWorker_WorkPriority), nameof(PawnColumnWorker_WorkPriority.DoCell))]
    public static class Patch_WorkPriorityIconSize
    {
        // Nutze einen Postfix-Patch, um das Vanilla-Icon zu überschreiben
        static void Postfix(Rect rect, Pawn pawn, PawnColumnWorker_WorkPriority __instance)
        {
            if (pawn?.skills == null || __instance.def.workType == null)
                return;

            SkillDef skillDef = __instance.def.workType.relevantSkills?.FirstOrDefault();
            if (skillDef == null)
                return;

            Passion passion = pawn.skills.GetSkill(skillDef).passion;
            if (passion == Passion.None)
                return;

            // Lade das benutzerdefinierte Icon
            string defName = (passion == Passion.Major) ? "PassionMajor" : "PassionMinor";
            CustomPassionDef passionDef = DefDatabase<CustomPassionDef>.GetNamed(defName);

            if (passionDef?.Icon == null)
                return;

            // 1. Überschreibe das Vanilla-Icon mit einem transparenten Bereich
            Rect vanillaIconRect = new Rect(rect.x + 2f, rect.y + 2f, 20f, 20f); // Vanilla-Icon-Größe
            GUI.DrawTexture(vanillaIconRect, BaseContent.ClearTex); // Lösche das Vanilla-Icon

            // 2. Zeichne das benutzerdefinierte Icon
            Rect customIconRect = new Rect(rect.x + 2f, rect.y + 2f, 24f, 24f); // Deine größere Version
            GUI.DrawTexture(customIconRect, passionDef.Icon);
        }
    }

    [StaticConstructorOnStartup]
    public static class ModInit
    {
        static ModInit()
        {
            var harmony = new Harmony("de.Silerra.BetterPassionIcons");
            harmony.PatchAll();
        }
    }
}