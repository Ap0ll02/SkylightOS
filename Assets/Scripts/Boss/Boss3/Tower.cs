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

  public abstract Towers towerType;

  public abstract float damage;

  public abstract float timeToDamage = 1f;

  public abstract float cooldown;

  public abstract bool isSpecial;

  public abstract int level;

  public abstract float costToUpgrade;

  public abstract void Attack();
}
