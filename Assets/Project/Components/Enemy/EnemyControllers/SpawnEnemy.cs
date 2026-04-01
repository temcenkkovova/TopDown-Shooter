using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
  public bool spawningFinished;
  public Coroutine spawnCoroutine;
  public PlayerComponent player;
  private EnemyManager enemyManager;
  public static SpawnEnemy Instance;
  private int spawnedEnemiesCount;
  private int currentEnemyCount;
  public int CurrentEnemies => currentEnemyCount;
  public event Action WaveEnemiesDefeated;

  private RewardSystem rewardSystem;
  public event Action<Enemy> OnBossSpawn;

  void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
      return;
    }
    Instance = this;
    enemyManager = GetComponent<EnemyManager>();
    rewardSystem = GetComponent<RewardSystem>();

  }
  void Start()
  {
    GameStateController.Instance.OnGameStateChanged += OnGameStateChanged;
  }
  public void StartSpawnEnemyCoroutine(List<EnemyConfig> enemyConfigs, float radiusSpawn, float spawnInterval, Action onFinished)
  {
    enemyManager.ChangeMaxAmount(enemyConfigs.Count);
    spawnCoroutine = StartCoroutine(SpawnEnemyCoroutine(enemyConfigs, radiusSpawn, spawnInterval, onFinished));
  }

  public IEnumerator SpawnEnemyCoroutine(List<EnemyConfig> enemyConfigs, float radiusSpawn, float spawnInterval, Action onFinished)
  {
    if (enemyConfigs == null) yield break;

    spawningFinished = false;
    foreach (var item in enemyConfigs)
    {
      if (item == null || item.prefab == null)
      {
        Debug.Log("EnemyConfig or prefab missing");
        continue;
      }
      Enemy enemy = Instantiate(item.prefab, RandomRadiusSpawn(radiusSpawn), transform.rotation);
      enemyManager.AddEnemy(enemy);
      spawnedEnemiesCount++;
      enemy.InitComponents();
      enemy.InitConfig(item, player.transform);
      enemy.InitEvents();
      enemy.OnEnemyDied += HandleEnemyDeath;
      if (item is BossConfig boss)
      {
        BossComponent bossComponent = enemy.GetComponent<BossComponent>();
        bossComponent.Init(boss, enemy, player.transform);
        OnBossSpawn?.Invoke(enemy);

      }
      yield return new WaitForSeconds(spawnInterval);
    }
    onFinished?.Invoke();
    spawningFinished = true;
  }


  public void HandleEnemyDeath(Enemy enemy)
  {

    if (!enemyManager.spawnedEnemies.Contains(enemy)) return;

    if (enemy is IGoldRewardable goldRewardable)
    {
      rewardSystem.HandleCoinReward(goldRewardable.GoldReward);

    }
    if (enemy is IExpRewardable expRewardable)
    {
      rewardSystem.HandleExpReward(expRewardable.ExpReward);
    }
    enemyManager.RemoveEnemy(enemy);
    if (enemyManager.spawnedEnemies.Count == 0 && enemyManager.maxAmount == 0)
    {
      WaveEnemiesDefeated?.Invoke();
    }
    enemy.OnEnemyDied -= HandleEnemyDeath;
  }

  public Vector3 RandomRadiusSpawn(float radiusSpawn)
  {
    Vector2 dir = UnityEngine.Random.insideUnitCircle.normalized;
    Vector2 randomCircle = dir * radiusSpawn;

    Vector3 spawnPosition = new Vector3(
        player.transform.position.x + randomCircle.x,
        0f,
        player.transform.position.z + randomCircle.y
    );
    return spawnPosition;
  }

  private void OnGameStateChanged(GameState state)
  {


    if (state == GameState.GameOver)
    {
      if (spawnCoroutine != null)
        StopCoroutine(spawnCoroutine);
    }

  }


  void OnDisable()
  {
    GameStateController.Instance.OnGameStateChanged -= OnGameStateChanged;
  }
}