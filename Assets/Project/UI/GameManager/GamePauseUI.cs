using UnityEngine;

public class GamePauseUI : MonoBehaviour
{

  public GameObject pausePanel;


  void Start()
  {
    GameStateController.Instance.OnPauseChanged += PauseMenu;
  }

  void OnDisable()
  {
    GameStateController.Instance.OnPauseChanged -= PauseMenu;
  }

  public void PauseMenu(bool pause)
  {

    pausePanel.SetActive(pause);
  }
}