using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject previewBall;
    Dictionary<string, List<List<Vector2>>> ballSpawns = new Dictionary<string, List<List<Vector2>>>();
    List<string> values = new List<string> { "0.2x", "2x", "4x", "9x", "26x", "130x", "1000x" };
    // Start is called before the first frame update
    void Start()
    {
        foreach (string val in values)
            ballSpawns.Add(val, new List<List<Vector2>>());
        GetPaths();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newBall = Instantiate(previewBall);
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
}
