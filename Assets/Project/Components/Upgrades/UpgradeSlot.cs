
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlot : MonoBehaviour
{
  public TMP_Text titleField;
  public Image image;
  public TMP_Text descriptionField;
  public TMP_Text costField;
  private UpgradeManager upgradeManager;
  private PlayerUpgradeConfig playerUpgradeConfig;
  public Button button;

  private float costUpdateSlot;


  void OnDisable()
  {
    if (upgradeManager != null)
      upgradeManager.OnUpdateStatusChanged -= BlockButton;

    GameEconomy.Instance.OnTotalCoinChanged -= Check;
  }

  void Start()
  {
    GameEconomy.Instance.OnTotalCoinChanged += Check;
  }


  public void Init(PlayerUpgradeConfig config, UpgradeManager refManager)
  {
    upgradeManager = refManager;
    if (upgradeManager != null)
      upgradeManager.OnUpdateStatusChanged += BlockButton;

    float costToUpgrade = config.cost * Mathf.Pow(1.25f, upgradeManager.playerLevel.Level);
    titleField.text = config.title;
    descriptionField.text = config.description;
    image.sprite = config.icon;
    costField.text = "Buy for " + costToUpgrade;
    costUpdateSlot = costToUpgrade;
    playerUpgradeConfig = config;



    BlockButton(GameEconomy.Instance.HasMoney(costToUpgrade));
  }
  public void HandleSlotClick()
  {
    upgradeManager.Upgrade(playerUpgradeConfig);
  }

  public void BlockButton(bool upgradeStatus)
  {
    button.interactable = upgradeStatus;
  }

  public void Check(float amount)
  {
    bool status = costUpdateSlot > amount;
    BlockButton(status);
  }
}