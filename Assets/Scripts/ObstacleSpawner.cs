using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ldjam_hellevator
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private List<Obstacle> obstacles = new List<Obstacle>();

        [SerializeField] private GameObject gameArea;
        [SerializeField] private ScoreManagerChannel scoreManagerChannel;

        private readonly List<float> _laneCoordinates = new List<float>();
        private float _laneWidth = 0f;
        private Vector2 _gameAreaBottomLeft;

        private void Awake()
        {
            var leftEdge = gameArea.transform.position.x - gameArea.transform.localScale.x / 2;
            var rightEdge = gameArea.transform.position.x + gameArea.transform.localScale.x / 2;
            _gameAreaBottomLeft = new Vector2(leftEdge, gameArea.transform.position.y - gameArea.transform.localScale.y / 2);
            _laneWidth = (rightEdge - leftEdge) / 4;
    
            for (var i = 0; i < 4; i++)
            {
                _laneCoordinates.Add(leftEdge + _laneWidth * i);
            }

            foreach (var obstacle in obstacles)
            {
                StartCoroutine(SpawnRoutine(obstacle));
            }
        }

        // Update is called once per frame
        private void Update()
        {
            
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(gameArea.transform.position, gameArea.transform.localScale);

            foreach (var lane in _laneCoordinates)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(new Vector3(lane, -5, 0), new Vector3(lane, 5, 0));
            }
        }

        IEnumerator SpawnRoutine(Obstacle obstacle)
        {
            yield return new WaitForSeconds(obstacle.SpawnFrequencySec);
            var probability = Random.Range(0f, 1f);
            if (probability < obstacle.SpawnProbability)
            {
                var lane = obstacle.spawningLanes[Random.Range(0, obstacle.spawningLanes.Count)];
                var laneCoordinate = _laneCoordinates[lane];
                var offset = Random.Range(-obstacle.offset, obstacle.offset);
                var center = laneCoordinate + _laneWidth / 2f;
                var xCoord = Mathf.Clamp(center + offset, laneCoordinate, laneCoordinate + _laneWidth);
                var yCoord = _gameAreaBottomLeft.y - 10;
                var spawnPosition = new Vector2(xCoord, yCoord);
                if (scoreManagerChannel.GetScore() >= obstacle.difficultyThreshold)
                {
                    Instantiate(obstacle.prefab, spawnPosition, Quaternion.identity);
                }
            }
            // repeat the spawn routine
            StartCoroutine(SpawnRoutine(obstacle));
        }
    }
}