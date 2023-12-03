using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    [SerializeField] private Renderer[] _renderers;
    [SerializeField] Color _flashColor = Color.white;
    [SerializeField] Color _originalColor = Color.white;
    [SerializeField] private float _flashDuration = 20f;
    [SerializeField] private bool _playOnAwake = false;

    private void Awake()
    {
        if(_playOnAwake)
        {
            PlayFlash();
        }
    }
    
    public void PlayFlash()
    {
        foreach(Renderer renderer in _renderers)
        {
           renderer.material.color = _flashColor;
        }
    }

    private void Update(){

        if(_renderers.Length == 0)
        {
            return;
        }

        if(_renderers[0].material.color == _originalColor)
        {
            return;
        }
        foreach(Renderer renderer in _renderers)
        {
            renderer.material.color = Color.Lerp(renderer.material.color, _originalColor, _flashDuration * Time.deltaTime);
        }
    }

}
