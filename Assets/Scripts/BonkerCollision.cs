using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonkerCollision : MonoBehaviour
{
    PlayerManager playerManager;
    Upgrades upgrades;
    [SerializeField] bool glowing = false;

    public void Awake()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
        upgrades = playerManager.GetUpgrades();
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag.Equals("Ball"))
        {
            GameObject ball = collision.gameObject;
            transform.gameObject.GetComponent<ParticleSystem>().Play();
            upgrades.RemoveBonker(gameObject);
            playerManager.AddCoins(upgrades.GetBonkerValue(ball.GetComponent<PreviewBall>().GetBallValue(), glowing));
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
