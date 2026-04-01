using UnityEngine;

public class BossPhaseController : MonoBehaviour
{
  private Health health;
  private BossAbilityController abilityController;
  private int currentPhase = 0;

  public void Init(BossConfig config, Enemy enemy, Transform playerTransform)
  {

    abilityController = GetComponent<BossAbilityController>();
    abilityController.Init(config, enemy, playerTransform);
    health = enemy.GetComponent<Health>();
    health.OnHealthPercentChanged += HandlePhaseChange;

    EnterPhase1();
  }
  void OnDestroy()
  {
    if (health != null)
    {
      health.OnHealthPercentChanged -= HandlePhaseChange;

    }

  }
  private void HandlePhaseChange(float hpPercent)
  {
    if (currentPhase == 0 && hpPercent <= 0.7f)
    {
      currentPhase = 1;
      EnterPhase2();
    }
    else if (currentPhase == 1 && hpPercent <= 0.4f)
    {
      currentPhase = 2;
      EnterPhase3();
    }
  }

  private void EnterPhase1()
  {

  }

  private void EnterPhase2()
  {
    abilityController.EnableAbility<SlamAbility>();
  }
  private void EnterPhase3()
  {
    //abilityController.EnableAbility<SlamAbility>();
    // abilityController.DisableAbility<SlamAbility>();
    abilityController.EnableAbility<SpawnEnemyAbility>();

  }
  void OnDisable()
  {
    abilityController?.CancelAll();
  }


}