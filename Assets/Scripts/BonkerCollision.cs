using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonkerCollision : MonoBehaviour
{
    PlayerManager playerManager;
    Upgrades upgrades;
    UIScoreDisplay scoreDisplay;
    [SerializeField] bool glowing = false;
    
    public void Start()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
        upgrades = playerManager.GetUpgrades();
        scoreDisplay = GameObject.Find("ScoreTextDisplay").GetComponent<UIScoreDisplay>();
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag.Equals("Ball"))
        {
            GameObject ball = collision.gameObject;
            transform.gameObject.GetComponent<ParticleSystem>().Play();
            float gain = upgrades.GetBonkerValue(ball.GetComponent<PreviewBall>().GetBallValue(), glowing);
            collision.gameObject.GetComponent<PreviewBall>().HitBonker(glowing);
            upgrades.RemoveBonker(gameObject);
            playerManager.AddCoins(gain);
            scoreDisplay.ShowPointGain(ball.transform.position, gain);

            
        }
    }

    public void SetGlowing(bool isGlowing)
    {
        glowing = isGlowing;
        if (!isGlowing)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.magenta;
        }
    }

    public bool GetGlowing()
    {
        return glowing;
    }
}
