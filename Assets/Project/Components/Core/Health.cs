using System;
using UnityEngine;

public abstract class Health : MonoBehaviour, IDamageable
{
  public float currentHealth;

  public float CurrentHealth => currentHealth;
  public float MaxHealth;
  public float HealthPercent => (float)currentHealth / MaxHealth;
  public event Action<float> OnHealthPercentChanged;
  public event Action<float> OnHealthChanged;

  public event Action OnDeath;

  public event Action Damaged;

  public void Init(float healthAmount, PlayerStats stats = null)
  {
    if (stats == null)
    {
      currentHealth = healthAmount;
      MaxHealth = healthAmount;
      OnHealthChanged?.Invoke(currentHealth);
    }
    else
    {
      currentHealth = healthAmount;
      MaxHealth = healthAmount;
      stats.OnHealthChanged += OnMaxHealthChanged;
    }



  }

  private void OnMaxHealthChanged(float newMaxHealth)
  {
    currentHealth = newMaxHealth;
    OnHealthChanged?.Invoke(currentHealth);
    OnHealthPercentChanged?.Invoke(HealthPercent);
  }
  public void TakeDamage(float amount)
  {


    if (currentHealth <= amount)
    {

      currentHealth = 0f;

      Die();
    }
    else
    {
      currentHealth -= amount;
      Damaged?.Invoke();
    }

    OnHealthChanged?.Invoke(currentHealth);
    OnHealthPercentChanged?.Invoke(HealthPercent);

  }
  protected virtual void Die()
  {

    Debug.Log("I`m dead");
    OnDeath?.Invoke();
  }
}