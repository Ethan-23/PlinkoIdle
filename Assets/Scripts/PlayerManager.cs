using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    //Base stats 
    [Header("BaseStats")]
    [SerializeField] float baseCooldownDuration = 2f;
    [SerializeField] float BaseAutoCooldownDuration = 4f;
    [SerializeField] float baseBallSpeed = 50f;
    [SerializeField] bool autoDrop = false;

    [Header("Player Stats")]
    [SerializeField] Player player;

    [Header("Objects")]
    [SerializeField] GameObject previewBall;
    [SerializeField] GameObject dropBot;
    [SerializeField] GameObject multiplierBot;
    Upgrades upgrades;
    BallController ballController;
    BoardManager boardManager;

    // Start is called before the first frame update

    private void Awake()
    {
        upgrades = GetComponent<Upgrades>();
        ballController = GameObject.Find("BallController").GetComponent<BallController>();
        boardManager = GameObject.Find("BoardManager").GetComponent<BoardManager>();
        player = new Player();
        player.Coins = 0;
    }

    public void Start()
    {
        LoadPlayerData();
    }

    public void OnApplicationQuit()
    {
        SavePlayerData();
    }

    public void SavePlayerData()
    {
        if (player == null)
        {
            Debug.LogWarning("Player instance is null, cannot save data.");
            return;
        }

        player.DropBotData.Clear();
        player.MultiplierBotData.Clear();

        foreach (GameObject bot in player.BotList)
        {
            if(bot.CompareTag("DropBot"))
            {
                player.DropBotData.Add(bot.GetComponent<DropBotFunctionality>().GetBotData());
            }
            else if (bot.CompareTag("MultiplierBot"))
            {
                player.MultiplierBotData.Add(bot.GetComponent<MultiplierBotFunctionality>().GetBotData());
            }
        }

        try
        {
            string json = JsonUtility.ToJson(player);
            string path = Application.persistentDataPath + "/playerData.json";
            System.IO.File.WriteAllText(path, json);
            Debug.Log("Player data saved to " + path);
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to save player data: " + ex.Message);
        }
    }

    public void LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/playerData.json";

        if (!System.IO.File.Exists(path))
        {
            Debug.LogWarning("Player data file does not exist. Initializing default player.");
            player = new Player(); // Initialize default player if file is missing
            return;
        }

        try
        {
            string json = System.IO.File.ReadAllText(path);
            player = JsonUtility.FromJson<Player>(json);

            if (player == null)
            {
                Debug.LogWarning("Failed to deserialize player data. Initializing default player.");
                player = new Player(); // Initialize default player if deserialization fails
            }
            else
            {
                Debug.Log("Player data loaded from " + path);
                player.BotList.Clear();

                // Iterate through the bot data list
                foreach (DropBot botData in player.DropBotData)
                {
                    LoadBot(botData);
                }
                foreach (MultiplierBot botData in player.MultiplierBotData)
                {
                    LoadBot(botData);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to load player data: " + ex.Message);
            player = new Player(); // Initialize default player on error
        }
    }


    //Coin Functions
    public float GetCoins()
    {
        return (float)Math.Round(player.Coins, 2);
    }
    public void AddCoins(float amount)
    {
        player.Coins = FloatAdd(player.Coins, amount);
    }
    public void RemoveCoins(float amount)
    {
        player.Coins = FloatSub(player.Coins, amount);
    }
    public void SetCoins(float amount)
    {
        player.Coins = amount;
    }

    //Diamond Functions
    public float GetDiamonds()
    {
        return (float)Math.Round(player.Diamonds, 2);
    }
    public void AddDiamonds(float amount)
    {
        player.Diamonds = FloatAdd(player.Diamonds, amount);
    }
    public void SetDiamonds(float amount)
    {
        player.Diamonds = amount;
    }

    public int GetBallValueUpgrade()
    {
        return player.BallValueUpgrade;
    }
    public void SetBallValueUpgrade(int value)
    {
        player.BallValueUpgrade = value;
    }
    public void AddBallValueUpgrade(int value)
    {
        player.BallValueUpgrade += value;
    }


    //Speed Functions
    public int GetSpeedUpgrade()
    {
        return player.BallSpeedUpgrade;
    }
    public void SetSpeedUpgrade(int amount)
    {
        player.BallSpeedUpgrade = amount;
    }
    public void AddSpeedUpgrade(int amount)
    {
        player.BallSpeedUpgrade += amount;
    }

    //BonkerValue Functions
    public int GetBonkerUpgrade()
    {
        return player.BonkerValueUpgrade;
    }
    public void AddBonkerUpgrade(int amount)
    {
        player.BonkerValueUpgrade += amount;
    }
    public void SetBonkerUpgrade(int amount)
    {
        player.BonkerValueUpgrade = amount;
    }

    //GlowingBonker Functions
    public int GetGlowingBonkerUpgrade()
    {
        return player.GlowingBonkersUpgrade;
    }
    public void AddGlowingBonkerUpgrade(int amount)
    {
        player.GlowingBonkersUpgrade += amount;
    }
    public void SetGlowingBonkerUpgrade(int amount)
    {
        player.GlowingBonkersUpgrade = amount;
    }


    //SpecialBall Functions
    public int GetSpecialBallUpgrade()
    {
        return player.SpecialBallUpgrade;
    }
    public void AddSpecialBallUpgrade(int amount)
    {
        player.SpecialBallUpgrade += amount;
    }
    public void SetSpecialBallUpgrade(int amount)
    {
        player.SpecialBallUpgrade = amount;
    }

    //Cooldown Functions
    public int GetCooldownUpgrade()
    {
        return player.CooldownUpgrade;
    }
    public void AddCooldownUpgrade(int amount)
    {
        player.CooldownUpgrade += amount;
    }
    public void SetCooldownUpgrade(int amount)
    {
        player.CooldownUpgrade = amount;
    }

    //Auto Functions
    public int GetAutoCooldownUpgrade()
    {
        return player.AutoCooldownUpgrade;
    }
    public void AddAutoCooldownUpgrade(int amount)
    {
        player.AutoCooldownUpgrade += amount;
    }
    public void SetAutoCooldownUpgrade(int amount)
    {
        player.AutoCooldownUpgrade = amount;
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
    
    public float GetBaseAutoCooldownDuration()
    {
        return BaseAutoCooldownDuration;
    }

    public bool GetAutoDrop()
    {
        return autoDrop;
    }

    public void SetAutoDrop(bool mode)
    {
        autoDrop = mode;
    }

    //Board Size
    public void SetBoardSize(int size)
    {
        if (size > 11)
            return;
        player.BoardSize = size;
        boardManager.ChangeBoard();
        ballController.UpdatePaths();
        PrestigeReset();
    }
    public void AddBoardSize(int size)
    {
        if (player.BoardSize >= 11)
            return;
        player.BoardSize += size;
        boardManager.ChangeBoard();
        ballController.UpdatePaths();
        PrestigeReset();
    }
    public int GetBoardSize()
    {
        return player.BoardSize;
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

    public void LoadBot(DropBot botData)
    {
        GameObject botInstance = Instantiate(dropBot, new Vector3(1.9f, 3.2f, 0), new Quaternion(0, 0, 0, 0));

        // Get the DropBot component on the new instance and set its data
        botInstance.GetComponent<DropBotFunctionality>().SetBotData(botData);
        player.BotList.Add(botInstance);
    }

    public void LoadBot(MultiplierBot botData)
    {
        GameObject botInstance = Instantiate(multiplierBot);
        // Get the DropBot component on the new instance and set its data
        botInstance.GetComponent<MultiplierBotFunctionality>().SetBotData(botData);
        player.BotList.Add(botInstance);
    }

    public void AddDropBot()
    {
        //List<GameObject> botlist = player.Botlist;
        //botlist.Add(gameObject);
        //player.Botlist = botlist;
        GameObject tempdropBot = Instantiate(dropBot, new Vector3(1.9f, 3.2f, 0), new Quaternion(0, 0, 0, 0));
        player.BotList.Add(tempdropBot);
    }

    public void AddMultiplierBot()
    {
        //List<GameObject> botlist = player.Botlist;
        //botlist.Add(gameObject);
        //player.Botlist = botlist;
        GameObject tempMultiplierBot = Instantiate(multiplierBot);
        player.BotList.Add(tempMultiplierBot);
    }

    public void PrestigeReset()
    {
        player.Prestige += 1;
        SetCoins(0);
        SetBallValueUpgrade(0);
        //SetSpeedUpgrade(0); if I ever get this to work
        SetBonkerUpgrade(0);
        SetGlowingBonkerUpgrade(0);
        SetSpecialBallUpgrade(0);
        SetCooldownUpgrade(0);
        SetAutoCooldownUpgrade(0);
    }
}
