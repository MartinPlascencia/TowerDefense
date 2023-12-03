using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourcesController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int _startGoldAmount = 100;
    [SerializeField] private int _increaseGoldAmount = 10;
    [SerializeField] private int _increaseGoldDelayTime = 1;
    [SerializeField] private int _gunCost = 100;
    [SerializeField] private int _laserCost = 200;
    [SerializeField] private int _cannonCost = 300;
    [Header("Events")]
    [SerializeField] private UnityEvent<int> OnGoldChange; 
    [Header("Data")]
    [SerializeField] GameState _gameState;
    private int _gold;
    private int cost = 0;
    public int Gold
    {
        get => _gold;
        set
        {
            _gold = value;
        }
    }

    private void Start()
    {
        Gold = _startGoldAmount;
    }

    public void StartIncreasingGold()
    {
        StartCoroutine(IncreaseGoldOverTime());
    }

    IEnumerator IncreaseGoldOverTime()
    {
        while (_gameState.GamePlayingState == GameState.State.Playing)
        {
            yield return new WaitForSeconds(_increaseGoldDelayTime);
            Gold+= _increaseGoldAmount;
            OnGoldChange?.Invoke(Gold);
        }
    }

    public bool HasEnoughGoldForWeapon(string weaponType)
    {
        switch(weaponType)
        {
            case "Gun":
                cost = _gunCost;
                break;
            case "Cannon":
                cost = _cannonCost;
                break;
            case "Laser":
                cost = _laserCost;
                break;
            default:
                Debug.LogError("Weapon type not found " + weaponType);
                break;
        }
        if(cost != 0 && Gold >= cost)
        {
            Gold -= cost;
            OnGoldChange?.Invoke(Gold);
            return true;
        }
        return false;
    }

    public void ReturnWeaponCost(){
        Gold += cost;
        OnGoldChange?.Invoke(Gold);
    }
}
