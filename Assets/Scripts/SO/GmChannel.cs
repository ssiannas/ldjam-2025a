using UnityEngine;
using UnityEngine.Events;

namespace ldjam_hellevator
{
    [CreateAssetMenu(fileName = "GMChannel", menuName = "Scriptable Objects/GMChannel")]
    public class GmChannel : ScriptableObject
    {
        public UnityAction OnGameStart;
        public UnityAction OnGameEnd;
        public UnityAction OnGamePause;
        public UnityAction OnGameResume;
        public UnityAction OnGameQuit;
        public UnityAction<float, float, bool> OnBloomPulsate;
        public UnityAction OnCameraShake;

       
        public void GameStart()
        {
            if (OnGameStart == null)
            {
                throw new System.Exception("No Game Manager Assigned");
            }
            OnGameStart?.Invoke();
        }
        
        public void GameEnd()
        {
            if (OnGameEnd == null)
            {
                throw new System.Exception("No Game Manager Assigned");
            }
            OnGameEnd?.Invoke();
        }
        
        public void GamePause()
        {
            if (OnGamePause == null)
            {
                throw new System.Exception("No Game Manager Assigned");
            }
            OnGamePause?.Invoke();
        }
        
        public void GameResume()
        {
            if (OnGameResume == null)
            {
                throw new System.Exception("No Game Manager Assigned");
            }
            OnGameResume?.Invoke();
        }
        
        public void GameQuit()
        {
            if (OnGameQuit == null)
            {
                throw new System.Exception("No Game Manager Assigned");
            }
            OnGameQuit?.Invoke();
        }

        public void CameraShake()
        {
            OnCameraShake?.Invoke();
        }
        
        public void BloomPulsate(float intensity, float duration, bool loop = false)
        {
            OnBloomPulsate?.Invoke(intensity, duration, loop);
        }
    }
}
