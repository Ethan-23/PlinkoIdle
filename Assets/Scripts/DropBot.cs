using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBot : MonoBehaviour
{
    [SerializeField] int level = 1;
    [SerializeField] int dropSpeedUpgrade = 0;
    [SerializeField] int multiplierUpgrade = 0;
    [SerializeField] int specialBallUpgrade = 0;
    [SerializeField] bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
            return;

    }   

    public void ToggleActive()
    {
        active = !active;
    }
}
