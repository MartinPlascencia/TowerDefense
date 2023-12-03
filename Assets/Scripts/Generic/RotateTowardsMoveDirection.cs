using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsMoveDirection : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 10f;
    private Vector3 _lastPosition;
    const float _maxRadiansAllowed = 45f;
    void Update()
    {
        Vector3 moveDirection = transform.position - _lastPosition;
        Vector3 targetDirection = Vector3.RotateTowards(transform.forward, moveDirection, _maxRadiansAllowed, Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDirection), _rotationSpeed * Time.deltaTime);
        _lastPosition = transform.position;
    }
}
