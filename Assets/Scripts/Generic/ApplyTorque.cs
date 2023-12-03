using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyTorque : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private ForceMode _forceMode;
    private Vector3 _forceVector;
    [SerializeField] private Vector2 _xForceRange;
    [SerializeField] private Vector2 _yForceRange;
    [SerializeField] private Vector2 _zForceRange;
    [SerializeField] private bool _applyOnAwake;

    private void Awake()
    {
        _forceVector = new Vector3(Random.Range(_xForceRange.x,_xForceRange.y), Random.Range(_yForceRange.x,_yForceRange.y), Random.Range(_zForceRange.x,_zForceRange.y));
        if (_applyOnAwake)
        {
            ApplyTorqueVector();
        }
    }

    public void ApplyTorqueVector()
    {
        _rigidbody.AddTorque(_forceVector,_forceMode);
    }
}
