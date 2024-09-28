using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class DropBot : Bot
{
    [Header("Upgrades")]
    [SerializeField] int dropSpeedUpgrade = 0;
    [SerializeField] int multiplierUpgrade = 0;
    [SerializeField] int specialBallUpgrade = 0;

    public int DropSpeedUpgrade
    { 
        get { return dropSpeedUpgrade; } 
        set { dropSpeedUpgrade = value; } 
    }

    public int MultiplierUpgrade
    {
        get { return multiplierUpgrade; }
        set { multiplierUpgrade = value; }
    }

    public int SpecialBallUpgrade
    {
        get { return specialBallUpgrade; }
        set { specialBallUpgrade = value; }
    }

    public DropBot()
    {
        BotType = "DropBot";
        Level = 1;
    }

}
