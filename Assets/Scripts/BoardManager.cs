using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] List<GameObject> boards = new List<GameObject>();
    PlayerManager player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerManager>();
        Instantiate(boards[player.GetBoardSize() - 1]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
