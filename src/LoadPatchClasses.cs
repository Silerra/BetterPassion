using System;
using System.Collections.Generic;
using HarmonyLib;
using Verse;

namespace BetterPassionIcons
{
    [StaticConstructorOnStartup]
    public class LoadPatchClasses : Mod
    {
        public LoadPatchClasses(ModContentPack content) : base(content)
        {
            Load();
        }

        public static void Load()
        {
            // Load all patch classes
            var patchClasses = new List<Type>
            {
                typeof(WidgetsWorkPatch)
            };
            var harmony = new Harmony("de.silerra.betterpassionicons");
            Log.Message($"Loading {patchClasses.Count} patch classes");

            foreach (var patchClass in patchClasses)
            {
                Log.Message($"Loading patch class {patchClass.FullName}");
                harmony.CreateClassProcessor(patchClass).Patch();
            }
        }
    }
}