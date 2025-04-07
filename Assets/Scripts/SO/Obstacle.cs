using System.Collections.Generic;
using UnityEngine;

namespace ldjam_hellevator
{
    [CreateAssetMenu(fileName = "Obstacle", menuName = "Scriptable Objects/Obstacle")]
    public class Obstacle : ScriptableObject
    {
        [Header("Obstacle Prefab")] [SerializeField]
        public GameObject prefab;
        [Header("Obstacle Properties")]
        [SerializeField] private float spawnFrequencySecDefault = 0.5f;
        [SerializeField] private float spawnProbabilityDefault = 1.0f;
        [SerializeField] private float minFrequencySec = 0.25f;
        
        public List<int> spawningLanes = new List<int>(){0, 1, 2, 3};
        public float offset = 0.5f;
        public float difficultyThreshold = 0f;
        
            
        [Header("Debug")] [SerializeField] 
        private float spawnFrequencySec;
        public float SpawnFrequencySec
        {
            get => spawnFrequencySec;
            set
            {
                spawnFrequencySec = value;
                if (spawnFrequencySec < minFrequencySec)
                {
                    spawnFrequencySec = minFrequencySec;
                }
            }
        }
        
        [SerializeField] private float spawnProbability;
        public float SpawnProbability
        {
            get => spawnProbability;
            set
            {
                spawnProbability = value;
                if (spawnProbability > 1f)
                {
                    spawnProbability = 1f;
                }
            }
        }

        private void OnEnable()
        {
            SpawnProbability = spawnProbabilityDefault;
            SpawnFrequencySec = spawnFrequencySecDefault;
        }
    }
}