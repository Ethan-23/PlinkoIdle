using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropBotFunctionality : MonoBehaviour
{
    [Header("BaseStats")]
    [SerializeField] bool active = true;
    [SerializeField] float cooldown = 7f;

    [SerializeField] DropBot bot;

    float timer = 0f;

    List<Vector2> path = new List<Vector2>();
    PlayerManager playerManager;

    // Start is called before the first frame update
    private void Awake()
    {
        bot = new DropBot();
    }

    void Start()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
        SpawnRobotBall();
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
            return;
        timer += Time.deltaTime;
        if (timer > cooldown)
        {
            SpawnRobotBall();
        }
    }

    void SpawnRobotBall()
    {
        timer = 0f;
        path = playerManager.GetBallController().GetRandomBallPath();
        StartCoroutine(MoveTo(path[0]));
    }

    IEnumerator MoveTo(Vector2 location)
    {
        Vector2 startPos = transform.position;

        float duration = cooldown;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {

            transform.position = Vector2.Lerp(startPos, location, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.position = location;
        playerManager.GetBallController().SpawnRobotBall(path);

    }

    public void ToggleActive()
    {
        active = !active;
    }

    public DropBot GetBotData()
    {
        return bot;
    }

    public void SetBotData(DropBot dropBot)
    {
        bot = dropBot;
    }


}
