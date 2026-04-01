using System;
using UnityEngine;

public class PlayerStats
{
  private float health;

  public float Health
  {
    get => health; set
    {
      health = value;
      OnHealthChanged?.Invoke(health);
    }
  }
  public event Action<float> OnHealthChanged;

  private float speed;

  public float MoveSpeed
  {
    get => speed; set
    {
      speed = value;
      OnMoveSpeedChanged?.Invoke(speed);
    }
  }
  public event Action<float> OnMoveSpeedChanged;


  public float Armor;
  public float RotateSpeed;
  public int ExpToLevel;
  public float MultiplyExp;

  public PlayerStats(PlayerConfig config)
  {


    health = config.maxHealth;
    MoveSpeed = config.speed;
    MultiplyExp = config.multiplyExp;
    ExpToLevel = config.expToLevel;
    RotateSpeed = config.rotateSpeed;



  }

}