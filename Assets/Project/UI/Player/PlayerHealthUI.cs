
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
  public TMP_Text healthField;

  public PlayerHealth playerHealth;
  public Image hpFill;
  public Image hpDelayedFill;
  public float delaySpeed = 0.5f;
  private bool active;
  private float target;
  private float maxHpPlayer;



  void Start()
  {
    playerHealth.OnHealthChanged += ShowPlayerHealth;
    maxHpPlayer = playerHealth.currentHealth;
    ShowPlayerHealth(playerHealth.currentHealth);
  }
  void OnDisable()
  {
    playerHealth.OnHealthChanged -= ShowPlayerHealth;
  }
  public void ShowPlayerHealth(float health)
  {
    active = health > 0;
    target = health / maxHpPlayer;
    hpFill.fillAmount = target;
    healthField.text = health.ToString();
  }

  void Update()
  {
    if (!active) return;
    hpDelayedFill.fillAmount = Mathf.Lerp(hpDelayedFill.fillAmount, target, Time.deltaTime * delaySpeed);
  }
}