using System;
using UnityEngine;
using UnityEngine.Events;

namespace ldjam_hellevator
{
    [CreateAssetMenu(fileName = "ScoreManagerChannel", menuName = "Scriptable Objects/ScoreManagerChannel")]
    public class ScoreManagerChannel : ScriptableObject
    {
        public UnityAction<int> OnAddPoints;
        public UnityAction<int> OnSetScore;
        public UnityAction OnResetScore;
        public Func<int> OnGetScore;    
        public UnityAction OnStopCounting;
        public Func<int> OnGetDifficultyLevel;
        public Func<float> OnGetDepth;

        public void AddPoints(int points)
        {
            OnAddPoints?.Invoke(points);
        }
        
        public void SetScore(int score)
        {
            OnSetScore?.Invoke(score);
        }
        
        public void ResetScore()
        {
            OnResetScore?.Invoke();
        }
        
        public int GetScore()
        {
            return OnGetScore?.Invoke() ?? -1;
        }
        
        
        public float GetDepth()
        {
            return OnGetDepth?.Invoke() ?? 0;
        }

        public void StopCounting()
        {
            OnStopCounting?.Invoke();
        }
        
        public int GetDifficultyLevel()
        {
            return OnGetDifficultyLevel?.Invoke() ?? 0;
        }
    }
}
