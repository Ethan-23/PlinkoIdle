using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    [SerializeField] float cooldownTimer = 0f;
    [SerializeField] Slider autoSlider;
    [SerializeField] float autoCooldownTimer = 0f;
    [SerializeField] float autoDropBaseCooldown = 4f;
    [SerializeField] PlayerManager playerManager;
    Upgrades upgrades;
    PlayerInput playerInput;
    Dictionary<string, List<List<Vector2>>> ballSpawns = new Dictionary<string, List<List<Vector2>>>();
    List<string> values = new List<string> { "0.2x", "2x", "4x", "9x", "26x", "130x", "1000x" };
    // Start is called before the first frame update

    private void Awake()
    {
        playerInput = new PlayerInput();
        foreach (string val in values)
            ballSpawns.Add(val, new List<List<Vector2>>());
    }

    private void Start()
    {
        GetPaths();
        upgrades = playerManager.GetUpgrades();
        autoCooldownTimer = playerManager.GetBaseAutoCooldownDuration();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else
        {
            cooldownTimer = 0f;
        }

        if (playerManager.GetAutoDrop() == true && autoCooldownTimer > 0f)
        {
            autoCooldownTimer -= Time.deltaTime;
        }
        else if(playerManager.GetAutoDrop() == true)
        {
            SpawnBall();
        }
        else
        {
            autoCooldownTimer = upgrades.GetAutoCooldown();
        }
    }

    private void OnEnable()
    {
        // Enable the input actions
        playerInput.Enable();

        // Subscribe to the space bar (Jump) action
        playerInput.Gameplay.SpawnBall.performed += SpawnBallAction;
    }

    private void OnDisable()
    {
        // Disable the input actions
        playerInput.Disable();
    }

    // Update is called once per frame
    private void SpawnBallAction(InputAction.CallbackContext context)
    {
        if (context.performed && cooldownTimer <= 0)
        {
            playerManager.SetAutoDrop(false);
            if (autoSlider.IsActive())
                autoSlider.gameObject.SetActive(false);
            SpawnBall();
        }
    }

    void SpawnBall()
    {
        Instantiate(upgrades.GetSpawningBall());

        cooldownTimer = upgrades.GetCooldown();
        autoCooldownTimer = upgrades.GetAutoCooldown();
    }

    public string GetFilePath(string key)
    {
        return "/ImportantData/BoardTracks/Board" + (playerManager.GetBoardSize()) + "/" + key + ".txt";
    }

    public void UpdatePaths()
    {
        ballSpawns.Clear();
        foreach (string val in values)
            ballSpawns.Add(val, new List<List<Vector2>>());
        GetPaths();
    }

    public void GetPaths()
    {
        //Debug.Log("Paths Updated");
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
        if (playerManager.GetBoardSize() >= 1 && playerManager.GetBoardSize() <= 4)
            max = 3;
        else if (playerManager.GetBoardSize() >= 5 && playerManager.GetBoardSize() <= 8)
            max = 4;
        else if (playerManager.GetBoardSize() == 9)
            max = 5;
        else if (playerManager.GetBoardSize() == 10)
            max = 6;
        else if (playerManager.GetBoardSize() == 11)
            max = 7;
        return max;
    }

    public List<Vector2> GetRandomBallPath()
    {
        int randMax = GetMaxMulti();
        string key = values[Random.Range(0, randMax)];
        return ballSpawns[key][Random.Range(0, ballSpawns[key].Count)];
    }

    public float GetCooldownTimer()
    {
        return cooldownTimer;
    }

    public float GetAutoCooldownTimer()
    {
        return autoCooldownTimer;
    }


}
