using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class PowerUPManager : MonoBehaviour
{
     [SerializeField] private PlayerBehaviour playerReference;
   //  [SerializeField] AudioSource itemSelected;
     [SerializeField] private SpawnManager spawnManagerReference;
     [SerializeField] private GameObject parentOjbect;
        
      public float waveRound;
      public ItemType type;
      Random _R = new Random();
    //Type of Power up items
    public enum ItemType
    {
            BulletPowerPlus,
            BulletSpeedPlus,
            IncreaseMaxHP,
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
        

    }
    private void OnEnable()
    {
       waveRound = spawnManagerReference.waveNumber;
        var itemTYPE = RandomizePowerUPForDisplay();
        type = itemTYPE;
    }
    private void OnDisable()
    { 
        //Remove Sprite from button
        this.gameObject.GetComponent<Image>().sprite = null;
        
    }



    //Function to execute ability on collision with pick-up prefab
    public  void OnItemPickUp(ItemType type)
    {

            switch (type)
            {
                case ItemType.BulletPowerPlus:
                playerReference.increaseBulletPower();
                    break;
            case ItemType.BulletSpeedPlus:
                playerReference.increaseBulletSpeed();
                break;
            case ItemType.IncreaseMaxHP:
                playerReference.increaseMAXHP();
                break;
            case ItemType.SPEEDUP:
                playerReference.SpeedUP();
                break;

            case ItemType.CHANGEBULLET:
                 playerReference.ChangeBulletPattern();
                break;

                case ItemType.FIRERATEUP:
                playerReference.FireRateUP();
                    break;

                case ItemType.DOUBLESHOT:
                playerReference.DoubleShotActive();
                    break;
            case ItemType.BURSTSHOT:
                playerReference.BurstShotActive();
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
        SetImageType(type);
        return type;
    }
    public void SetImageType(ItemType type)
    {
        switch (type)
        {

            case ItemType.BulletPowerPlus:
                this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Achivement");
                break;
            case ItemType.BulletSpeedPlus:
                this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Achivement");
                break;
            case ItemType.IncreaseMaxHP:
                this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Achivement");
                break;
            case ItemType.SPEEDUP:
                this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Achivement");
                break;

            case ItemType.CHANGEBULLET:
                this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Achivement");
                break;

            case ItemType.FIRERATEUP:
                this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Achivement");
                break;

            case ItemType.DOUBLESHOT:
                this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Achivement");
                break;
            case ItemType.BURSTSHOT:
                this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Achivement");
                break;
        }

    }

}
