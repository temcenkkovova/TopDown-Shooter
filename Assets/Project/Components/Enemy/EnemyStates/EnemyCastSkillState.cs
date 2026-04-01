public class EnemyCastSkillState : IEnemyState
{
  private EnemyMovement movement;
  public EnemyCastSkillState(Enemy enemy)
  {

    movement = enemy.GetComponent<EnemyMovement>();

  }
  public void Enter()
  {

    movement.canMove = false;
  }
  public void Update()
  {

  }
  public void Exit()
  {
    movement.canMove = true;
  }
}