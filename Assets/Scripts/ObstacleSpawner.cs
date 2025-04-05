using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> obstaclePrefabs = new List<GameObject>();

    [SerializeField] private GameObject gameArea;
    [SerializeField] private bool showLanes = false;
    
    // split the game area into 4 vertical lanes. The area is a rectangle. We want to store the coordinates of the lanes to spawn
    // obstacles in the lanes. The lanes are defined by the x coordinates of the left and right edges of the game area.
    private List<float> _laneCoordinates = new List<float>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Get the coordinates of the lanes
        var leftEdge = gameArea.transform.position.x - gameArea.transform.localScale.x / 2;
        var rightEdge = gameArea.transform.position.x + gameArea.transform.localScale.x / 2;
        var laneWidth = (rightEdge - leftEdge) / 4;

        for (var i = 0; i < 4; i++)
        {
            _laneCoordinates.Add(leftEdge + laneWidth * i);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (showLanes)
        {
            DrawLanes();
        }
    }

    private void DrawLanes()
    {
        if (showLanes)
        {
            foreach (var lane in _laneCoordinates)
            {
                // draw lines in the scene view
                Debug.DrawLine(new Vector3(lane, -5, 0), new Vector3(lane, 5, 0), Color.blue, 100f);
            }
        }
    }
}
