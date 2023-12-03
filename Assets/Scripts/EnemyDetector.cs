using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDetector : MonoBehaviour
{
    [Header("Settings")]
    private Transform _dectectedEnemy;
    [SerializeField] private float _maxAttackRange = 10f;
    [Header("Events")]
    [SerializeField] private UnityEvent<Transform> OnEnemyDetected;
    [SerializeField] private UnityEvent OnEnemyLost;
    private void OnTriggerStay(Collider other)
    {
        if(_dectectedEnemy == null && other.CompareTag("Enemy"))
        {
            _dectectedEnemy = other.gameObject.transform;
            OnEnemyDetected?.Invoke(_dectectedEnemy);
        }
    }

    private void Update(){
        if (_dectectedEnemy != null)
        {

            // Check Attack Distance
            float distance = Vector3.Distance(transform.position, _dectectedEnemy.position);
            if (distance > _maxAttackRange || !_dectectedEnemy.gameObject.activeSelf)
            {
                _dectectedEnemy = null;
                OnEnemyLost?.Invoke();
            }
        }
    }
}
