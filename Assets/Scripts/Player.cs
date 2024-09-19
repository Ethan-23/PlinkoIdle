using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    /*//Base stats
    float baseCooldownDuration = 2f;
    float baseBallSpeed = 50f;*/
    
    //Resets???
    [SerializeField] int prestige = 0;
    [SerializeField] int ascension = 0;

    [Header("Currency")]
    [SerializeField] float coins = 0f;
    [SerializeField] float crystals = 0f; //Chips????
    [SerializeField] float ascensionThing; //Gambling Themed?

    [Header("Upgrades")]
    [Header("Ball")]
    [SerializeField] int cooldownUpgrade = 0;
    [SerializeField] int ballSpeedUpgrade = 0;
    [SerializeField] int specialBallUpgrade = 0;
    [SerializeField] int ballValueUpgrade = 0;

    [Header("Bonkers")]
    [SerializeField] int bonkerValueUpgrade = 0; // Does not multiply unless special ball
    [SerializeField] int glowingBonkersUpgrade = 0;

    [Header("Robots")]

    [Header("Misc")]
    [SerializeField] int boardSize = 1;
    
    public float Coins
    {
        get { return coins; }
        set { coins = value; }
    }

    public int CooldownUpgrade
    {
        get { return cooldownUpgrade; }
        set { cooldownUpgrade = value; }
    }

    public int BallValueUpgrade
    {
        get { return ballValueUpgrade; }
        set { ballValueUpgrade = value; }
    }

    public int BallSpeedUpgrade
    {
        get { return ballSpeedUpgrade; }
        set { ballSpeedUpgrade = value; }
    }

    public int SpecialBallUpgrade
    {
        get { return specialBallUpgrade; }
        set { specialBallUpgrade = value; }
    }

    public int BonkerValueUpgrade
    {
        get { return bonkerValueUpgrade; }
        set { bonkerValueUpgrade = value; }
    }

    public int GlowingBonkersUpgrade
    {
        get { return glowingBonkersUpgrade; }
        set { glowingBonkersUpgrade = value; }
    }

    public int BoardSize
    {
        get { return boardSize; }
        set { boardSize = value; }
    }

}
