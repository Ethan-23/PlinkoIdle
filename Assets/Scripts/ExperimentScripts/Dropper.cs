using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.Rendering;

public class Dropper : MonoBehaviour
{
    [SerializeField] List<GameObject> boards = new List<GameObject>();
    [SerializeField] int boardCount = 1;
    [SerializeField] float start = 0;
    [SerializeField] float end = 0;
    [SerializeField] PlayerManager player;
    [SerializeField] bool right = true;
    [SerializeField] GameObject ball;
    [SerializeField] GameObject previewBall;
    [SerializeField] Dictionary<string, List<List<Vector2>>> ballSpawns = new Dictionary<string, List<List<Vector2>>>();
    List<string> values = new List<string> { "0.2x", "2x", "4x", "9x", "26x", "130x", "1000x" };

    int times;
    float ballCount = 0;
    /*int counter = 0;*/
    List<Vector2> xCords = new List<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        times = 0;
        //Gets pathways from files COMMENT OUT TO RETEST BALLS
        //GetPaths();
    }

    IEnumerator ToggleBallSpawnCoroutine()
    {
            yield return new WaitForSeconds(0.0001f);

            transform.position = right ? new Vector3(transform.position.x + 0.0001f, transform.position.y, transform.position.z) : transform.position = new Vector3(transform.position.x - 0.0001f, transform.position.y, transform.position.z); ;

            if (right && transform.position.x > end)
            {
                right = false;
                StopCoroutine(ToggleBallSpawnCoroutine());
            }
            /*else if (transform.position.x < start)
            {
                right = true;
                times++;
                StopCoroutine(ToggleBallSpawnCoroutine());
            }*/
            else
            {
                ballCount++;
                GameObject newBall = Instantiate(ball);
                newBall.transform.position = new Vector3(transform.position.x, 3.2f, transform.position.z);
                StartCoroutine(ToggleBallSpawnCoroutine());
            }
    }

    // Update is called once per frame
    void Update()
    {

        /*if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(boardCount < 11)
                boardCount++;
            player.UpdatePaths();
            player.SetBoardSize(boardCount);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (boardCount > 0)
                boardCount--;
            player.UpdatePaths();
            player.SetBoardSize(boardCount);
        }*/

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Logging Disabled for now");
            //Prints successful test paths to files
            /*Debug.Log("Test Printout");
            StartCoroutine(PrintToFileSpawns());*/
        }
            

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Testing Disabled for now");
            //Runs Test Balls for files
            /*transform.position = new Vector3(start, transform.position.y);
            ClearTest();
            //Clears old files
            *//*foreach (string val in values)
                ClearFiles(val);*//*
            StartCoroutine(ToggleBallSpawnCoroutine());*/
        }
    }

    public void AddPath(string tag, List<Vector2> loc)
    {
        Debug.Log("Added " + tag);
        if (ballSpawns.ContainsKey(tag))
        {
            List<List<Vector2>> temp = ballSpawns[tag];
            temp.Add(loc);
            ballSpawns[tag] = temp;
        }
        else
        {
            List<List<Vector2>> temp = new List<List<Vector2>>();
            temp.Add(loc);
            ballSpawns.Add(tag, temp);
        }
    }

    IEnumerator PrintToFileSpawns()
    {
        foreach (KeyValuePair<string, List<List<Vector2>>> kvp in ballSpawns)
        {
            File.WriteAllText(Application.dataPath + GetFilePath(kvp.Key), "");

            foreach (List<Vector2> v in kvp.Value)
            {
                yield return new WaitForSeconds(0.05f);
                Debug.Log("Printing...!");
                foreach (Vector2 v2 in v)
                {
                    File.AppendAllText(Application.dataPath + GetFilePath(kvp.Key), v2.x + "," + v2.y + "*");
                }
                File.AppendAllText(Application.dataPath + GetFilePath(kvp.Key), "\n");
            }

        }
    }

    public string GetFilePath(string key)
    {
        return "/ImportantData/BoardTracks/Board" + (boardCount) + "/" + key + ".txt";
    }

    public void ClearFiles(string key)
    {
        File.WriteAllText(Application.dataPath + GetFilePath(key), "");
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
            if (ballSpawns.ContainsKey(key))
            {
                ballSpawns[key] = spawn;
            }
            else
            {
                ballSpawns.Add(key, spawn);
            }
            
        }
    }

    public List<Vector2> GetRandomBallPath()
    {
        //Get keys from dict not list UPDATE THIS NOOB
        string key = values[Random.Range(0, values.Count)];
        return ballSpawns[key][Random.Range(0, ballSpawns[key].Count)];
    }

    public void ClearTest()
    {
        ballCount = 0;
        times = 0;
        ballSpawns.Clear();
    }
}
