using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject ball;
    [SerializeField] GameObject uncommonBall;
    [SerializeField] GameObject rareBall;
    [SerializeField] GameObject epicBall;
    [SerializeField] GameObject mythicalBall;
    [SerializeField] GameObject legendaryBall;
    PlayerManager playerManager;
    [SerializeField] float glowingTimer = 300f;
    List<GameObject> glowing = new List<GameObject>();

    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        if(playerManager.GetGlowingBonkerUpgrade() >= 1)
        {
            glowingTimer = 300f;
            BonkerGlow(playerManager.GetGlowingBonkerUpgrade() / 10 + 1);
            //ResetBonkers();
        }
    }

    // Update is called once per frame
    void Update()
    {
        BonkerCooldown();
    }



    public GameObject GetSpawningBall()
    {
        GameObject spawningBall = ball;
        //Calculation to find ball
        int upgrade = playerManager.GetSpecialBallUpgrade();
        int uncommonChance = Random.Range(1, 101);
        int rareChance = Random.Range(1, 251);
        int epicChance = Random.Range(1, 501);
        int mythicChance = Random.Range(1, 751);
        int legendaryChance = Random.Range(1, 1001);
        if (upgrade >= uncommonChance)
            spawningBall = uncommonBall;
        if (upgrade >= rareChance)
            spawningBall = rareBall;
        if (upgrade >= epicChance)
            spawningBall = epicBall;
        if (upgrade >= mythicChance)
            spawningBall = mythicalBall;
        if (upgrade >= legendaryChance)
            spawningBall = legendaryBall;
        return spawningBall;
    }

    public float GetCooldown()
    {
        return playerManager.GetBaseCooldownDuration() - (playerManager.GetCooldownUpgrade() * 0.01f);
    }

    void BonkerCooldown()
    {
        if (playerManager.GetGlowingBonkerUpgrade() < 1)
            return;
        if (glowingTimer > 0f)
        {
            glowingTimer -= Time.deltaTime;
        }
        else
        {
            glowingTimer = 300f;
            BonkerGlow(playerManager.GetGlowingBonkerUpgrade() / 10 + 1);
            //ResetBonkers();
        }
    }

    public float GetBonkerValue(float ballValue, bool isGlowing)
    {
        int level = (playerManager.GetBonkerUpgrade() + 1);
        if (isGlowing)
        {
            return (level * 0.01f + GetGlowingMulti()) * ballValue;
        }
        return (0.1f + (level * 0.001f)) * ballValue;
    }

    float GetGlowingMulti()
    {
        return 0.001f * playerManager.GetGlowingBonkerUpgrade();
    }

    void BonkerGlow(int amount)
    {
        //Every 10 levels add another bonker and increase multiplier every level

        //Old system of bonkers staying until timer changes set bonkers
        //List<GameObject> bonkers = GameObject.FindGameObjectsWithTag("Bonk").ToList();
        List<GameObject> bonkers = GetNonGlowingBonkers();
        int max = bonkers.Count;
        if (max == 0)
            return;
        for(int i = 0; i < amount; i++)
        {
            GameObject glowingBonker = bonkers[Random.Range(0, max)];
            glowingBonker.GetComponent<BonkerCollision>().SetGlowing(true);
            glowing.Add(glowingBonker);
            max--;
        }
    }

    List<GameObject> GetNonGlowingBonkers()
    {
        List<GameObject> bonkers = GameObject.FindGameObjectsWithTag("Bonk").ToList();
        bonkers.RemoveAll(i => i.GetComponent<BonkerCollision>().GetGlowing() == true);
        /*foreach (GameObject bonker in bonkers)
        {
            if(bonker.GetComponent<BonkerCollision>().GetGlowing())
                bonkers.Remove(bonker);
        }*/
        return bonkers;
    }

    //Old version bonkers stay until cooldown resets
    /*void ResetBonkers()
    {
        foreach (GameObject bonker in glowing)
        {
            bonker.GetComponent<BonkerCollision>().SetGlowing(false);
        }
        BonkerGlow(playerManager.GetGlowingBonkerUpgrade()/10 + 1);
    }*/

    public void RemoveBonker(GameObject bonker)
    {
        if(glowing.Contains(bonker))
            glowing.Remove(bonker);
        bonker.GetComponent<BonkerCollision>().SetGlowing(false);
    }
}
