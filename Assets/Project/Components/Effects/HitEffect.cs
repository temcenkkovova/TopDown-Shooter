using UnityEngine;

public class HitEffect : MonoBehaviour
{
  [SerializeField] private ParticleSystem ps;

  public void SetColor(Color color)
  {
    var main = ps.main;
    main.startColor = color;

  }
}