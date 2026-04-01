

using UnityEngine;

public class ThirdPhaseState : IPhaseState
{
  private Enemy enemy;
  private BossPhaseController controller;
  public ThirdPhaseState(Enemy enemy, BossPhaseController controller)
  {

  }
  public void Enter()
  {
    Debug.Log("THIRD PHASE");
  }
  public void Update()
  {

  }
  public void Exit()
  {

  }
}