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
    [Header("Resets")]
    [SerializeField] int prestige = 0;
    [SerializeField] int ascension = 0;

    [Header("Currency")]
    [SerializeField] float coins = 0f;
    [SerializeField] float diamonds = 0f; //Chips????
    [SerializeField] float ascensionThing; //Gambling Themed?

    [Header("Upgrades")]
    [Header("Ball")]
    [SerializeField] int ballSpeedUpgrade = 0;
    [SerializeField] int specialBallUpgrade = 0;
    [SerializeField] int ballValueUpgrade = 0;

    [Header("Bonkers")]
    [SerializeField] int bonkerValueUpgrade = 0; // Does not multiply unless special ball
    [SerializeField] int glowingBonkersUpgrade = 0;

    [Header("Cooldown")]
    [SerializeField] int cooldownUpgrade = 0;
    [SerializeField] int autoCooldownUpgrade = 0;

    [Header("Robots")]
    [SerializeField] List<GameObject> botList;
    [Header("Robot JSON data")]
    [SerializeField] List<DropBot> dropBotData;
    [SerializeField] List<MultiplierBot> multiplierBotData;

    [Header("Misc")]
    [SerializeField] int boardSize = 1;
    
    public float Coins
    {
        get { return coins; }
        set { coins = value; }
    }

    public float Diamonds
    {
        get { return diamonds; }
        set { diamonds = value; }
    }
    
    public int Prestige
    {
        get { return prestige; }
        set { prestige = value; }
    }

    public int CooldownUpgrade
    {
        get { return cooldownUpgrade; }
        set { cooldownUpgrade = value; }
    }

    public int AutoCooldownUpgrade
    {
        get { return autoCooldownUpgrade; }
        set { autoCooldownUpgrade = value; }
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

    public List<GameObject> BotList
    {
        get { return botList; }
        set { botList = value; }
    }

    public List<DropBot> DropBotData
    {
        get { return dropBotData; }
        set { dropBotData = value; }
    }
    
    public List<MultiplierBot> MultiplierBotData
    {
        get { return multiplierBotData; }
        set { multiplierBotData = value; }
    }

    public int BoardSize
    {
        get { return boardSize; }
        set { boardSize = value; }
    }

}
