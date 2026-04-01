using System;
using UnityEngine;

public class EnemyAnimationsController : MonoBehaviour
{
  private Animator animator;
  private Rigidbody rb;

  private BossComponent boss;

  void Awake()
  {
    animator = GetComponent<Animator>();
    rb = GetComponent<Rigidbody>();
    boss = GetComponent<BossComponent>();
    if (boss != null)
    {
      boss.OnCastingSlamAbility += SlamAbilityAnimation;
      boss.OnCastingSpawnAbility += SpawnAbilityAnimation;
    }

  }
  void LateUpdate()
  {

    float speed = rb.velocity.magnitude;

    animator.SetFloat("Speed", speed);
    animator.SetBool("Running", speed > 0.1f);
  }
  public void ShootAnimation()
  {
    animator.SetTrigger("Shoot");
  }
  public void GetHitAnimation()
  {
    animator.SetTrigger("GettingHit");
  }
  public void SlamAbilityAnimation()
  {
    animator.SetTrigger("Slam");
  }
  public void SpawnAbilityAnimation()
  {
    animator.SetTrigger("Spawn");
  }

  void OnDestroy()
  {
    if (boss != null)
    {
      boss.OnCastingSlamAbility -= SlamAbilityAnimation;
      boss.OnCastingSpawnAbility -= SpawnAbilityAnimation;
    }

  }
}