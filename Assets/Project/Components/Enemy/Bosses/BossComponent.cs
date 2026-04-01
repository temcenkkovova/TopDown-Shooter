using System;
using UnityEngine;

public class BossComponent : MonoBehaviour
{
  public event Action OnCastingSlamAbility;
  public event Action OnCastingSpawnAbility;
  private Enemy enemy;
  private BossPhaseController bossPhaseController;

  void Awake()
  {
    bossPhaseController = GetComponent<BossPhaseController>();
  }
  public void Init(BossConfig config, Enemy enemy, Transform playerTr)
  {

    bossPhaseController.Init(config, enemy, playerTr);
    DropItemController.Instance.Init(config.dropItems);
    this.enemy = enemy;
    enemy.OnEnemyDied += DropItemController.Instance.DropItem;


  }
  public void HandleSlamCast()
  {
    OnCastingSlamAbility?.Invoke();
  }
  public void HandleSpawnCast()
  {
    OnCastingSpawnAbility?.Invoke();
  }

  void OnDisable()
  {
    enemy.OnEnemyDied -= DropItemController.Instance.DropItem;
  }

}