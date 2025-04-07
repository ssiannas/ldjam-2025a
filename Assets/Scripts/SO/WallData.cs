using UnityEngine;

namespace ldjam_hellevator
{
    [CreateAssetMenu(fileName = "WallData", menuName = "Scriptable Objects/WallData")]
    public class WallData : ScriptableObject
    {
        [SerializeField] public float baseWallSpeed = 1.5f;
        public float maxWallSpeed = 5f;
        public float wallSpeedIncreaseRate = 0.4f;
        public float currentWallSpeed = 1.5f;
        public void OnEnable()
        {
            currentWallSpeed = baseWallSpeed;
        }
    }
}
