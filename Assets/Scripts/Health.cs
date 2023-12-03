using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int _maxHealth = 100;
    private int _currentHealth = 100;
    [Header("Events")]
    [SerializeField] private UnityEvent<int> OnReceiveDamage;
    [SerializeField] private UnityEvent<int> OnHeal;
    [SerializeField] private UnityEvent OnZeroHealth;
    public int CurrentHealth
    {
        get { return _currentHealth; }
        set { 
            _currentHealth = value; 
            }
    }

    private void OnEnable()
    {
        CurrentHealth = _maxHealth;
        OnHeal?.Invoke(_currentHealth);
    }
    
    public void ReceiveDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        OnReceiveDamage?.Invoke(_currentHealth);
        if (_currentHealth <= 0)
        {
            OnZeroHealth?.Invoke();
        }

    }

    public void KillInstance()
    {
        ReceiveDamage(_maxHealth);
    }
}
