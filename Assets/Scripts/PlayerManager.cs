using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject previewBall;

    [Header("BasePlayerStats")]
    [SerializeField] float cooldownDuration = 5f;
    [SerializeField] float coins = 0f;
    [SerializeField] float ballValue = 1f;
    [SerializeField] float ballSpeed = 50f;

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

    public void GetPaths()
    {
        List<string> keys = new List<string>();
        foreach (string key in ballSpawns.Keys)
        {
            keys.Add(key);
        }

        foreach (string key in keys)
        {
            //Debug.Log("GOT " + key);
            List<string> spawnPoints = File.ReadAllLines(Application.dataPath + "/ImportantData/BallTracks/" + key + ".txt").ToList();
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
    public List<Vector2> GetRandomBallPath()
    {
        string key = values[Random.Range(0, values.Count)];
        return ballSpawns[key][Random.Range(0, ballSpawns[key].Count)];
    }

    public void AddCoins(float amount)
    {
        coins += amount;
    }

    public float GetCoins()
    {
        return coins;
    }

    public void AddValue(float amount)
    {
        ballValue += amount;
    }

    public float GetValue()
    {
        return ballValue;
    }

    public void AddSpeed(float amount)
    {
        ballSpeed += amount;
    }

    public float GetSpeed()
    {
        return ballSpeed;
    }

    public float GetCooldownDuration()
    {
        return cooldownDuration;
    }

    public float GetCooldownTimer()
    {
        return cooldownTimer;
    }
}
