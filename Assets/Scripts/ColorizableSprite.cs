using UnityEngine;

namespace Assets.Scripts
{
    internal class ColorizableSprite : Colorizable
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public override void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }
    }
}