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

        public void AddPoints(int points)
        {
            OnAddPoints?.Invoke(points);
            Debug.Log("sianna gamiesai");
        }
        
        public void SetScore(int score)
        {
            OnSetScore?.Invoke(score);
        }
        
        public void ResetScore()
        {
            OnResetScore?.Invoke();
        }
    }
}
