using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{

    [Header("Assets")]
    [SerializeField] GameObject _gunPrefab;
    [SerializeField] GameObject _cannonPrefab;
    [SerializeField] GameObject _laserPrefab;
    [SerializeField] Camera _mainCamera;
    private GameObject _heldWeapon;
    [Header("Settings")]
    [SerializeField] float _rayDistance = 18f;
    [SerializeField] LayerMask _weaponPositionMask;
    [Header("Data")]
    [SerializeField] private GameState _gameState;
    [Header("Events")]
    [SerializeField] private UnityEvent OnItemBought;
    [SerializeField] private UnityEvent NotEnoughGold;

    public void StartHeldWeapon()
    {
        StartCoroutine(HeldWeaponRoutine());
    }

    public void CreateWeapon(string weaponType)
    {   
        if(_heldWeapon != null)
            return;
        if(!GameManager.Instance.ResourcesController.HasEnoughGoldForWeapon(weaponType))
        {
            Debug.Log("Not enough gold for weapon " + weaponType);
            GameManager.Instance.MessageController.EnqueueMessage("Not enough gold for weapon " + weaponType,3);
            NotEnoughGold?.Invoke();
            return;
        }
        OnItemBought?.Invoke();
        switch(weaponType)
        {
            case "Gun":
                _heldWeapon = Instantiate(_gunPrefab);
                break;
            case "Cannon":
                _heldWeapon = Instantiate(_cannonPrefab);
                break;
            case "Laser":
                _heldWeapon = Instantiate(_laserPrefab);
                break;
            default:
                Debug.LogError("Weapon type not found " + weaponType);
                break;
        }
    }

    IEnumerator HeldWeaponRoutine()
    {
        while(_gameState.GamePlayingState == GameState.State.Playing)
        {
            if(_heldWeapon != null)
            {
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if(Physics.Raycast(ray, out hitInfo,_rayDistance,_weaponPositionMask))
                {
                    _heldWeapon.transform.position = hitInfo.point;
                    if(Input.GetMouseButtonDown(0) && hitInfo.collider.CompareTag("WeaponSlot") && hitInfo.transform.childCount == 0)
                    {
                        _heldWeapon.transform.position = hitInfo.transform.position - Vector3.up * 0.5f;
                        _heldWeapon.transform.SetParent(hitInfo.transform);
                        _heldWeapon.GetComponent<WeaponAttack>().StartAttack();
                        _heldWeapon = null;
                    }
                }
                if(Input.GetMouseButtonDown(1) && _heldWeapon != null)
                {
                    Destroy(_heldWeapon);
                    _heldWeapon = null;
                    GameManager.Instance.ResourcesController.ReturnWeaponCost();
                }
            }
            yield return null;
        }
    }
}
