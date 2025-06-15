using System.Linq;
using RimWorld;
using Verse;

namespace BetterPassionIcons
{
    public class ModDependencyChecker : GameComponent
    {
        private bool checkedDependencies = false;

        public ModDependencyChecker(Game game) { }

        public override void FinalizeInit()
        {
            base.FinalizeInit();

            if (checkedDependencies)
                return;

            checkedDependencies = true;

            // Liste der akzeptierten Mods
            string[] requiredMods = new string[]
            {
                "Graphics Settings+",
                "Performance Fish",
                "[Silerra] High quality textures"
            };

            // Überprüfen, ob mindestens eine der Mods geladen ist
            bool isDependencySatisfied = ModsConfig.ActiveModsInLoadOrder.Any(mod => requiredMods.Contains(mod.Name));

            if (!isDependencySatisfied)
            {
                Messages.Message($"Damit BetterPassionIcons funktioniert, benötigt dieser mindestens eine der folgenden Mods: {requiredMods[0]}, {requiredMods[1]} oder {requiredMods[2]}.", MessageTypeDefOf.RejectInput, historical: false);
                Log.Warning($"[BetterPassionIcons] Keine kompatible Mod gefunden! Bitte installiere eine der folgenden Mods: {requiredMods[0]}, {requiredMods[1]} oder {requiredMods[2]}.");
            }
            else
            {
                Log.Message("[BetterPassionIcons] Kompatible Mod gefunden. Mod funktioniert wie erwartet.");
            }
        }
    }
}