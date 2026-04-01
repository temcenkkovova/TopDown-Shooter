using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveController : MonoBehaviour
{
  private List<EnemyConfig> Enemies = new();
  private SpawnEnemy spawnEnemy;
  private float RadiusSpawn;
  private float SpawnInterval;
  public event Action OnAllEnemiesDefeated;
  public event Action OnAllEnemiesSpawned;
  private WaveController waveController;
  void Awake()
  {
    spawnEnemy = GetComponent<SpawnEnemy>();
    waveController = GetComponent<WaveController>();

    spawnEnemy.WaveEnemiesDefeated += HandleEnemyDefeated;
  }

  void OnDisable()
  {

    spawnEnemy.WaveEnemiesDefeated -= HandleEnemyDefeated;
  }

  public void SetEnemies(List<EnemyConfig> enemies, float radius, float interval)
  {

    Enemies = enemies;
    RadiusSpawn = radius;
    SpawnInterval = interval;
  }

  public void Spawn()
  {

    spawnEnemy.StartSpawnEnemyCoroutine(Enemies, RadiusSpawn, SpawnInterval, OnAllEnemiesSpawned);
  }

  public void HandleEnemyDefeated()
  {
    OnAllEnemiesDefeated?.Invoke();
  }
}