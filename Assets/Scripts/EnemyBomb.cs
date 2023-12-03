using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBomb : MonoBehaviour
{
    [SerializeField] List<Transform> _damageList = new List<Transform>();
    [SerializeField] private Renderer _bombRenderer;
    [SerializeField] private int _damageAmount = 50;
    [SerializeField] private UnityEvent _onDamageDealt;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            _damageList.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            _damageList.Remove(other.transform);
        }
    }

    private void Start()
    {
        StartCoroutine(ExplodeRoutine());
    }

    private IEnumerator ExplodeRoutine()
    {
        float blinkDelay = 1f;
        while(blinkDelay > 0)
        {
            yield return new WaitForSeconds(blinkDelay);
            _bombRenderer.material.SetColor("_EmissionColor", Color.white);
            yield return new WaitForSeconds(0.1f);
            _bombRenderer.material.SetColor("_EmissionColor", Color.red);
            blinkDelay -= 0.1f;
        }
        _bombRenderer.material.SetColor("_EmissionColor", Color.white);
        foreach (Transform damage in _damageList)
        {
            if(damage != null)
                damage.GetComponent<Health>().ReceiveDamage(_damageAmount);
        }
        _onDamageDealt?.Invoke();
    }
}
