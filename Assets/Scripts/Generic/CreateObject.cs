using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    [SerializeField] private GameObject _objectToCreate;

    public void InstantiateObject()
    {
        Instantiate(_objectToCreate, transform.position, transform.rotation);
    }
}
