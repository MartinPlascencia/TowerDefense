using UnityEngine;
[CreateAssetMenu(fileName = "WeaponsData", menuName = "ScriptableObjects/CreateWeaponsData",order = 1)]

public class WeaponsData : ScriptableObject
{
    public float MaxRayDistance = 10;
    public int DamagePower = 10;
    public float ShootCooldown = 1;
}
