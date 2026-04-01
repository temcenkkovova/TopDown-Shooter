using UnityEngine;

public class ItemTriggerEvents : MonoBehaviour
{
  private TakeItem takeItem;

  void Awake()
  {
    takeItem = GetComponent<TakeItem>();
  }
  void OnTriggerEnter(Collider other)
  {

    IWeaponEquipment receiver = other.GetComponent<IWeaponEquipment>();
    if (receiver == null) return;

    Item item = takeItem.Take();
    receiver.Equip(item);
    Destroy(transform.gameObject);

  }
}