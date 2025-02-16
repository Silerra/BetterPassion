using System;
using System.Collections.Generic;
using HarmonyLib;
using Verse;
using HighQualityTextures;

namespace BetterPassionIcons
{
    [StaticConstructorOnStartup]
    public class LoadPatchClasses : Mod
    {
        static LoadPatchClasses()
        {
            Load();
        }

        // public LoadPatchClasses(ModContentPack content) : base(content)
        // {
        //     Load();
        // }

        public static void Load()
        {
            // Load all patch classes
            var patchClasses = new List<Type>
            {
                // typeof(Patch_WorkPriorityIconSize),
                typeof(PatchModContentLoaderTexture2D),
                typeof(PatchModAssetBundlesHandler),
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