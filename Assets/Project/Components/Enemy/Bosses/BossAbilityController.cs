using System.Collections.Generic;
using UnityEngine;

public class BossAbilityController : MonoBehaviour
{

  private BossConfig bossConfig;
  private Enemy enemy;



  private List<IBossAbility> allAbilities = new();
  private List<IBossAbility> activeAbilities = new();
  public void Init(BossConfig config, Enemy enemy, Transform transform)
  {

    this.enemy = enemy;

    foreach (var abilityConfig in config.abilityConfigs)
    {

      if (abilityConfig is SlamAbilityConfig slamConfig)
      {
        SlamAbility slam = new SlamAbility(slamConfig);
        slam.Initialize(enemy, transform);
        allAbilities.Add(slam);
      }
      /*Дальше буду вот так расширять */
      if (abilityConfig is SpawnEnemyAbilityConfig spawnConfig)
      {

        SpawnEnemyAbility spawn = new SpawnEnemyAbility(spawnConfig, SpawnEnemy.Instance);
        spawn.Initialize(enemy, transform);
        allAbilities.Add(spawn);
      }
    }


  }
  void Start()
  {

    GameStateController.Instance.OnGameStateChanged += OnGameStateChanged;

  }
  void OnDisable()
  {
    GameStateController.Instance.OnGameStateChanged -= OnGameStateChanged;
  }

  void Update()
  {
    float dTime = Time.deltaTime;

    for (int i = 0; i < activeAbilities.Count; i++)
    {
      activeAbilities[i].Tick(dTime);
    }
  }

  public void EnableAbility<T>() where T : IBossAbility
  {
    foreach (var ability in allAbilities)
    {

      if (ability is T)
      {

        ability.Enable();
        if (!activeAbilities.Contains(ability))

          activeAbilities.Add(ability);
      }
    }
  }
  public void DisableAbility<T>() where T : IBossAbility
  {
    foreach (var ability in allAbilities)
    {
      if (ability is T)
      {
        ability.Disable();
        activeAbilities.Remove(ability);
      }
    }
  }

  public void CancelAll()
  {
    foreach (var ability in activeAbilities)
    {
      ability.Cancel();
    }

    activeAbilities.Clear();
  }

  private void OnGameStateChanged(GameState state)
  {
    enabled = state == GameState.Gameplay;

  }
}