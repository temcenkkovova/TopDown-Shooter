using System.Collections.Generic;
using UnityEngine;



public abstract class PlayerUpgradeConfig : ScriptableObject
{
  public Sprite icon;
  public string title;

  [TextArea] public string description;
  public float cost;
  public abstract bool CanApply(PlayerStats stats);
  public abstract void Apply(PlayerStats stats);

}