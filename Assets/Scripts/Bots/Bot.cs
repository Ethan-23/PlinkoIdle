using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bot
{

    [SerializeField] string botType;
    [SerializeField] int level = 1;

    public string BotType 
    { 
        get { return botType; } 
        set {  botType = value; }
    }

    public int Level
    {
        get { return level; }
        set { level = value; }
    }

}
