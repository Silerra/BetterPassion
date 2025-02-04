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

            // Verwende die VANILLA DefNames "PassionMajor" und "PassionMinor"
            string defName = (passion == Passion.Major) ? "PassionMajor" : "PassionMinor";
            
            // Lade die CUSTOM PassionDef (jetzt CustomPassionDef)
            CustomPassionDef passionDef = DefDatabase<CustomPassionDef>.GetNamed(defName);

            if (passionDef?.Icon == null)
                return;

            // Zeichne das Icon mit angepasster Größe
            Rect iconRect = new Rect(rect.x + 2f, rect.y + 2f, 24f, 24f); // Größeres Icon (24x24)
            GUI.DrawTexture(iconRect, passionDef.Icon);
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