using System;
using UnityEngine;

public class WaveMenuControllerUI : MonoBehaviour
{
  private WaveController waveController;
  public Transform gridParent;
  public UpgradeSlot itemPrefab;
  public event Action OnClickedStartWave;
  public event Action<bool> OnShowWavePanel;

  private UpgradeManager upgradeManager;

  private WaveConfig config;

  public void Init(WaveConfig waveConfig)
  {
    config = waveConfig;
  }
  void Awake()
  {
    waveController = GetComponent<WaveController>();
    upgradeManager = GetComponent<UpgradeManager>();
    waveController.OnWaveStateChanged += ToggleWaveMenu;

  }

  void OnDisable()
  {
    waveController.OnWaveStateChanged -= ToggleWaveMenu;
  }
  public void HideWavePanel()
  {

    OnShowWavePanel?.Invoke(false);

  }
  public void ToggleWaveMenu(WaveController.WaveState waveState)
  {

    bool state = waveState == WaveController.WaveState.Preparing;


    if (state)
    {
      OnShowWavePanel?.Invoke(true);

      foreach (Transform child in gridParent)
        Destroy(child.gameObject);

      for (int i = 0; i < config.playerUpgradeConfigs.Count; i++)
      {
        UpgradeSlot slot = Instantiate(itemPrefab, gridParent);
        slot.Init(config.playerUpgradeConfigs[i], upgradeManager);
      }
    }
  }

  public void OnClickStartWave()
  {
    OnClickedStartWave?.Invoke();
  }
}