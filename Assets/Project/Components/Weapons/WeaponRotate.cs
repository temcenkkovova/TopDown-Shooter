using UnityEngine;

public class WeaponRotate : MonoBehaviour
{

  private Transform tr;
  public float rotateSpeed = 60f;

  void Awake()
  {
    tr = transform;
  }

  void Update()
  {
    transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
  }
}