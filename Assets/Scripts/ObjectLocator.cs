using UnityEngine;

namespace Scripts
{
    public class ObjectLocator : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _backgroundRenderer;

        [SerializeField] private SpriteRenderer _playerRenderer;

        [SerializeField] private Transform _playerTransform;

        [SerializeField] private Transform _packagesContainer;
        
        [SerializeField] private Transform _fxsParent;

        public Rect BackgroundRect => MakeRect(_backgroundRenderer);

        public Rect PlayerRect => MakeRect(_playerRenderer);
    
        public Transform PlayerTransform => _playerTransform;
        public Transform PackagesContainer => _packagesContainer;

        public Transform FXsParent => _fxsParent;

        private Rect MakeRect(Renderer sr) => new(sr.bounds.min, sr.bounds.size);
    }
}