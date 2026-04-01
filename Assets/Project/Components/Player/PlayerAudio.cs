using UnityEngine;
using UnityEngine.UI;

public class PlayerAudio : MonoBehaviour
{
  [SerializeField] private PlayerHealth playerHealth;
  [SerializeField] private AudioSource audioSource;

  [SerializeField] private AudioClip hitClip;
  [SerializeField] private AudioClip FiredClip;
  [SerializeField, Range(0f, 1f)] private float volume = 0.5f;
  [SerializeField, Range(0f, 1f)] private float firedVolume = 0.3f;
  [SerializeField] private Image damageOverlay;
  [SerializeField] private float fadeSpeed = 10f;
  private float targetAlpha = 0f;

  void Awake()
  {
    playerHealth = GetComponent<PlayerHealth>();
    audioSource = GetComponent<AudioSource>();

  }
  void Update()
  {
    if (damageOverlay == null) return;

    Color c = damageOverlay.color;
    c.a = Mathf.Lerp(c.a, targetAlpha, Time.deltaTime * fadeSpeed);
    damageOverlay.color = c;

    targetAlpha = Mathf.Lerp(targetAlpha, 0f, Time.deltaTime * 2f);
  }

  private void OnEnable()
  {
    playerHealth.Damaged += PlayHit;

  }
  private void OnDisable()
  {
    playerHealth.Damaged -= PlayHit;

  }

  public void PlayHit()
  {
    targetAlpha = 0.4f;
    audioSource.PlayOneShot(hitClip, volume);
  }
}