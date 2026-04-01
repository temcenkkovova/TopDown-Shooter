using UnityEngine;

public enum AbilityPhase { Cooldown, Windup, Execute };
public class SecondPhaseState : IPhaseState
{
  private Enemy enemy;
  private BossPhaseController controller;
  public SecondPhaseState(Enemy enemy, BossPhaseController controller)
  {

  }
  public void Enter()
  {
    Debug.Log("SECOND PHASE");
  }
  public void Exit()
  {

  }
}