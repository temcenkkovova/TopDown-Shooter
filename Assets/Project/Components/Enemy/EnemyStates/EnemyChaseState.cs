using UnityEngine;

public class EnemyChaseState : IEnemyState
{
  private Enemy enemy;
  private Transform target;
  private EnemyAttack enemyAttack;
  public EnemyChaseState(Enemy enemy, Transform target)
  {
    this.enemy = enemy;
    this.target = target;
    enemyAttack = enemy.GetComponent<EnemyAttack>();
  }
  public void Enter()
  {

  }

  public void Update()
  {
    Vector3 offset = target.position - enemy.transform.position;
    offset.y = 0f;
    float distanceSqr = offset.sqrMagnitude;
    if (distanceSqr <= enemy.enemyConfig.rangeAttack * enemy.enemyConfig.rangeAttack)
    {
      enemy.enemyMovement.ResetVelocity();
      enemy.SwitchState(enemy.AttackState);
      return;
    }

    Vector3 dir = offset.normalized;
    if (enemyAttack.finishedAttack)
    {
      enemy.enemyMovement.Move(dir, enemy.enemyConfig.moveSpeed);
    }


  }
  public void Exit()
  {

  }
}