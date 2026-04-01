using UnityEngine;

public class FirstPhaseState : IPhaseState
{
  private Enemy enemy;
  private BossPhaseController controller;



  public FirstPhaseState(Enemy enemy, BossPhaseController controller)
  {
    this.enemy = enemy;
    this.controller = controller;
  }
  public void Enter()
  {

    Debug.Log("FIRST PHASE");

  }
  public void Update()
  {

  }
  public void Exit()
  {

  }


}