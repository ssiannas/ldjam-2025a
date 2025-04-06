using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace ldjam_hellevator
{
    public class GlobalFXController : MonoBehaviour
    {
        private Volume _volume;
        private Bloom _bloom;
        
        [SerializeField] GmChannel gmChannel;
        
        [Header("Bloom Properties")]
        private float originalIntensity;
        private float bloomIntensity;


        private void Awake()
        {
            _volume = GetComponent<Volume>();
            if (_volume == null)
            {
                throw new Exception("No Volume component found on GlobalFXController");
            }
            _volume.profile.TryGet<Bloom>(out _bloom);
            gmChannel.OnBloomPulsate += BloomPulsate;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            

        }
        
        private IEnumerator ApplyIntensityEffect(float targetIntensity, float duration, bool loop = false)
        {
            var elapsedTime = 0f;
            var halfway = duration / 2f;

            while (elapsedTime < halfway)
            {
                var t = elapsedTime / halfway; 
                var intensity = Mathf.Lerp(originalIntensity, targetIntensity, t);  
                _bloom.intensity = new MinFloatParameter(intensity, 0); 
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            while (elapsedTime < duration)
            {
                var t = (elapsedTime - halfway) / halfway; 
                var intensity = Mathf.Lerp(targetIntensity, originalIntensity, t);  
                _bloom.intensity = new MinFloatParameter(intensity, 0);  
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _bloom.intensity = new MinFloatParameter(originalIntensity, 0);  
            if (loop)
            {
               StartCoroutine(ApplyIntensityEffect(targetIntensity, duration, true));
            }
        }

        void BloomPulsate(float intensity, float duration, bool isPeriodic = false)
        {
            StartCoroutine(ApplyIntensityEffect(intensity, duration, isPeriodic));
        }
    }
}
