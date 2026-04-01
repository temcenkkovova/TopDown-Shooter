using UnityEngine;

public class AbilityIndicator : MonoBehaviour
{

  private Material material;

  private float duration;
  private float timer;
  void Awake()
  {
    material = GetComponent<MeshRenderer>().material;
  }
  public void StartFill(float windupTime)
  {
    duration = windupTime;
    timer = 0f;

  }

  void Update()
  {
    if (duration <= 0) return;

    timer += Time.deltaTime;

    float t = timer / duration;

    material.SetFloat("_Fill", t);

    if (t >= 1f)
    {
      duration = 0f;
    }
  }
}