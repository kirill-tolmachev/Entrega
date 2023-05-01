using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Util;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    internal abstract class Pickable : MonoBehaviour
    {
        private bool _isActive = true;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private TMP_Text _text;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_isActive)
                return;

            if (_layerMask.HasLayer(other.gameObject.layer))
            {
                _isActive = false;
                OnPicked();

                transform.DOShakeScale(0.2f);
                _text.DOFade(0f, 0.2f);
            }
        }

        protected abstract void OnPicked();
    }
}
