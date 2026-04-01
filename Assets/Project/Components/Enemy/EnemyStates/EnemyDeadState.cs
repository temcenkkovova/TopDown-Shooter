using UnityEngine;

public class EnemyDeadState : IEnemyState
{
  private Enemy enemy;

  public EnemyDeadState(Enemy enemy)
  {
    this.enemy = enemy;

  }
  public void Enter()
  {

  }
  public void Update()
  {


    enemy.enabled = false; // отключаем Update FSM
    Object.Destroy(enemy.gameObject);


  }
  public void Exit()
  {

  }
}