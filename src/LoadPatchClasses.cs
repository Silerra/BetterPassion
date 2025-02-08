using System;
using System.Collections.Generic;
using HarmonyLib;
using Verse;

namespace BetterPassionIcons
{
    public class LoadPatchClasses
    {
        public static void Load()
        {
            // Load all patch classes
            var patchClasses = new List<Type>
            {
                typeof(WidgetsWorkPatch),
                typeof(Patch_WorkPriorityIconSize),
                typeof(CustomPassionDef)
            };

            foreach (var patchClass in patchClasses)
            {
                Log.Message($"Loading patch class {patchClass.FullName}");
                var harmony = new Harmony("de.silerra.betterpassionicons");
                harmony.PatchAll(patchClass.Assembly);
            }
        }
    }
}