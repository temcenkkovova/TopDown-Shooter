
using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{

    public PlayerConfig config;
    [NonSerialized] public PlayerMovement movement;
    [NonSerialized] public PlayerLevel playerLevel;
    [NonSerialized] public PlayerHealth playerHealth;
    public PlayerStats playerStats;
    public UpgradeManager upgradeManager;



    void Awake()
    {
        playerStats = new PlayerStats(config);
        upgradeManager.Init(playerStats);
        movement = GetComponent<PlayerMovement>();
        playerLevel = GetComponent<PlayerLevel>();
        playerHealth = GetComponent<PlayerHealth>();

        playerHealth.Init(playerStats.Health, playerStats);
        playerLevel.Init(playerStats.ExpToLevel, playerStats.MultiplyExp);
        movement.Init(playerStats);



    }

}
