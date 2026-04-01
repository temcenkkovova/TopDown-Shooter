using TMPro;
using UnityEngine;

public class ShowEconomyUI : MonoBehaviour
{
  public TMP_Text textField;


  void Start()
  {
    GameEconomy.Instance.OnTotalCoinChanged += ShowCoin;
    ShowCoin(0);
  }

  void OnDisable()
  {
    GameEconomy.Instance.OnTotalCoinChanged -= ShowCoin;
  }

  public void ShowCoin(float coins)
  {

    if (!textField) return;
    textField.text = coins.ToString();

  }
}