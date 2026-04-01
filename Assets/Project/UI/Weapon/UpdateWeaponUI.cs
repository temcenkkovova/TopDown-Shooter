using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateWeaponUI : MonoBehaviour
{
  public Button updateLvlButton;
  public TMP_Text textField;
  public PlayerWeaponController weaponController;
  public WaveController waveController;
  private bool isPreparing;
  private bool canUpgrade;
  private float updateCost;


  void Start()
  {
    ChangeButtonStatus(0f);
    GameEconomy.Instance.OnTotalCoinChanged += ChangeButtonStatus;
    waveController.OnWaveStateChanged += ToggleButtonStatus;
    weaponController.currentWeapon.WeaponStatsChanged += SetUpdateCost;
    SetUpdateCost();
  }
  void OnDisable()
  {
    GameEconomy.Instance.OnTotalCoinChanged -= ChangeButtonStatus;
    waveController.OnWaveStateChanged -= ToggleButtonStatus;
  }

  public void SetUpdateCost()
  {
    updateCost = weaponController.currentWeapon.CostUpdateLevel;
    textField.text = "Upgrade ( " + updateCost + " )";

  }

  void ChangeButtonStatus(float value)
  {
    bool state = GameEconomy.Instance.HasMoney(weaponController.currentWeapon.CostUpdateLevel);

    canUpgrade = state;
    if (state)
    {
      updateLvlButton.interactable = true;
    }
    else
    {
      updateLvlButton.interactable = false;
    }
  }
  public void ToggleButtonStatus(WaveController.WaveState waveState)
  {

    bool state = waveState == WaveController.WaveState.Preparing;
    isPreparing = state;
    if (canUpgrade)
    {
      updateLvlButton.interactable = true;
    }
    else
    {
      updateLvlButton.interactable = false;
    }
  }
}