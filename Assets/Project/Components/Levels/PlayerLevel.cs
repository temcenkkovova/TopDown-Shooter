using System;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{

  public int Level { get; private set; } = 1;
  public float CurrentExp { get; private set; }
  public float ExpToLevel { get; set; }
  private float MultiplyExp;
  public event Action<float> OnExpChanged;
  public event Action<int> OnLevelChanged;

  public void Init(float expLevel, float multiplyExp)
  {
    ExpToLevel = expLevel;
    MultiplyExp = multiplyExp;
  }


  public void AddExp(float exp)
  {
    CurrentExp += exp;

    while (CurrentExp >= ExpToLevel) // Делаю через while что бы можно было апнуть уровень несколько раз сразу
    {
      CurrentExp -= ExpToLevel;
      Level++;
      ExpToLevel *= MultiplyExp;
      OnLevelChanged?.Invoke(Level);
    }
    OnExpChanged?.Invoke(CurrentExp);
  }
}