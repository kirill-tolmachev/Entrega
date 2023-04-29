using Assets.Scripts;
using UnityEngine;
using Zenject;

namespace Scripts.Controllers
{
    internal class VerticalMovementController : MonoBehaviour
    {
        [Inject] private ObjectRegistry _objectRegistry;
        [Inject] private GlobalSettings _settings;

        void Update()
        {
            float dy = _settings.ScrollSpeed * Time.deltaTime;

            foreach (var item in _objectRegistry.Objects)
            {
                var pos = item.localPosition;
                item.localPosition = new Vector3(pos.x, pos.y + dy, pos.z);
            }
        }
    }
}
