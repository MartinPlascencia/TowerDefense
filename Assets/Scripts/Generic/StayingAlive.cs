using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayingAlive : MonoBehaviour
{
    [SerializeField] private float _timeToLive = 3f;

    private void Start()
    {
        Destroy(gameObject, _timeToLive);
    }
}
