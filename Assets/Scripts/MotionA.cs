using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionA : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;
    [SerializeField] private float _checkDistance = 0.05f;

    private Transform _targetWaypoint;
    private int _currentWaypointIndex = 0;
    private Transform _originalParent;

    // Start is called before the first frame update
    void Start()
    {
        _targetWaypoint = _waypoints[0];
        _originalParent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (Vector3)Vector2.MoveTowards(
            current: (Vector2)transform.position,
            (Vector2)_targetWaypoint.position,
            maxDistanceDelta: _speed * Time.deltaTime
            );

        if (Vector2.Distance(a: (Vector2)transform.position, b:_targetWaypoint.position) < _checkDistance)
        {
            _targetWaypoint = GetNextWaypoint();
        }
    }

    private Transform GetNextWaypoint()
    {
        _currentWaypointIndex++;
        if (_currentWaypointIndex >= _waypoints.Length)
        {
            _currentWaypointIndex = 0;
        }
        return _waypoints[_currentWaypointIndex];
    }

    public void SetParent(Transform newParent)
    {
        _originalParent = transform.parent;
        transform.parent = newParent;
    }

    public void ResetParent()
    {
        transform.parent = _originalParent;
    }
}
