using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace BetterPassionIcons
{
    [HarmonyPatch(typeof(PawnColumnWorker_WorkPriority), "DoCell")]
    public static class Patch_WorkPriorityIconSize
    {
        static void Postfix(Rect rect, Pawn pawn, PawnTable table)
        {
            // Vergrößere das Symbol auf 20x20 Pixel
            Rect passionRect = new Rect(rect.x + 4f, rect.y + 4f, 20f, 20f);
            Passion passion = pawn.skills?.GetSkill(table.def.workType.relevantSkill)?.passion ?? Passion.None;
            
            if (passion == Passion.Major)
                GUI.DrawTexture(passionRect, MyTextures.MajorPassion); // Deine benutzerdefinierte Textur
            else if (passion == Passion.Minor)
                GUI.DrawTexture(passionRect, MyTextures.MinorPassion);
        }
    }
}