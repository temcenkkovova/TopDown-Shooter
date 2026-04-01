using UnityEngine;

public class SlamAbility : IBossAbility
{
  private SlamAbilityConfig config;
  private Enemy enemy;
  private BossComponent boss;

  private enum State { Cooldown, Windup, Execute }
  private State currentState;
  private AbilityIndicator indicator;
  private float timer;
  private GameObject activeIndicator;
  private Transform targetTr;

  private Vector3 currentSlamPosition;

  public SlamAbility(SlamAbilityConfig config)
  {
    this.config = config;
  }

  public void Initialize(Enemy enemy, Transform targetTr)
  {
    this.enemy = enemy;
    currentState = State.Cooldown;
    timer = config.cooldown;
    this.targetTr = targetTr;
    boss = enemy.GetComponent<BossComponent>();
  }

  public void Enable()
  {
    currentState = State.Cooldown;
    timer = config.cooldown;
  }

  public void Disable()
  {
    Cancel();
  }

  public void Tick(float deltaTime)
  {
    timer -= deltaTime;

    switch (currentState)
    {
      case State.Cooldown:
        if (timer <= 0f)
        {
          enemy.SetSkillCasting(true);
          boss.HandleSlamCast();
          StartWindup();
        }
        break;

      case State.Windup:
        if (timer <= 0f)
        {
          Execute();
        }
        break;

      case State.Execute:
        if (timer <= 0f)
        {
          enemy.SetSkillCasting(false);
          StartCooldown();
        }
        break;
    }
  }

  private void StartWindup()
  {

    currentState = State.Windup;
    timer = config.windupTime;

    activeIndicator = Object.Instantiate(
     config.indicatorPrefab.gameObject,
     targetTr.position,
     Quaternion.Euler(0f, 0f, 0f));
    currentSlamPosition = targetTr.position;
    indicator = activeIndicator.GetComponent<AbilityIndicator>();
    float diameter = config.radius * 2f;
    activeIndicator.transform.localScale =
        new Vector3(diameter, 0.02f, diameter);

    indicator.StartFill(config.windupTime);
  }

  private void Execute()
  {

    currentState = State.Execute;
    timer = config.activeTime;

    if (activeIndicator != null)
      Object.Destroy(activeIndicator);

    GameObject effect = Object.Instantiate(
         config.effectPrefab,
         currentSlamPosition,
         Quaternion.identity);

    Collider[] hits = Physics.OverlapSphere(
        currentSlamPosition,
        config.radius,
        config.layerMask);

    foreach (var hit in hits)
    {
      if (hit.TryGetComponent<IDamageable>(out var damageable))
      {

        damageable.TakeDamage(config.damage);

      }

    }
    GameObject.Destroy(effect);
  }

  private void StartCooldown()
  {
    currentState = State.Cooldown;
    timer = config.cooldown;
  }

  public void Cancel()
  {

    if (activeIndicator != null)
      Object.Destroy(activeIndicator);

    currentState = State.Cooldown;
    timer = config.cooldown;
  }
}