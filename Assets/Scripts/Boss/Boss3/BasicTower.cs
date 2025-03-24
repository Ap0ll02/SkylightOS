using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : Tower
{
  public override float damage = 35;
  public override Tower towerType = Towers.Basic;
  public override float timeToDamage = 1;
  public override float cooldown = 1;
  public override bool isSpecial = false;
  public override int level = 0;
  public override float costToUpgrade = 50;

  public override void Attack() {
    Debug.Log("Attack");
  }
}
