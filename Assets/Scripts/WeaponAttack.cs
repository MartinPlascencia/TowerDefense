using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponAttack : MonoBehaviour
{
    enum WeaponType
    {
        Gun,
        Cannon,
        LaserTurret
    }
    [Header("Settings")]
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private WeaponsData _weaponsData;
    [SerializeField] private LayerMask _enemyLayerMask;
    [SerializeField] private GameState _gameState;
    [Header("Assets")]
    [SerializeField] private Transform _weaponBarrel;
    [Header("CannonAssets")]
    [SerializeField] private GameObject _cannonBallPrefab;
    [SerializeField] private Transform _cannonBallSpawnPoint;
    [Header("Events")]
    [SerializeField] private UnityEvent _onShoot;

    public void StartAttack(){
        StartCoroutine(FireRoutine());
    }

    IEnumerator FireRoutine()
    {
        while (enabled && _gameState.GamePlayingState == GameState.State.Playing){
            Ray ray = new Ray(_weaponBarrel.position, _weaponBarrel.forward);
            if (Physics.Raycast(ray, out RaycastHit hitInfo,_weaponsData.MaxRayDistance,_enemyLayerMask)){
                if(hitInfo.collider.CompareTag("Enemy")){
                    _onShoot?.Invoke();
                    //print(hitInfo.collider.name + " name");
                    if(_weaponType == WeaponType.Cannon){
                        Instantiate(_cannonBallPrefab, _cannonBallSpawnPoint.position, _cannonBallSpawnPoint.rotation);
                    }else{
                        Debug.DrawRay(ray.origin, ray.direction * _weaponsData.MaxRayDistance, Color.red);
                        Health enemyHealth = hitInfo.collider.GetComponent<Health>();
                        enemyHealth.ReceiveDamage(_weaponsData.DamagePower);
                        
                    }
                    yield return new WaitForSeconds(_weaponsData.ShootCooldown);
                }
            }else{
                Debug.DrawRay(ray.origin, ray.direction * _weaponsData.MaxRayDistance, Color.yellow);
            }
            
            yield return null;
        }
    }
}
