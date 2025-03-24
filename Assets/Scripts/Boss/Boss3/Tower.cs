using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
  public enum Towers {
    Basic,
    SlowDown,
    Mage,
    AOE,
    Trapper
  }

  public Towers towerType;

  public float damage;

  public float timeToDamage = 1f;

  public float cooldown;

  public bool isSpecial;

  public int level;

  public float costToUpgrade;

  public abstract void Attack();
}
