using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropSlider : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] PlayerManager player;
    [SerializeField] Upgrades upgrades;
    [SerializeField] BallController ballController;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = player.GetBaseCooldownDuration();
        slider.value = slider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(player.GetCooldownTimer());
        slider.maxValue = upgrades.GetCooldown();
        slider.value = (upgrades.GetCooldown()) - ballController.GetCooldownTimer();
    }
}
