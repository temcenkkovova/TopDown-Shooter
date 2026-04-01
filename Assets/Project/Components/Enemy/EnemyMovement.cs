using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
  [NonSerialized] public float Speed;
  [NonSerialized] public Transform Target;
  private Enemy enemy;
  public bool canMove = true;
  private Rigidbody rb;

  void Awake()
  {
    rb = GetComponent<Rigidbody>();

  }

  public void Init(Enemy enemy)
  {
    this.enemy = enemy;
    enemy.OnSkillCasting += HandleCasting;
  }
  public void Move(Vector3 direction, float speed)
  {

    if (canMove)
    {
      rb.velocity = direction * speed;
      if (direction != Vector3.zero)
        transform.rotation = Quaternion.LookRotation(direction);
    }
    else
    {
      rb.velocity = Vector3.zero * 0f;
    }

  }

  public void ResetVelocity()
  {
    rb.velocity = Vector3.zero;
  }
  public void HandleCasting(bool castingStatus)
  {

    canMove = castingStatus == false ? true : false;
  }

  public void OnDestroy()
  {
    enemy.OnSkillCasting -= HandleCasting;

  }




}