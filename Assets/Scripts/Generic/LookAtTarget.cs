using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _targetAimOffset = 0.5f;
    [SerializeField] private float _pivotRotationSpeed = 10f;
    private void Update()
    {
        if (_target == null) 
            return;
            
        Vector3 direction = _target.transform.position - transform.position;
        direction.y += _targetAimOffset;
        Quaternion targetRotation = Quaternion.LookRotation(direction,Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _pivotRotationSpeed);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void LoseTarget()
    {
        _target = null;
    }
}
