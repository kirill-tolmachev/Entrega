using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.MessageImpl;
using DG.Tweening;
using Scripts.Infrastructure.Messages;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Assets.Scripts.Controllers
{
    internal class ResetMenuController : MonoBehaviour
    {
        [SerializeField] private float _fillSpeed;
        [SerializeField] private float _decaySpeed;

        [Inject] private IMessageBus _messageBus;

        private bool _isActive;

        private float _value = 0f;

        private void OnEnable()
        {
            _messageBus.Subscribe<ResetMessage>(OnReset);
        }

        private void OnDisable()
        {
            _messageBus.Unsubscribe<ResetMessage>(OnReset);
        }

        private void OnReset(ResetMessage message)
        {
            _isActive = true;
        }

        private void Update()
        {
            if (!_isActive)
                return;

            float delta = Input.GetKey(KeyCode.Space) ? _fillSpeed / 100f : -_decaySpeed / 100f;
            _value = Mathf.Clamp01(_value + delta * Time.deltaTime);

            var scale = _fillSpriteRenderer.transform.localScale;
            _fillSpriteRenderer.transform.localScale = new Vector3(_value, scale.y, scale.z);

            if (_value >= 1f)
            {
                _isActive = false;
                _fillSpriteRenderer.transform.DOScaleX(0f, 0.2f).SetEase(Ease.InOutCubic);
                _text.localScale = Vector3.zero;
            }
        }

    }
}
