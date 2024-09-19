using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PreviewBall : MonoBehaviour
{
    List<Vector2> path = new List<Vector2>();
    int currentIndex = 0;

    float speed;
    [SerializeField] float baseBallValue = 1;

    [Header("Data Recording")]
    int bonkersHit = 0;
    int glowingBonkersHit = 0;

    PlayerManager playerManager;
    Upgrades upgrades;
    UIScoreDisplay scoreDisplay;
    List<string> multis = new List<string> { "0.2x", "2x", "4x", "9x", "26x", "130x", "1000x" };
    
    // Start is called before the first frame update
    void Start()
    {
        //Get values of outside variables
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
        upgrades = playerManager.gameObject.GetComponent<Upgrades>();
        scoreDisplay = GameObject.Find("ScoreTextDisplay").GetComponent<UIScoreDisplay>();
        speed = playerManager.GetBaseSpeed();
        path = playerManager.GetBallController().GetRandomBallPath();
        
        //Start position
        transform.position = path[0];
        /*StartCoroutine(FollowPath());*/
    }

    public void SetBallValue(float amount)
    {
        baseBallValue = amount;
    }
    public float GetBallValue()
    {
        return baseBallValue;
    }

    /*IEnumerator FollowPath()
    {
        if (currentIndex < path.Count)
        {
            // Move ball to the next recorded position
            transform.position = Vector3.Lerp(transform.position, path[currentIndex], Time.deltaTime * replaySpeed);

            // Check if the ball is close enough to the next point, then move to the next one

            if (Vector3.Distance(transform.position, path[currentIndex]) < 0.01f)
            {
                currentIndex++;
            }
            yield return new WaitForSeconds(0f);
            StartCoroutine(FollowPath());
        }
        StopCoroutine(FollowPath());
        
    }*/

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentIndex < path.Count)
        {
            // Move ball to the next recorded position
            transform.position = Vector3.Lerp(transform.position, path[currentIndex], Time.fixedDeltaTime * speed);

            // Check if the ball is close enough to the next point, then move to the next one
            if (Vector3.Distance(transform.position, path[currentIndex]) < 0.01f)
            {
                currentIndex++;
            }
        }
    }

    void displayData(float ballValue, float finalValue, float multi)
    {
        float bonkerVal = bonkersHit * upgrades.GetBonkerValue(baseBallValue, false);
        float glowingBonkerVal = glowingBonkersHit * upgrades.GetBonkerValue(baseBallValue, true);
        float totalValue = finalValue + glowingBonkerVal + bonkerVal;
        Debug.Log("Ball Data: BaseValue = " + ballValue + " | Multiplier = " + multi + " | Bonkers Hit = " + bonkersHit + " | Bonker Value = " + bonkerVal + " | GlowingBonkers Hit = " + glowingBonkersHit + " | Glowing Bonker Value = " + glowingBonkerVal + " | Final Value = " + finalValue + " | Total = " + Math.Round(totalValue, 2));
    }

    public void HitBonker(bool glowing)
    {
        if(glowing)
        {
            glowingBonkersHit += 1;
        }
        else
        {
            bonkersHit += 1;
        }
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(multis.Contains(collider.tag)) {
            float ballValue = baseBallValue * upgrades.GetBallValue();
            float multi = float.Parse(collider.tag.Substring(0, collider.tag.Length - 1));
            float amount = ballValue * multi;
            displayData(ballValue, amount, multi);
            playerManager.AddCoins(amount);
            scoreDisplay.ShowPointGain(transform.position, amount);
            Destroy(gameObject);
        }
        
    }
}
