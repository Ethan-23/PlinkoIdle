using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class MultiplierBotFunctionality : MonoBehaviour
{
    [Header("BaseStats")]
    [SerializeField] bool active = true;
    [SerializeField] float speed = 10f;
    [SerializeField] int layer = 1;
    [SerializeField] int selectedLayer = 1;
    [SerializeField] bool moving = false;

    [SerializeField] MultiplierBot bot;

    PlayerManager playerManager;

    private void Awake()
    {
        bot = new MultiplierBot();
    }

    void Start()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
        UpdateBotPosition();
    }

    void Update()
    {
        if (!active)
        {
            StopAllCoroutines();
            UpdateBotPosition();
            return;
        }
            

        // Move to the new layer if it's different from the current one
        if (selectedLayer != layer)
        {
            StopAllCoroutines();
            layer = selectedLayer;
            UpdateBotPosition();
        }

        // Move only when not already moving
        if (!moving)
        {
            moving = true;
            float startX = 0.8f - (0.165f * (layer - 1));
            float endX = 2.2f + (0.165f * (layer - 1));
            float y = 2.7f - (0.32f * (layer - 1));

            Vector2 startPos = new Vector2(startX, y);
            Vector2 endPos = new Vector2(endX, y);

            // Start the movement coroutine
            StartCoroutine(Move(startPos, endPos));
        }
    }

    IEnumerator Move(Vector3 startPos, Vector3 endPos)
    {
        moving = true;

        float elapsedTime = 0f;

        while (elapsedTime < speed)
        {
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;

        float stopDuration = 5f; // SET TO UPGRADE - STOP DURATION TO SPEED HOW OFTEN IT CROSSES
        yield return new WaitForSeconds(stopDuration);
        if (active)
        {
            StartCoroutine(Move(endPos, startPos));
        }
        else
        {
            moving = false;
            StopAllCoroutines();
        }
    }

    private void UpdateBotPosition()
    {
        float startX = 0.8f - (0.165f * (layer - 1));
        float y = 2.7f - (0.32f * (layer - 1));
        transform.position = new Vector3(startX, y, 0); // Update the bot's position based on the layer
    }

    public void ToggleActive()
    {
        active = !active;
    }

    public MultiplierBot GetBotData()
    {
        return bot;
    }

    public void SetBotData(MultiplierBot multiplierBot)
    {
        bot = multiplierBot;
    }
}
