using System;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IGoldRewardable, IExpRewardable
{
  [NonSerialized] public EnemyConfig enemyConfig;
  [NonSerialized] public EnemyAttack enemyAttack;
  [NonSerialized] public EnemyHealth enemyHealth;
  [NonSerialized] public EnemyMovement enemyMovement;
  private EnemyChaseState chaseState;
  private EnemyAttackState attackState;
  private EnemyDeadState deadState;

  public EnemyChaseState ChaseState => chaseState;
  public EnemyAttackState AttackState => attackState;
  private IEnemyState currentState;
  private EnemyAnimationsController enemyAnimationsController;
  public event Action<bool> OnSkillCasting;

  public event Action<float> OnBossHealth;

  public int GoldReward => enemyConfig.coinReward;
  public float ExpReward => enemyConfig.xpReward;

  public event Action<Enemy> OnEnemyDied;


  void Awake()
  {
    enemyAnimationsController = GetComponent<EnemyAnimationsController>();
  }
  public void InitComponents()
  {
    enemyAttack = GetComponent<EnemyAttack>();
    enemyHealth = GetComponent<EnemyHealth>();
    enemyMovement = GetComponent<EnemyMovement>();
  }
  public void InitConfig(EnemyConfig config, Transform targetTr)
  {
    enemyMovement.Init(this);
    enemyConfig = config;
    enemyAttack.Init(enemyConfig.attackConfig, targetTr);
    enemyHealth.Init(enemyConfig.maxHealth);
    chaseState = new EnemyChaseState(this, targetTr);
    attackState = new EnemyAttackState(this, targetTr, enemyConfig.attackConfig);
    deadState = new EnemyDeadState(this);
    SwitchState(chaseState); // стартовое состояние
  }

  public void InitEvents()
  {
    enemyHealth.OnDeath += HandleDeath;
    enemyHealth.Damaged += enemyAnimationsController.GetHitAnimation;
    enemyAttack.OnShoot += enemyAnimationsController.ShootAnimation;
  }

  void Start()
  {
    GameStateController.Instance.OnGameStateChanged += OnGameStateChanged;
  }
  void OnDestroy()
  {
    GameStateController.Instance.OnGameStateChanged -= OnGameStateChanged;
    enemyHealth.OnDeath -= HandleDeath;
    enemyAttack.OnShoot -= enemyAnimationsController.ShootAnimation;
    enemyHealth.Damaged -= enemyAnimationsController.GetHitAnimation;
  }

  public void HandleDeath()
  {
    OnEnemyDied?.Invoke(this);
    SwitchState(deadState);
  }
  public void SwitchState(IEnemyState newState)
  {
    currentState?.Exit();
    currentState = newState;
    currentState.Enter();
  }

  void Update()
  {
    currentState?.Update();
  }
  private void OnGameStateChanged(GameState state)
  {

    enabled = state == GameState.Gameplay;

  }

  public void SetSkillCasting(bool casting)
  {

    OnSkillCasting?.Invoke(casting);
  }
}