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
        // Ändere von Postfix zu Prefix und unterdrücke die Originalmethode
        static bool Prefix(Rect rect, Pawn pawn, PawnColumnWorker_WorkPriority __instance)
        {
            bool result = true; // Standardwert
            if (pawn?.skills == null || __instance.def.workType == null)
                return result;

            SkillDef skillDef = __instance.def.workType.relevantSkills?.FirstOrDefault();
            if (skillDef == null)
                return result;

            Passion passion = pawn.skills.GetSkill(skillDef).passion;
            if (passion == Passion.None)
                return result;

            string defName = (passion == Passion.Major) ? "PassionMajor" : "PassionMinor";
            CustomPassionDef passionDef = DefDatabase<CustomPassionDef>.GetNamed(defName);

            if (passionDef?.Icon != null)
            {
                Rect iconRect = new Rect(rect.x + 2f, rect.y + 2f, 24f, 24f);
                GUI.DrawTexture(iconRect, passionDef.Icon);
                result = false; // Unterdrücke Vanilla-Code
            }

            return result; // Garantiert Rückgabe in allen Pfaden
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