using UnityEngine;

namespace Assets.Scripts
{
    internal class ColorizableCamera : Colorizable
    {
        [SerializeField] private Camera _camera;

        public override void SetColor(Color color)
        {
            _camera.backgroundColor = color;
        }
    }
}