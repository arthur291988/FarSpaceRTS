using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MegaAttackController : MonoBehaviour
{
    [HideInInspector]
    public PlayerBattleShip chosenCruiserToMegaAttack;
    public Button megaAttackButton;
    public Image megaAttackButtonImg;

    [HideInInspector]
    public float megaAttackTimer;

    private float megaAttackTime;

    //Start is called before the first frame update
    void Start()
    {
        megaAttackButtonImg = megaAttackButton.image;
        megaAttackTime = CommonProperties.megaAttackTime; 
    }

    public void megaAttackOfChosenCruiser()
    {
        chosenCruiserToMegaAttack.MegaAttackOfShip();
        disableMegaAttackButtonIfCruiserIsDestroyed();
        megaAttackTimer = megaAttackTime;
        megaAttackButtonImg.fillAmount = 0;
    }
    public void disableMegaAttackButtonIfCruiserIsDestroyed()
    {
        chosenCruiserToMegaAttack = null;
        megaAttackButton.interactable = false;
    }

    private void Update()
    {
        if (megaAttackTimer > 0)
        {
            megaAttackTimer -= Time.deltaTime;
            if (megaAttackTimer < 0) megaAttackTimer = 0;
            megaAttackButtonImg.fillAmount = 1 - megaAttackTimer / megaAttackTime;
        }
    }
}
