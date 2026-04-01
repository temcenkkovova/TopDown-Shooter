using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
  public GameObject bossHealthPanel;
  public TMP_Text healthField;
  private Health health;
  public Image hpFill;
  public Image hpDelayedFill;
  public float delaySpeed = 0.5f;
  private float maxHpBoss;
  private bool active;
  private float target;
  public SpawnEnemy spawnEnemy;


  void Awake()
  {
    spawnEnemy.OnBossSpawn += InitBoss;
  }

  void OnDisable()
  {
    spawnEnemy.OnBossSpawn -= InitBoss;
    if (health != null)
      health.OnHealthChanged -= ShowBossHealth;

  }
  public void InitBoss(Enemy enemy)
  {
    health = enemy.GetComponent<Health>();
    maxHpBoss = health.MaxHealth;
    health.OnHealthChanged += ShowBossHealth;

    ShowBossHealth(health.MaxHealth);

  }
  public void ShowBossHealth(float health)
  {

    active = health > 0;

    bossHealthPanel.SetActive(active);
    target = health / maxHpBoss;
    hpFill.fillAmount = target;

    healthField.text = Mathf.CeilToInt(health).ToString();
  }

  void Update()
  {
    if (!active) return;
    hpDelayedFill.fillAmount = Mathf.Lerp(hpDelayedFill.fillAmount, target, Time.deltaTime * delaySpeed);
  }
}