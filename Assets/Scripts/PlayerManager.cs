using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject previewBall;

    [Header("BasePlayerStats")]
    [SerializeField] float coins = 0f;
    [SerializeField] float baseCooldownDuration = 2f;
    [SerializeField] float baseBallSpeed = 50f;

    [Header("Upgrades:")]
    [Header("Ball")]
    [SerializeField] int cooldownUpgrade = 0;
    [SerializeField] int ballSpeedUpgrade = 0;
    [SerializeField] int specialBallUpgrade = 0;
    [Header("Bonkers")]
    [SerializeField] int bonkerValueUpgrade = 0;
    [SerializeField] int glowingBonkersUpgrade = 0;
    [Header("Bots")]
    [Header("Misc")]
    [SerializeField] int boardSize = 1;

    [SerializeField] Upgrades upgrades;
    [SerializeField] BallController ballController;
    
    
    // Start is called before the first frame update

    private void Awake()
    {
        // Initialize the input actions
    }


    //Coin Functions
    public float GetCoins()
    {
        return (float)Math.Round(coins, 2);
    }
    public void AddCoins(float amount)
    {
        coins = FloatAdd(coins, amount);
    }
    public void RemoveCoins(float amount)
    {
        coins = FloatSub(coins, amount);
    }


    //Speed Functions
    public int GetSpeed()
    {
        return ballSpeedUpgrade;
    }
    public void SetSpeed(int amount)
    {
        ballSpeedUpgrade = amount;
    }
    public void AddSpeed(int amount)
    {
        ballSpeedUpgrade += amount;
    }

    //BonkerValue Functions
    public int GetBonkerUpgrade()
    {
        return bonkerValueUpgrade;
    }
    public void AddBonkerUpgrade(int amount)
    {
        bonkerValueUpgrade += amount;
    }
    public void SetBonkerUpgrade(int amount)
    {
        bonkerValueUpgrade = amount;
    }

    //GlowingBonker Functions
    public int GetGlowingBonkerUpgrade()
    {
        return glowingBonkersUpgrade;
    }
    public void AddGlowingBonkerUpgrade(int amount)
    {
        glowingBonkersUpgrade += amount;
    }
    public void SetGlowingBonkerUpgrade(int amount)
    {
        glowingBonkersUpgrade = amount;
    }


    //SpecialBall Functions
    public int GetSpecialBallUpgrade()
    {
        return specialBallUpgrade;
    }

    //Base Functions
    public float GetBaseSpeed()
    {
        return baseBallSpeed;
    }

    public float GetBaseCooldownDuration()
    {
        return baseCooldownDuration;
    }

    public int GetCooldownUpgrade()
    {
        return cooldownUpgrade;
    }

    

    //Board Size
    public void SetBoardSize(int size)
    {
        boardSize = size;
    }
    public void AddBoardSize(int size)
    {
        boardSize += size;
    }
    public int GetBoardSize()
    {
        return boardSize;
    }

    //Allow for floats to round to 2nd decimal to prevent 0.9999999
    float FloatAdd(float num1, float num2)
    {
        return (float)Math.Round(num1 + num2, 2);
    }

    float FloatSub(float num1, float num2)
    {
        return (float)Math.Round(num1 - num2, 2);
    }

    public Upgrades GetUpgrades()
    {
        return upgrades;
    }

    public BallController GetBallController()
    {
        return ballController;
    }

}
