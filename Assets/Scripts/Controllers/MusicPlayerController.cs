using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    internal class MusicPlayerController : MonoBehaviour
    {
        [SerializeField] private AudioClip _startupClip;

        [SerializeField] private AudioClip _loopClip;

        [SerializeField] private AudioSource _audioSource;

        private void OnEnable()
        {
            _audioSource.clip = _startupClip;
            _audioSource.Play();
        }

        private void OnDisable()
        {
            _audioSource.Stop();
        }

        void Update()
        {
            if (_audioSource.isPlaying == false)
            {
                _audioSource.clip = _loopClip;
                _audioSource.loop = true;
                _audioSource.Play();
            }
        }

    }
}
