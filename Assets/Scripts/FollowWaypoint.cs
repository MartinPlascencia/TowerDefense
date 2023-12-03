using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoint : MonoBehaviour
{
    private List<Vector3> _waypointsPositions = new List<Vector3>();
    [Header("Settings")]
    [SerializeField] private string _pathName;

    private int _currentWaypointIndex = 0;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _minimumDistanceFromWaypoint = 0.2f;
    [Header("References")]
    [SerializeField] private GameState _gameState;

    private void Awake()
    {
        //Debug.Log(transform.name + " follower objects name");
    }
    private void OnEnable()
    {
        _currentWaypointIndex = 0;
        StartCoroutine(MoveToNextWaypoint());
    }

    private void OnDisable()
    {
        _waypointsPositions.Clear();
    }

    private void GetWaypoints()
    {
        Transform pathsParent = GameObject.Find("Paths").transform;
        Transform path = pathsParent.GetChild(UnityEngine.Random.Range(0, pathsParent.childCount)).transform;
        for(int i = 0; i < path.childCount; i++)
        {
            _waypointsPositions.Add(path.GetChild(i).position);
        }
    }

    IEnumerator MoveToNextWaypoint(){

        if(_waypointsPositions.Count == 0)
            GetWaypoints();
        float distance = Vector3.Distance(transform.position, _waypointsPositions[_currentWaypointIndex]);
        while(distance > _minimumDistanceFromWaypoint && _gameState.GamePlayingState == GameState.State.Playing){
            transform.position = Vector3.MoveTowards(transform.position, _waypointsPositions[_currentWaypointIndex], _speed * Time.deltaTime);
            distance = Vector3.Distance(transform.position, _waypointsPositions[_currentWaypointIndex]);
            yield return null;
        }
        if(_currentWaypointIndex < _waypointsPositions.Count - 1){
            _currentWaypointIndex++;
            StartCoroutine(MoveToNextWaypoint());
        } 
    }
}
