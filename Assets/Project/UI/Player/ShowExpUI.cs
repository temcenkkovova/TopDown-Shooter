using TMPro;
using UnityEngine;

public class ShowExpUI : MonoBehaviour
{
  public TMP_Text expField;
  public TMP_Text levelField;
  public PlayerLevel playerLevel;

  void Start()
  {
    expField.text = playerLevel.CurrentExp.ToString() + " / " + playerLevel.ExpToLevel;
    levelField.text = playerLevel.Level.ToString();
  }
  void OnEnable()
  {
    playerLevel.OnExpChanged += ShowExp;
    playerLevel.OnLevelChanged += ShowLevel;
  }
  void OnDisable()
  {
    playerLevel.OnExpChanged -= ShowExp;
    playerLevel.OnLevelChanged -= ShowLevel;
  }

  public void ShowLevel(int level)
  {
    levelField.text = level.ToString();
  }
  public void ShowExp(float exp)
  {
    expField.text = playerLevel.CurrentExp.ToString() + " / " + playerLevel.ExpToLevel;
  }
}