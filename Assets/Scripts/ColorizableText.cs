using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    internal class ColorizableText : Colorizable
    {
        [SerializeField] private TMP_Text _text;


        public override void SetColor(Color color)
        {
            _text.color = color;
        }
    }
}