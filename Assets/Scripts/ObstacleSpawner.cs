using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ldjam_hellevator
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private List<Obstacle> obstacles = new List<Obstacle>();

        [SerializeField] private GameObject gameArea;

        // split the game area into 4 vertical lanes. The area is a rectangle. We want to store the coordinates of the lanes to spawn
        // obstacles in the lanes. The lanes are defined by the x coordinates of the left and right edges of the game area.
        private readonly List<float> _laneCoordinates = new List<float>();
        private float _laneWidth = 0f;
        private Vector2 _gameAreaBottomLeft;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Awake()
        {
            // Get the coordinates of the lanes
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
            // draw the game area
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(gameArea.transform.position, gameArea.transform.localScale);

            // draw the lanes
            foreach (var lane in _laneCoordinates)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(new Vector3(lane, -5, 0), new Vector3(lane, 5, 0));
            }
        }

        IEnumerator SpawnRoutine(Obstacle obstacle)
        {
            yield return new WaitForSeconds(obstacle.spawnFrequencySec);
            var probability = Random.Range(0f, 1f);
            if (probability < obstacle.spawnProbability)
            {
                var lane = obstacle.spawningLanes[Random.Range(0, obstacle.spawningLanes.Count)];
                var laneCoordinate = _laneCoordinates[lane];
                var offset = Random.Range(-obstacle.offset, obstacle.offset);
                var center = laneCoordinate + _laneWidth / 2f;
                var xCoord = Mathf.Clamp(center + offset, laneCoordinate, laneCoordinate + _laneWidth);
                var yCoord = _gameAreaBottomLeft.y - 10;
                var spawnPosition = new Vector2(xCoord, yCoord);
                Instantiate(obstacle.prefab, spawnPosition, Quaternion.identity);
            }
            // repeat the spawn routine
            StartCoroutine(SpawnRoutine(obstacle));
        }
    }
}