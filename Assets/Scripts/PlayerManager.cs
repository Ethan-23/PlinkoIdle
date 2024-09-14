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
    [SerializeField] float cooldownDuration = 5f;
    [SerializeField] float coins = 0f;
    [SerializeField] float ballValue = 1f;
    [SerializeField] float ballSpeed = 50f;
    [SerializeField] int boardSize = 1;

    float cooldownTimer = 0f;
    PlayerInput playerInput;
    Dictionary<string, List<List<Vector2>>> ballSpawns = new Dictionary<string, List<List<Vector2>>>();
    List<string> values = new List<string> { "0.2x", "2x", "4x", "9x", "26x", "130x", "1000x" };
    // Start is called before the first frame update

    private void Awake()
    {
        // Initialize the input actions
        playerInput = new PlayerInput();
    }

    private void Update()
    {
        if(cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else
        {
            cooldownTimer = 0f;
        }
    }

    private void OnEnable()
    {
        // Enable the input actions
        playerInput.Enable();

        // Subscribe to the space bar (Jump) action
        playerInput.Gameplay.SpawnBall.performed += SpawnBall;
    }

    private void OnDisable()
    {
        // Disable the input actions
        playerInput.Disable();
    }

    void Start()
    {
        foreach (string val in values)
            ballSpawns.Add(val, new List<List<Vector2>>());
        GetPaths();
    }

    // Update is called once per frame
    private void SpawnBall(InputAction.CallbackContext context)
    {
        if (context.performed && cooldownTimer <= 0)
        {
            GameObject newBall = Instantiate(previewBall);
            cooldownTimer = cooldownDuration;
        } 
    }

    public string GetFilePath(string key)
    {
        return "/ImportantData/BoardTracks/Board" + (boardSize) + "/" + key + ".txt";
    }

    public void UpdatePaths()
    {
        ballSpawns.Clear();
        //GetPaths();
    }

    public void GetPaths()
    {
        List<string> keys = new List<string>();
        for (int i = 0; i < GetMaxMulti(); i++)
        {
            keys.Add(ballSpawns.Keys.ToList()[i]);
        }

        foreach (string key in keys)
        {
            //Debug.Log("GOT " + key);
            List<string> spawnPoints = File.ReadAllLines(Application.dataPath + GetFilePath(key)).ToList();
            List<List<Vector2>> spawn = new List<List<Vector2>>();
            foreach (string spawnPoint in spawnPoints)
            {
                List<Vector2> v = new List<Vector2>();
                List<string> cords = spawnPoint.Split('*').ToList();
                foreach (string c in cords)
                {
                    if (c == "")
                        continue;
                    List<string> vectors = c.Split(",").ToList();
                    v.Add(new Vector2(float.Parse(vectors[0]), float.Parse(vectors[1])));
                }
                spawn.Add(v);
            }
            ballSpawns[key] = spawn;
        }
    }

    public int GetMaxMulti()
    {
        int max = 0;
        if (boardSize == 1 || boardSize == 2 || boardSize == 3 || boardSize == 4)
            max = 3;
        else if (boardSize == 5 || boardSize == 6 || boardSize == 7 || boardSize == 8)
            max = 4;
        else if (boardSize == 9)
            max = 5;
        else if (boardSize == 10)
            max = 6;
        else if (boardSize == 11)
            max = 7;
        return max;
    }

    public List<Vector2> GetRandomBallPath()
    {
        int randMax = GetMaxMulti();
        string key = values[Random.Range(0, randMax)];
        return ballSpawns[key][Random.Range(0, ballSpawns[key].Count)];
    }

    public void AddCoins(float amount)
    {
        coins = FloatAdd(coins, amount);
    }

    public void RemoveCoins(float amount)
    {
        coins = FloatSub(coins, amount);
    }

    float FloatAdd(float num1, float num2)
    {
        return (float)Math.Round(num1 + num2, 2);
    }

    float FloatSub(float num1, float num2)
    {
        return (float)Math.Round(num1 - num2, 2);
    }

    public float GetCoins()
    {
        return (float)Math.Round(coins, 2);
    }

    public void AddValue(float amount)
    {
        ballValue = FloatAdd(ballValue, amount);
    }

    public float GetValue()
    {
        return (float)Math.Round(ballValue, 2);
    }

    public void AddSpeed(float amount)
    {
        ballSpeed = FloatAdd(ballSpeed, amount);
    }

    public float GetSpeed()
    {
        return (float)Math.Round(ballSpeed, 2);
    }

    public float GetCooldownDuration()
    {
        return (float)Math.Round(cooldownDuration, 2);
    }

    public float GetCooldownTimer()
    {
        return (float)Math.Round(cooldownTimer, 2);
    }

    public void SetBoardSize(int size)
    {
        boardSize = size;
    }

    public int GetBoardSize()
    {
        return boardSize;
    }
}
