using System;
using System.Collections.Generic;
using HarmonyLib;
using Verse;

namespace BetterPassionIcons
{
    [StaticConstructorOnStartup]
    public class LoadPatchClasses : Mod
    {
        static LoadPatchClasses()
        {
            Load();
        }

        public static void Load()
        {
            // Load all patch classes
            var patchClasses = new List<Type>
            {
                // typeof(Patch_WorkPriorityIconSize),
                typeof(WidgetsWorkPatch)
            };
            Log.Message($"Loading {patchClasses.Count} patch classes");

            foreach (var patchClass in patchClasses)
            {
                Log.Message($"Loading patch class {patchClass.FullName}");
                var harmony = new Harmony("de.silerra.betterpassionicons");
                harmony.PatchAll(patchClass.Assembly);
            }
        }
    }
}