using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveTexture : MonoBehaviour
{
    [SerializeField] private Vector2 _direction = new Vector2(1, 0);
    private Material _imageMaterial;
    private void Awake()
    {
        if(GetComponent<Image>() != null)
            _imageMaterial = GetComponent<Image>().material;
        else if(GetComponent<Renderer>() != null)
            _imageMaterial = GetComponent<Renderer>().material;
        else
            Debug.LogError("No Image or Renderer component found on this object");
    }
    private void Update()
    {
        _imageMaterial.mainTextureOffset += _direction * Time.deltaTime;
    }


}
