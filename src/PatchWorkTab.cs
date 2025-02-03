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

            // Hole die erste relevante Skill-Def aus der WorkTypeDef
            SkillDef skillDef = __instance.def.workType.relevantSkills?.FirstOrDefault();
            if (skillDef == null)
                return;

            Passion passion = pawn.skills.GetSkill(skillDef).passion;
            if (passion == Passion.None)
                return;

            // Lade die benutzerdefinierte PassionDef
            PassionDef passionDef = (passion == Passion.Major)
                ? DefDatabase<PassionDef>.GetNamed("MajorPassion")
                : DefDatabase<PassionDef>.GetNamed("MinorPassion");

            // Zeichne das Symbol
            Rect iconRect = new Rect(rect.x + 2f, rect.y + 2f, 20f, 20f);
            GUI.DrawTexture(iconRect, passionDef.Icon);
        }
    }
}