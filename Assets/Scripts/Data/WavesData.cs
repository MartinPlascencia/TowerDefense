using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Waves", menuName = "ScriptableObjects/CreateWavesData", order = 1)]
public class WavesData : ScriptableObject
{
    [Serializable]
    public struct Wave 
    {
        public int WeakEnemies;
        public int MidEnemies;
        public int HeavyEnemies;
    }

    public Wave[] Waves;


}
