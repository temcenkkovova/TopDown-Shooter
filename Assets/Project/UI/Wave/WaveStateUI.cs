using System;
using TMPro;
using UnityEngine;

public class WaveStateUI : MonoBehaviour
{
  public TMP_Text waveField;
  public TMP_Text enemyCountField;


  // public TMP_Text waveStateField;
  // public TMP_Text currentWaveStateField;

  public TMP_Text preparingTimeField;
  public GameObject preparingTimePanel;
  public GameFlowController gameFlowController;
  public EnemyManager enemyManager;
  public WaveController waveController;

  public WaveMenuControllerUI waveMenuControllerUI;
  void Start()
  {
    ShowCountWave(0);
    preparingTimePanel.SetActive(false);
  }

  void OnEnable()
  {
    // waveController.OnWaveStateChanged += ShowWaveState;
    gameFlowController.OnWaveIndexChanged += ShowCountWave;
    enemyManager.OnEnemyAmountChanged += ShowCountEnemies;
    waveController.OnPreparingTimeChanged += ShowPreparingTimer;
    waveMenuControllerUI.OnClickedStartWave += OpenTimer;
  }
  void OnDisable()
  {
    // waveController.OnWaveStateChanged -= ShowWaveState;
    gameFlowController.OnWaveIndexChanged -= ShowCountWave;
    enemyManager.OnEnemyAmountChanged -= ShowCountEnemies;
    waveController.OnPreparingTimeChanged -= ShowPreparingTimer;
    waveMenuControllerUI.OnClickedStartWave -= OpenTimer;
  }
  public void ShowCountWave(int indexWave)
  {
    waveField.text = "Wave " + (indexWave + 1).ToString();

  }

  public void ShowCountEnemies(int value)
  {
    enemyCountField.text = "Live enemies : " + value.ToString();
  }
  public void ShowWaveState(WaveController.WaveState waveState)
  {
    // currentWaveStateField.text = waveController.CurrentWaveState.ToString();
    // waveStateField.text = waveState.ToString();
  }
  public void OpenTimer()
  {
    preparingTimePanel.SetActive(true);
  }
  public void ShowPreparingTimer(float timer)
  {

    int seconds = Mathf.CeilToInt(timer);
    preparingTimeField.text = seconds.ToString();
    if (seconds == 0)
    {
      preparingTimePanel.SetActive(false);
    }

  }
}