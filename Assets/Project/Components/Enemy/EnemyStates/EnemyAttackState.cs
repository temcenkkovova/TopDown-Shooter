using System;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{

  private Enemy enemy;
  private Transform target;
  private AttackConfig config;



  public EnemyAttackState(Enemy enemy, Transform target, AttackConfig config)
  {
    this.enemy = enemy;
    this.target = target;
    this.config = config;
  }
  public void Enter()
  {

  }
  public void Update()
  {
    Vector3 offset = target.position - enemy.transform.position;
    offset.y = 0f;

    float distanceSqr = offset.sqrMagnitude;

    if (distanceSqr > enemy.enemyConfig.rangeAttack * enemy.enemyConfig.rangeAttack)
    {
      enemy.SwitchState(enemy.ChaseState);
      return;
    }

    enemy.enemyAttack.Shoot();

  }
  public void Exit()
  {

  }
}