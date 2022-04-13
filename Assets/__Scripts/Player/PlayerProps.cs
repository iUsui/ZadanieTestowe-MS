using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerProps : MonoBehaviour
{
    public static PlayerProps instance;
    private PlayerController playerController;
    [SerializeField] private PlayerHealth health = null;
    [SerializeField] private int experience;
    [SerializeField] private int score;
    [SerializeField] private TMP_Text scoreText = null;
    private int playerLevel = 1;
    private int currentPlayerAttackDamage;
    private float currentPlayerSpecialAttackCooldown;

    [Header("Levelup props")]
    [Tooltip("0 - experience needed to get to lvl2\n"
    + "1 - experience needed to get to lvl3")]
    [SerializeField] List<int> playerExperienceNeededToLvlUp = new List<int>(2);

    [Tooltip("0 - level1 attack damage\n 1 - level2 attack damage\n 2 - level3 attack damage")]

    [SerializeField] List<int> playerAttackDamageLevels = new List<int>(3);
    [SerializeField] List<float> playerSpecialAttackCooldownLevel = new List<float>(3);

    public PlayerHealth GetHealth() {
        return health;
    }

    public void GainScore(int value) {
        score += value;
        scoreText.text = score.ToString();
    }
    public void GainExperience(int value) {
        experience += value;
        if (playerLevel <= 2) {
            if (playerExperienceNeededToLvlUp[playerLevel-1] <= experience) { LevelUp(); }
        }
    }

    private void LevelUp()
    {
        experience -= playerExperienceNeededToLvlUp[playerLevel-1];
        currentPlayerAttackDamage = playerAttackDamageLevels[playerLevel];
        currentPlayerSpecialAttackCooldown = playerSpecialAttackCooldownLevel[playerLevel];
        playerController.SetAttackDamage(currentPlayerAttackDamage);
        playerController.SetSpecialProjectileCooldown(currentPlayerSpecialAttackCooldown);
        playerLevel++;
        Debug.Log($"Level upped");
    }

    private void OnEnable() {
        instance = this;
        health.OnDie += HandleOnDie;
    }
    private void OnDisable() {
        health.OnDie -= HandleOnDie;
    }

    private void Awake() {
        if (playerAttackDamageLevels.Count != 3 || playerSpecialAttackCooldownLevel.Count != 3) {
            Pause();
            throw new Exception("You have to setup 3 levels !\n" 
            + "-playerAttackDamageLevels\n"
            + "-playerSpecialAttackCooldownLevel");
        }
        currentPlayerAttackDamage = playerAttackDamageLevels[0];
        currentPlayerSpecialAttackCooldown = playerSpecialAttackCooldownLevel[0];
    }

    private void Start() {
        playerController = PlayerController.instance;
        playerController.SetAttackDamage(currentPlayerAttackDamage);
        playerController.SetSpecialProjectileCooldown(currentPlayerSpecialAttackCooldown);
    }
    
    private void HandleOnDie()
    {
        Pause();
        //display menu to restart game and show scoreboard
    }

    private void Pause(){
        Time.timeScale = 0;
    }
    private void Restart() {

    }
}