using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ldjam_hellevator
{
    public class DifficultyManager : MonoBehaviour
    {
        [SerializeField] private ScoreManagerChannel scoreManagerChannel;

        [SerializeField] private int difficultyIncreaseInterval = 10;

        private int _currentLevel = 0;

        [SerializeField] private List<Obstacle> obstacles;
        [SerializeField] private WallData wallData;

        void Awake()
        {
            ResetDifficulty();
            scoreManagerChannel.OnGetDifficultyLevel = () => _currentLevel;
        }

        private void ResetDifficulty()
        {
            _currentLevel = 0;
            foreach (var obstacle in obstacles)
            {
                obstacle.SpawnFrequencySec = obstacle.spawnFrequencySecDefault;
                obstacle.SpawnProbability = obstacle.spawnProbabilityDefault;
            }
            wallData.currentWallSpeed = wallData.baseWallSpeed;
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
           var newLevel = (int)scoreManagerChannel.GetDepth() / difficultyIncreaseInterval;
           if (newLevel != _currentLevel)
           {
               _currentLevel = (int)newLevel;
               AdjustDifficulty(_currentLevel);
           }
        }

        // UNIFORM scaling 
        void AdjustDifficulty(int level)
        {
            foreach (var obstacle in obstacles)
            {
                if (scoreManagerChannel.GetDepth() >= obstacle.difficultyThreshold)
                {
                    obstacle.SpawnFrequencySec -= (level * 0.05f);
                    obstacle.SpawnProbability += (level * 0.005f);
                }
            }
            
            var newSpeed = wallData.currentWallSpeed + wallData.wallSpeedIncreaseRate;
            if (newSpeed <= wallData.maxWallSpeed)
            {
                wallData.currentWallSpeed = newSpeed;
            }
        }
    }
}
