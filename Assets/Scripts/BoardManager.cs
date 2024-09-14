using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] List<GameObject> boards = new List<GameObject>();
    GameObject mainCamera;
    PlayerManager player;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
        if(player.GetBoardSize() == 1)
        {
            mainCamera.transform.position = new Vector3 (mainCamera.transform.position.x, 1.4f, mainCamera.transform.position.z);
            mainCamera.GetComponent<Camera>().orthographicSize = 2;
        }
        Instantiate(boards[player.GetBoardSize() - 1]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
