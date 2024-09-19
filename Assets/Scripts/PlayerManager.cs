using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    //Base stats 
    [Header("BaseStats")]
    [SerializeField] float baseCooldownDuration = 2f;
    [SerializeField] float BaseAutoCooldownDuration = 4f;
    [SerializeField] float baseBallSpeed = 50f;
    [SerializeField] bool autoDrop = false;
    

    [SerializeField] Player player;

    [Header("Objects")]
    [SerializeField] GameObject previewBall;
    [SerializeField] Upgrades upgrades;
    [SerializeField] BallController ballController;

    // Start is called before the first frame update

    private void Awake()
    {
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
    public int GetSpeed()
    {
        return player.BallSpeedUpgrade;
    }
    public void SetSpeed(int amount)
    {
        player.BallSpeedUpgrade = amount;
    }
    public void AddSpeed(int amount)
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
        player.BoardSize = size;
    }
    public void AddBoardSize(int size)
    {
        player.BoardSize += size;
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

}
