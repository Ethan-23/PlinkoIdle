using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] List<GameObject> boards = new List<GameObject>();
    GameObject mainCamera;
    PlayerManager player;

    float cameraSize = 2f;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
        ChangeBoard();
    }

    public void ChangeBoard()
    {
        foreach (GameObject temp in GameObject.FindGameObjectsWithTag("Board"))
        {
            Destroy(temp);
        }
        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Ball"))
        {
            Destroy(ball);
        }
        
        /*if (player.GetBoardSize() == 1)
        {*/
            //mainCamera.transform.position = new Vector3(0.7f, 1.8f, mainCamera.transform.position.z);
        mainCamera.GetComponent<Camera>().orthographicSize = cameraSize + (float)(0.1 * (player.GetBoardSize() - 1));
        mainCamera.transform.position = new Vector3(0.7f - (0.025f * player.GetBoardSize()) /*+ (float)(0.15f * (player.GetBoardSize() - 1))*/, 1.8f - (0.15f * player.GetBoardSize()), -5f);
        //}
        Instantiate(boards[player.GetBoardSize() - 1]);
    }
}
