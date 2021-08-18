using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HudScript : MonoBehaviour
{
    [SerializeField]
    private Slider secondaryReloadSlider;
    [SerializeField]
    private Slider primaryReloadSlider;




    private float reloadSecondaryTime = 3f; //ENDRE SLIVER VALUES
    private float reloadSecondaryTimeLeft;

    private float reloadPrimaryTime = 0.8f; //ENDRE SLIVER VALUES
    private float reloadPrimaryTimeLeft ;
    private void Start()
    {
        reloadPrimaryTimeLeft = reloadPrimaryTime;
        reloadSecondaryTimeLeft = reloadSecondaryTime;
    }
    // Update is called once per frame
    void Update()
    {
        PrimaryReload();
        SecondaryReload();

        
    }
    private void PrimaryReload()
    {
        if (SniperScript.runHudTimerPrimary)
        {
            reloadPrimaryTimeLeft -= Time.deltaTime;
            primaryReloadSlider.value = reloadPrimaryTimeLeft;
        }
        else if (!SniperScript.runHudTimerPrimary)
        {
            reloadPrimaryTimeLeft = reloadPrimaryTime;
        }
    }
    private void SecondaryReload()
    {
        if (SniperScript.runHudTimerSecondary)
        {
            reloadSecondaryTimeLeft -= Time.deltaTime;
            secondaryReloadSlider.value = reloadSecondaryTimeLeft;
        }
        else if (!SniperScript.runHudTimerSecondary)
        {
            reloadSecondaryTimeLeft = reloadSecondaryTime;
        }
    }


}
