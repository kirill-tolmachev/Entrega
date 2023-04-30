using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal abstract class Colorizable : MonoBehaviour
    {
        [SerializeField] private PaletteColor _paletteColor;

        public void SetColor(LevelInfo levelInfo)
        {
            var color = levelInfo.GetColor(_paletteColor);
            SetColor(color);
        }

        public abstract void SetColor(Color color);
    }
}
