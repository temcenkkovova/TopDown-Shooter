using UnityEngine;

[CreateAssetMenu(menuName = "Player")]
public class PlayerConfig : ScriptableObject
{
  public float maxHealth;
  public float speed;
  public float rotateSpeed;
  public int expToLevel;
  public float multiplyExp;
  public float damage;

}