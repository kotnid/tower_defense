using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawn
{
   public GameObject enemy;
   public int baseCount;
   public float baseRate;
   public int incCount;
   public int incRate;
   public int startLevel;
}
