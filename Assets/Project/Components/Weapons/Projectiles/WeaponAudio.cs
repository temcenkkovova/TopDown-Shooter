using UnityEngine;

public class WeaponAudio : MonoBehaviour
{
  private AudioSource audioSource;
  [SerializeField] private AudioClip FiredClip;
  [SerializeField, Range(0f, 1f)] private float firedVolume = 0.3f;

  void Awake()
  {
    audioSource = GetComponent<AudioSource>();

  }
  public void PlayFired()
  {

    audioSource.PlayOneShot(FiredClip, firedVolume);
  }
}