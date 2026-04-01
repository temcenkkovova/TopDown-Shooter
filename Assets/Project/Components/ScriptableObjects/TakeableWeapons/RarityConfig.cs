using UnityEngine;

public enum ItemRarity
{
  Common,
  Uncommon,
  Rare,
  Legendary
}
[CreateAssetMenu(menuName = "Rarity")]
public class RarityConfig : ScriptableObject
{
  public ItemRarity rarity;
  public Color color;

}