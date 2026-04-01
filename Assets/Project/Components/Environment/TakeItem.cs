using UnityEngine;

public class TakeItem : MonoBehaviour, ITakeable
{
  public Item itemData;

  public Item Take()
  {
    return itemData;
  }


}