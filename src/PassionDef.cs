// PassionDef.cs
using RimWorld;
using UnityEngine;
using Verse;

namespace BetterPassionIcons
{
    public class CustomPassionDef : Def // ← Geänderter Klassenname
    {
        public Color color = Color.white;
        public string? iconPath;

        [Unsaved]
        private Texture2D? icon;

        public Texture2D Icon
        {
            get
            {
                if (icon == null && !iconPath.NullOrEmpty())
                    icon = ContentFinder<Texture2D>.Get(iconPath);
                return icon!;
            }
        }
    }
}