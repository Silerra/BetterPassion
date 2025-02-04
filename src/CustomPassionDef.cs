// PassionDef.cs → Umbenennen in CustomPassionDef.cs
using RimWorld;
using UnityEngine;
using Verse;

namespace BetterPassionIcons
{
    public class CustomPassionDef : Def // ← Geänderter Klassenname
    {
        public string iconPath; // Kein "?" (nicht-nullable für XML)

        [Unsaved]
        private Texture2D icon;

        public Texture2D Icon
        {
            get
            {
                if (icon == null && !iconPath.NullOrEmpty())
                    icon = ContentFinder<Texture2D>.Get(iconPath);
                return icon;
            }
        }
    }
}