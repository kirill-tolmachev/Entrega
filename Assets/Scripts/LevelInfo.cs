using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(menuName = "Create LevelInfo")]
    internal class LevelInfo : ScriptableObject
    {
        public Color CameraBackgroundColor;
        public Color BackgroundColor;
        [HideInInspector] public Color FillColor;
        public Color TargetColor;
        public Color TextColor;
        public Color PlayerColor;

        public Color GetColor(PaletteColor paletteColor)
        {
            return paletteColor switch
            {
                PaletteColor.Background => BackgroundColor,
                PaletteColor.CameraBackground => CameraBackgroundColor,
                PaletteColor.Fill => FillColor,
                PaletteColor.Target => TargetColor,
                PaletteColor.Text => TextColor,
                PaletteColor.Player => PlayerColor,
                _ => throw new ArgumentOutOfRangeException(nameof(paletteColor), paletteColor, null)
            };
        }
    }
}
