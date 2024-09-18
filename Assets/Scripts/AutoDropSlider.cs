using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoDropSlider : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] PlayerManager player;
    [SerializeField] Upgrades upgrades;
    [SerializeField] BallController ballController;
    // Start is called before the first frame update
    void Start()
    {
        slider.gameObject.SetActive(false);
        slider = GetComponent<Slider>();
        slider.maxValue = player.GetBaseAutoCooldownDuration();
        slider.value = slider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        slider.maxValue = upgrades.GetAutoCooldown();
        slider.value = upgrades.GetAutoCooldown() - ballController.GetAutoCooldownTimer();
    }
}
