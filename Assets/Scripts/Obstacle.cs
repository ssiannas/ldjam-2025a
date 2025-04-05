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
        public float spawnFrequencySec = 0.5f;
        public float spawnProbability = 1.0f;
        public List<int> spawningLanes = new List<int>(){0, 1, 2, 3};
        public float offset = 0.5f;
    }
}