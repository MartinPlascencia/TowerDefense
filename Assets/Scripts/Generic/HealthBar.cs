using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform _lookAtCamera;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Slider _slider;
    [SerializeField] private Health _health;

    private void Awake()
    {
        _lookAtCamera = Camera.main.transform;
        _canvas.worldCamera = Camera.main;
        _slider.maxValue = _health.CurrentHealth;
        _slider.value = _health.CurrentHealth;

    }

    public void UpdateSliderValue(int newValue)
    {
        _slider.value = newValue;
    }

    private void Update()
    {
        transform.LookAt(_lookAtCamera);
    }
}
