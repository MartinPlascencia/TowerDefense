using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private UnityEvent OnDestroy;
    [SerializeField] private float _timeToLive = 3f;

    public void DestroyOnDelay()
    {
        OnDestroy?.Invoke();
        Destroy(gameObject, _timeToLive);
    }
}
