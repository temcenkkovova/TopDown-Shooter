using System;
using UnityEngine;
public class PlayerHealth : Health
{

  public static Action OnPlayerDied;
  protected override void Die()
  {
    base.Die();
    OnPlayerDied?.Invoke();

  }




}