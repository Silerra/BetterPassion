using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace BetterPassionIcons
{
    [HarmonyPatch(typeof(PawnColumnWorker_WorkPriority), nameof(PawnColumnWorker_WorkPriority.DoCell))]
    public static class Patch_WorkPriorityIconSize
    {
        static void Postfix(Rect rect, Pawn pawn, PawnColumnWorker_WorkPriority __instance)
        {
            if (pawn?.skills == null)
                return;

            // Hole die WorkTypeDef aus der PawnColumnDef
            WorkTypeDef workTypeDef = __instance.def.workType;
            if (workTypeDef?.relevantSkill == null)
                return;

            // Hole die Passion für die relevante Skill-Def
            Passion passion = pawn.skills.GetSkill(workTypeDef.relevantSkill).passion;
            if (passion == Passion.None)
                return;

            // Lade die benutzerdefinierten Texturen aus den XML-Defs
            PassionDef passionDef = (passion == Passion.Major) 
                ? DefDatabase<PassionDef>.GetNamed("MajorPassion") 
                : DefDatabase<PassionDef>.GetNamed("MinorPassion");

            Texture2D icon = ContentFinder<Texture2D>.Get(passionDef.iconPath);

            // Zeichne das Symbol (20x20 Pixel)
            Rect iconRect = new Rect(rect.x + 2f, rect.y + 2f, 20f, 20f);
            GUI.DrawTexture(iconRect, icon);
        }
    }
}