using UnityEngine;

public class DropItemRarity : MonoBehaviour
{
  public Light rarityLight;
  public void SetRarity(RarityConfig rarity)
  {
    rarityLight.color = rarity.color;


  }
}