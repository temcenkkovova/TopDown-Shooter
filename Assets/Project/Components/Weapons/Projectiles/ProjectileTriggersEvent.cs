using UnityEngine;

public class ProjectileTriggersEvent : MonoBehaviour
{
  private Projectile projectile;

  void Start()
  {
    projectile = GetComponent<Projectile>();
  }

  public void OnTriggerEnter(Collider other)
  {
    if (projectile != null && projectile.IsOwner(other)) return;
    if (other.tag != projectile.ownerProjectileTag && other.GetComponent<IDamageable>() != null)
    {
      GameObject fx = Instantiate(projectile.hitEffectPrefab, transform.position, Quaternion.identity);
      HitEffect effect = fx.GetComponent<HitEffect>();

      effect.SetColor(Color.black);


      projectile.ReleaseAttack(other.GetComponent<IDamageable>());
      Destroy(transform.gameObject);
    }

  }
}