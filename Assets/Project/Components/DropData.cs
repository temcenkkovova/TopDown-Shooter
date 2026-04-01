using UnityEngine;


[CreateAssetMenu(menuName = "Drop/Config")]
public class DropData : ScriptableObject
{
  public DropItem item;
  public float dropChance;
  public int minAmount;
  public int maxAmount;
  public RarityConfig rarity;
}