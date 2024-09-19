using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoToggle : MonoBehaviour
{
    [SerializeField] PlayerManager player;
    [SerializeField] Slider autoSlider;
    // Start is called before the first frame update

    // Update is called once per frame
    public void ToggleAuto()
    {
        if (player.GetAutoDrop() == true)
        {
            player.SetAutoDrop(false);
            autoSlider.gameObject.SetActive(false);
        }
        else
        {
            player.SetAutoDrop(true);
            autoSlider.gameObject.SetActive(true);
        }
    }
}
