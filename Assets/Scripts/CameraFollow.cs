using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ldjam_hellevator
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;
        private float yOffset = 3.9f;
        
        [Header("Camera Shake")]
        private Vector3 _originalPos;
        private float _shakeIntensity = 0.15f;
        private float _shakeDuration = 0.3f;
        private float _smoothness = 3f;
        
        [SerializeField] private GmChannel _gameManagerChannel;

        private void Start()
        {
            _originalPos = transform.localPosition;
            _gameManagerChannel.OnCameraShake += DoShake;
        }   

        public void DoShake()
        {
            StartCoroutine(ShakeCoroutine());
        }

        private IEnumerator ShakeCoroutine()
        {
            float elapsed = 0f;

            while (elapsed < _shakeDuration)
            {
                Vector3 offset = Random.insideUnitSphere * _shakeIntensity;
                transform.localPosition = _originalPos + offset;
                elapsed += Time.deltaTime;
                yield return null;
            }

            while (transform.localPosition != _originalPos)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, _originalPos, _smoothness * Time.deltaTime);
                yield return null;
            }
            
            transform.localPosition = _originalPos;
        }
        
        void Update()
        {
            if (target != null)
            {
                transform.position = new Vector3(transform.position.x, target.transform.position.y - yOffset,
                    transform.position.z);
            }
        }
    }
}