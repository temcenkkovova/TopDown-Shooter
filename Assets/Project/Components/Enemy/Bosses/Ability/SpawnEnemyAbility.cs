using System;
using UnityEngine;

public class SpawnEnemyAbility : IBossAbility
{
  private SpawnEnemyAbilityConfig config;
  private SpawnEnemy spawnEnemy;
  private bool isSpawning = false;
  private Enemy enemy;
  private BossComponent boss;
  private float timer;
  public event Action OnFinished;
  public SpawnEnemyAbility(SpawnEnemyAbilityConfig config, SpawnEnemy spawnEnemy)
  {
    this.config = config;
    this.spawnEnemy = spawnEnemy;
  }
  public void Initialize(Enemy enemy, Transform targetTr)
  {
    this.enemy = enemy;
    boss = enemy.GetComponent<BossComponent>();
  }

  public void Enable()
  {
    if (config.enemyConfigs == null) return;


    HandleSpawnEnemy();

  }
  public void HandleSpawnEnemy()
  {
    enemy.SetSkillCasting(true);
    boss.HandleSpawnCast();
    spawnEnemy.StartSpawnEnemyCoroutine(config.enemyConfigs, config.radius, config.spawnInterval, OnFinished);
    // spawnEnemy.ChangeEnemyCount(config.enemyConfigs.Count);
    timer = config.cooldown;
    OnFinished += OnFinishedSpawn;
  }

  public void OnFinishedSpawn()
  {
    enemy.SetSkillCasting(false);
  }

  public void Disable()
  {
    Cancel();
  }

  public void Tick(float deltaTime)
  {

    timer -= deltaTime;
    if (timer <= 0)
    {
      HandleSpawnEnemy();
    }


  }
  public void Cancel()
  {

    isSpawning = false;
  }

  // void OnDisable()
  // {
  //   OnFinished -= OnFinishedSpawn;
  // }
}