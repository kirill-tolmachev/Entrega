using UnityEngine;

namespace Assets.Scripts
{
    internal class Bullet : MonoBehaviour
    {
        [SerializeField] private LayerMask _affectedLayers;

        public LayerMask AffectedLayers => _affectedLayers;
    }
}