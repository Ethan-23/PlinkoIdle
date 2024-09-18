using System;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] PlayerManager player;
    [SerializeField] TextMeshProUGUI coinText;

    [SerializeField] TextMeshProUGUI ballCooldownCost;
    [SerializeField] TextMeshProUGUI ballCooldownAmount;
    [SerializeField] TextMeshProUGUI specialBallCost;
    [SerializeField] TextMeshProUGUI specialBallAmount;
    [SerializeField] TextMeshProUGUI bonkerMultiCost;
    [SerializeField] TextMeshProUGUI bonkerMultiAmount;
    [SerializeField] TextMeshProUGUI glowingBonkerCost;
    [SerializeField] TextMeshProUGUI glowingBonkerAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {
        coinText.text = "Coins: " + player.GetCoins().ToString();
        ballCooldownCost.text = "Cost: " + CalculateBallCooldownCost().ToString();
        ballCooldownAmount.text = player.GetCooldownUpgrade().ToString();
        specialBallCost.text = "Cost: " + CalculateSpecialBallCost().ToString();
        specialBallAmount.text = player.GetSpecialBallUpgrade().ToString();
        bonkerMultiCost.text = "Cost: " + CalculateBonkerValue().ToString();
        bonkerMultiAmount.text = player.GetBonkerUpgrade().ToString();
        glowingBonkerCost.text = "Cost: " + CalculateGlowingBonkers().ToString();
        glowingBonkerAmount.text = player.GetGlowingBonkerUpgrade().ToString();
    }

    public float CalculateBallCooldownCost()
    {
        return (float)Math.Round(10 * Math.Pow(1.07f, player.GetCooldownUpgrade()), 2);
    }

    public void PurchaseBallCooldown()
    {
        if (player.GetCoins() < CalculateBallCooldownCost())
            return;
        player.RemoveCoins(CalculateBallCooldownCost());
        player.AddCooldownUpgrade(1);
        UpdateText();
    }

    public float CalculateSpecialBallCost()
    {
        return (float)Math.Round(Math.Pow(player.GetSpecialBallUpgrade(), 1.25), 2);
    }

    public void PurchaseSpecialBall()
    {
        if (player.GetCoins() < CalculateSpecialBallCost())
            return;
        player.RemoveCoins(CalculateSpecialBallCost());
        player.AddSpecialBallUpgrade(1);
        UpdateText();
    }

    public float CalculateBonkerValue()
    {
        return (float)Math.Round(Math.Pow(player.GetBonkerUpgrade(), 1.25), 2);
    }

    public void PurchaseBonkerValue()
    {
        if (player.GetCoins() < CalculateBonkerValue())
            return;
        player.RemoveCoins(CalculateBonkerValue());
        player.AddBonkerUpgrade(1);
        UpdateText();
    }
    
    public float CalculateGlowingBonkers()
    {
        return (float)Math.Round(Math.Pow(player.GetGlowingBonkerUpgrade(), 1.25), 2);
    }

    public void PurchaseGlowingBonkers()
    {
        if (player.GetCoins() < CalculateGlowingBonkers())
            return;
        player.RemoveCoins(CalculateGlowingBonkers());
        player.AddGlowingBonkerUpgrade(1);
        UpdateText();
    }
}
