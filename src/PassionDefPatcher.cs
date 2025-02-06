// PassionDefPatcher.cs
using HarmonyLib;
using RimWorld;
using Verse;

namespace BetterPassionIcons
{
    [HarmonyPatch(typeof(PassionDef), nameof(PassionDef.IconPath), MethodType.Getter)]
    public static class PassionDefIconPatch
    {
        static void Postfix(PassionDef __instance, ref string __result)
        {
            // Überschreibe IconPath basierend auf defName
            switch (__instance.defName)
            {
                case "PassionMajor":
                    __result = "UI/Icons/Passion/MajorPassion";
                    break;
                case "PassionMinor":
                    __result = "UI/Icons/Passion/MinorPassion";
                    break;
            }
        }
    }
}