using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class DiamondShopManager : MonoBehaviour
{
    [SerializeField] PlayerManager player;
    [SerializeField] TextMeshProUGUI diamondText;

    [SerializeField] TextMeshProUGUI dropBotCost;
    [SerializeField] TextMeshProUGUI dropBotAmount;
    [SerializeField] TextMeshProUGUI multiplierBotCost;
    [SerializeField] TextMeshProUGUI multiplierBotAmount;
    // Start is called before the first frame update
    void Start()
    {
        //Run 1 sec after
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {
        diamondText.text = "Diamonds: " + player.GetDiamonds().ToString();
        dropBotCost.text = "Cost: " + CalculateDropBotCost().ToString();
        dropBotCost.text = "Cost: " + CalculateMultiplierBotCost().ToString();
    }

    public float CalculateDropBotCost()
    {
        return (float)Math.Round(10 * Math.Pow(1.07f, 1), 2);
    }

    public void PurchaseDropBot()
    {
        if (player.GetDiamonds() < CalculateDropBotCost())
            return;
        player.SetDiamonds(player.GetDiamonds() - CalculateDropBotCost());
        player.AddDropBot();
        UpdateText();
    }
    
    public float CalculateMultiplierBotCost()
    {
        return (float)Math.Round(10 * Math.Pow(1.07f, 1), 2);
    }

    public void PurchaseMultiplierBot()
    {
        if (player.GetDiamonds() < CalculateMultiplierBotCost())
            return;
        player.SetDiamonds(player.GetDiamonds() - CalculateMultiplierBotCost());
        player.AddMultiplierBot();
        UpdateText();
    }
}
