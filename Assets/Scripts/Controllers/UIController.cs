using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class UIController : MonoBehaviour
{
    [Header("UI Screens")]
    [SerializeField] private GameObject _titleScreen;
    [SerializeField] private GameObject _loseScreen;
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _gameplayCanvas;
    [Header("Events")]
    [SerializeField] private UnityEvent<float> OnSliderValueChanged;
    [Header("UI Elements")]
    [SerializeField] private TMP_Text _goldText;
    [SerializeField] private CanvasGroup _fadeUI;
    [SerializeField] private CanvasGroup _damageUI;

    [Header("Settings")]
    [SerializeField] private float _fadeDuration = 0.5f;
    [SerializeField] private float _damageDuration = 0.2f;
    [SerializeField] private float _damageStartValue = 0.6f;

    public void ShowGameOverScreen(bool win)
    {
        _gameplayCanvas.SetActive(false);
        if (win)
        {
            _winScreen.SetActive(true);
        }
        else
        {
            _loseScreen.SetActive(true);
        }
    }

    public void ShowFade()
    {
        _fadeUI.alpha = 1f;
        StartCoroutine(FadeElementCoroutine(_fadeUI, 0f, _fadeDuration));
    }

    public void ShowDamageUI()
    {
        _damageUI.alpha = _damageStartValue;
        StartCoroutine(FadeElementCoroutine(_damageUI, 0f, _damageDuration));
    }

    private IEnumerator FadeElementCoroutine(CanvasGroup canvasGroup, float targetValue, float duration)
    {
        float startValue = canvasGroup.alpha;
        float time = 0f;

        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startValue, targetValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = targetValue;
    }

    public void OnSliderValueChange(float value)
    {
        OnSliderValueChanged?.Invoke(value);
    }

    public void RetryGame(){
        SceneLoader.LoadScene("TowerDefense");
    }

    public void UpdateGoldUIText(int gold){
        _goldText.text = "Gold: " + gold.ToString();
    }

    private void Start()
    {
        _gameplayCanvas.SetActive(false);
        _titleScreen.SetActive(true);
        _loseScreen.SetActive(false);
        _winScreen.SetActive(false);
    }
}
