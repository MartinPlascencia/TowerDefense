using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageOnTrigger : MonoBehaviour
{
    [SerializeField] private int _damageAmount = 10;
    [SerializeField] private string _tagToCompare = "Enemy";
    [SerializeField] private UnityEvent OnTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tagToCompare) && other.TryGetComponent(out Health health))
        {
            health.ReceiveDamage(_damageAmount);
            OnTrigger?.Invoke();
        }
    }
}
