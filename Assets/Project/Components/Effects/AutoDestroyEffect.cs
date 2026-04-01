using UnityEngine;

public class AutoDestroyEffect : MonoBehaviour
{
  public float lifeTime = 0.2f;

  void OnEnable()
  {
    Destroy(gameObject, lifeTime);
  }
}