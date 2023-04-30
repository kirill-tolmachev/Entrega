using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class CanNotRotate : MonoBehaviour
    {
        [SerializeField] private Vector3 _rotation;

        private void LateUpdate()
        {
            transform.rotation = Quaternion.Euler(_rotation);
        }
    }
}
