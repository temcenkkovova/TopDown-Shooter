using UnityEngine;

public class WaveMenuUI : MonoBehaviour
{
  public WaveMenuControllerUI waveMenuControllerUI;
  public GameObject waveMenuPanel;

  void Awake()
  {

    waveMenuPanel.SetActive(false);
    waveMenuControllerUI.OnShowWavePanel += ToggleMenu;
  }
  void OnDisable()
  {
    waveMenuControllerUI.OnShowWavePanel -= ToggleMenu;
  }

  public void ToggleMenu(bool state)
  {

    waveMenuPanel.SetActive(state);
  }
}