using UnityEngine;

public interface IBossAbility
{
  void Initialize(Enemy enemy, Transform transform);
  void Enable();
  void Disable();
  void Tick(float deltaTime);
  void Cancel();
}