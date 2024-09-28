using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MultiplierBot : Bot
{

    [Header("Upgrades")]
    [SerializeField] int moveSpeedUpgrade = 0;
    [SerializeField] int multiplierUpgrade = 0;
    [SerializeField] int layerUpgrade = 0;

    public int MoveSpeedUpgrade
    {
        get { return moveSpeedUpgrade; }
        set { moveSpeedUpgrade = value; }
    }

    public int MultiplierUpgrade
    {
        get { return multiplierUpgrade; }
        set { multiplierUpgrade = value; }
    }

    public int LayerUpgrade
    {
        get { return layerUpgrade; }
        set { layerUpgrade = value; }
    }

    public MultiplierBot()
    {
        BotType = "MultiplierBot";
        Level = 1;
    }

}
