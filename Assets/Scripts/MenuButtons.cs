using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] GameObject activeShop;

    [SerializeField] GameObject coinShop;
    [SerializeField] GameObject diamondShop;
    [SerializeField] GameObject prestige;
    [SerializeField] GameObject settings;
    // Start is called before the first frame update
    void Start()
    {
        coinShop.SetActive(true);
        diamondShop.SetActive(false);
        prestige.SetActive(false);
        settings.SetActive(false);
        activeShop = coinShop;
    }

    // Update is called once per frame
    public void ActivateCoinShop(int shopID)
    {
        activeShop.SetActive(false);
        switch (shopID)
        {
            case 1:
                activeShop = coinShop;
                break;
            case 2:
                activeShop = diamondShop;
                break;
            case 3:
                activeShop = prestige;
                break;
            case 4:
                activeShop = settings;
                break;
        }
        activeShop.SetActive(true);
    }
}
