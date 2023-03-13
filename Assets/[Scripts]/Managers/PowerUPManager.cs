using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = System.Random;

public class PowerUPManager : MonoBehaviour
{
     [SerializeField] private PlayerBehaviour playerReference;
   //  [SerializeField] AudioSource itemSelected;
     [SerializeField] private SpawnManager spawnManagerReference;
     [SerializeField] private GameObject parentOjbect;
     [SerializeField] private TextMeshProUGUI nameOfPowerUP;
      public float waveRound;
      public ItemType typeX;
      Random _R = new Random();
      Sprite[] Xprites;
    


    //Type of Power up items
    public enum ItemType
    {
            BulletPowerPlus,
            BulletSpeedPlus,
            IncreaseMaxHP,
            IncreaseMaxStrenght,
            SPEEDUP,
            FIRERATEUP,

            DOUBLESHOT,
            BURSTSHOT,
            CHANGEBULLET,
            AUTOSHOT

    }

    private void Awake()
    {
        playerReference = FindObjectOfType<PlayerBehaviour>();
        spawnManagerReference = FindObjectOfType<SpawnManager>();
        Xprites = Resources.LoadAll<Sprite>("Sprites/PowerUps");
    }

    private void OnEnable()
    {
       waveRound = spawnManagerReference.waveNumber;
        var itemTYPE = RandomizePowerUPForDisplay();
        typeX = itemTYPE;
    }
    private void OnDisable()
    { 
        //Remove Sprite from button
        this.gameObject.GetComponent<Image>().sprite = null;
        
    }



    //Function to execute ability on collision with pick-up prefab
    public  void OnItemPickUp()
    {
        var selectedType = typeX;
            switch (selectedType)
            {
                case ItemType.BulletPowerPlus:
                playerReference.increaseBulletPower();
                parentOjbect.SetActive(false);
                    break;
            case ItemType.BulletSpeedPlus:
                playerReference.increaseBulletSpeed();
                parentOjbect.SetActive(false);
                break;
            case ItemType.IncreaseMaxHP:
                playerReference.increaseMAXHP();
                parentOjbect.SetActive(false);
                break;
            case ItemType.IncreaseMaxStrenght:
                playerReference.IncreaseStrength();
                parentOjbect.SetActive(false);
                break;
            case ItemType.SPEEDUP:
                playerReference.SpeedUP();
                parentOjbect.SetActive(false);
                break;
            case ItemType.FIRERATEUP:
                playerReference.FireRateUP();
                parentOjbect.SetActive(false);
                break;


                //TOP POWER UPS

            case ItemType.CHANGEBULLET:
                 playerReference.ChangeBulletPattern();
                parentOjbect.SetActive(false);
                break;

                case ItemType.DOUBLESHOT:
                playerReference.DoubleShotActive();
                parentOjbect.SetActive(false);
                break;
            case ItemType.BURSTSHOT:
                playerReference.SetBurstShotActive();
                parentOjbect.SetActive(false);
                break;

            case ItemType.AUTOSHOT:
                playerReference.SetAutomaticShot();
                parentOjbect.SetActive(false);
                break;
            }

           // itemSelected.Play();
            //GetComponent<SpriteRenderer>().enabled = false;
            //this.gameObject.SetActive(false);
    }

    //Randomizer Function
    T RandomEnumValue<T>()
    {
        //Make parameters to dont show
        //Double shoot till wave 5
        //AutoShot, Change bullet Pattern shot till wave 10
        var ItemTYPES = ItemType.GetValues(typeof(ItemType));
        var ItemSelected = (T)ItemTYPES.GetValue(_R.Next(ItemTYPES.Length));
        var ExcludeType1 = ItemType.AUTOSHOT;
        var ExcludeType2 = ItemType.CHANGEBULLET;
        var ExcludeType3 = ItemType.BURSTSHOT;

        if (waveRound < 5 && (ItemSelected.Equals(ExcludeType1) || ItemTYPES.Equals(ExcludeType2) || ItemTYPES.Equals(ExcludeType3)))
        {
            ItemSelected = (T)ItemTYPES.GetValue(_R.Next(ItemTYPES.Length - 4));
        }
        if (waveRound > 5 || waveRound <= 10)
        {
            ItemSelected = (T)ItemTYPES.GetValue(_R.Next(ItemTYPES.Length - 3));
        }
        if (waveRound >= 11)
        {
            ItemSelected = (T)ItemTYPES.GetValue(_R.Next(ItemTYPES.Length));
        }
        return ItemSelected;
    }
    //Set up type and sprite of button
    public ItemType RandomizePowerUPForDisplay()
    {
        var type = RandomEnumValue<ItemType>();
        SetImageTypeAndText(type);
        return type;
    }
    public void SetImageTypeAndText(ItemType type)
    {

        switch (type)
        {
            

            case ItemType.BulletPowerPlus:

                this.gameObject.GetComponent<Image>().sprite = Xprites[11];
                nameOfPowerUP.text = "Radioactive Touch: \n Increases the Projectile damage. ";
                break;

            case ItemType.BulletSpeedPlus:

                this.gameObject.GetComponent<Image>().sprite = Xprites[4];
                nameOfPowerUP.text = "Extended Momentum: \n Increase the Projectile speed.";
                break;

            case ItemType.IncreaseMaxHP:
                this.gameObject.GetComponent<Image>().sprite = Xprites[12];
                nameOfPowerUP.text = "Self-Sustenance: \n Increases Player's Max HP  \n Restores HP. ";
                break;

            case ItemType.IncreaseMaxStrenght:
                this.gameObject.GetComponent<Image>().sprite = Xprites[3];
                nameOfPowerUP.text = "Immunity: \n Increases Player's Max Strength. ";
                break;

            case ItemType.SPEEDUP:
                this.gameObject.GetComponent<Image>().sprite = Xprites[0];
                nameOfPowerUP.text = "Accelerated Evolution: \n Increases Player's Max Speed. \n ";
                break;

            case ItemType.FIRERATEUP:
                this.gameObject.GetComponent<Image>().sprite = Xprites[13];
                nameOfPowerUP.text = "Trigger happy: \n Increased fire rate. ";
                break;

            case ItemType.CHANGEBULLET:
                this.gameObject.GetComponent<Image>().sprite = Xprites[2];
                nameOfPowerUP.text = "Enhanced bullet: \n Gives the player a new projectile pattern.  ";
                break;

          

            case ItemType.DOUBLESHOT:
                this.gameObject.GetComponent<Image>().sprite = Xprites[1];
                nameOfPowerUP.text = "Double powered: \n Shoot extra bullet. ";
                break;
            case ItemType.BURSTSHOT:
                this.gameObject.GetComponent<Image>().sprite = Xprites[5];
                nameOfPowerUP.text = "The Three Musketeers: \n Activate burst shot mode. ";
                break;

            case ItemType.AUTOSHOT:
                this.gameObject.GetComponent<Image>().sprite = Xprites[14];
                nameOfPowerUP.text = "Unstoppable: \n Activate Automatic shot mode. ";
                break;
        }

    }

}
