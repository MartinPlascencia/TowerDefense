using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        GameManager.Instance.EnemiesOnScene++;
    }

    private void OnDisable()
    {
        GameManager.Instance.EnemiesOnScene--;
    }
}
