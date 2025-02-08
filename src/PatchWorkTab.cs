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

            string defName = (passion == Passion.Major) ? "PassionMajor" : "PassionMinor";
            CustomPassionDef passionDef = DefDatabase<CustomPassionDef>.GetNamed(defName);

            if (passionDef?.Icon == null)
                return;

            // A) Lösche Vanilla-Icon
            Rect vanillaIconRect = new Rect(rect.x + 2f, rect.y + 2f, 20f, 20f);
            GUI.DrawTexture(vanillaIconRect, BaseContent.ClearTex);

            // B) Zeichne Custom-Icon unter dem Häkchen
            Rect customIconRect = new Rect(rect.x + 5f, rect.y + 6f, 18f, 18f);
            GUI.DrawTexture(customIconRect, passionDef.Icon);
        }
    }
}