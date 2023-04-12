using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PowerUPManager;
using Object = UnityEngine.Object;
using Random = System.Random;

[System.Serializable]
public class RandomNumerSelector 
{
    public int minNumber;
    public int maxNumber;
}

public class PowerUPManager : MonoBehaviour
{
     [SerializeField] private PlayerBehaviour playerReference;
   //  [SerializeField] AudioSource itemSelected;
     [SerializeField] private SpawnManager spawnManagerReference;
     [SerializeField] private GameObject parentOjbect;
     //[SerializeField] private TextMeshProUGUI nameOfPowerUP;
     [SerializeField] private StatsVariableIncreaser VaribleIncreaser;
    [SerializeField] private GameObject[] buttons;
    [SerializeField] public List<RandomNumerSelector> numberPerRound;

      public float waveRound = 1;
      public ItemType typeX1, typeX2, typeX3;
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
            AUTOSHOT, 
            ROCKET

    }

    private void Awake()
    {
        playerReference = FindObjectOfType<PlayerBehaviour>();
        spawnManagerReference = FindObjectOfType<SpawnManager>();
        Xprites = Resources.LoadAll<Sprite>("Sprites/PowerUps");
        waveRound = VaribleIncreaser.WaveRound;
    }

    private void OnEnable()
    {
        waveRound = VaribleIncreaser.WaveRound;
        var itemTYPE = RandomizePowerUPForDisplay();
        var itemTYPE2 = RandomizePowerUPForDisplay2();
        var itemTYPE3 = RandomizePowerUPForDisplay3();

        typeX1 = itemTYPE;
        typeX2 = itemTYPE2;
        typeX3 = itemTYPE3;

    }
    private void OnDisable()
    { 
        //Remove Sprite from button
        this.gameObject.GetComponent<Image>().sprite = null;
        
    }

    //Function to execute ability on collision with pick-up prefab
    public  void OnItemPickUp1()
    {
        var selectedType = typeX1;
            switch (selectedType)
            {
                case ItemType.BulletPowerPlus:
                increaseBulletPower();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                    break;
            case ItemType.BulletSpeedPlus:
                increaseBulletSpeed();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;
            case ItemType.IncreaseMaxHP:
                playerReference.increaseMAXHP();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;
            case ItemType.IncreaseMaxStrenght:
                playerReference.IncreaseStrength();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;
            case ItemType.SPEEDUP:
                playerReference.SpeedUP();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;
            case ItemType.FIRERATEUP:
                playerReference.FireRateUP();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;


                //TOP POWER UPS

            case ItemType.CHANGEBULLET:
                playerReference.ChangeBulletPattern();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;

                case ItemType.DOUBLESHOT:
                playerReference.DoubleShotActive();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;
            case ItemType.BURSTSHOT:
                playerReference.SetBurstShotActive();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;

            case ItemType.AUTOSHOT:
                playerReference.SetAutomaticShot();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;

            case ItemType.ROCKET:
                playerReference.SetRocketShot();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;
        }

           // itemSelected.Play();
            //GetComponent<SpriteRenderer>().enabled = false;
            //this.gameObject.SetActive(false);
    }
    public void OnItemPickUp2()
    {
        var selectedType = typeX2;
        switch (selectedType)
        {
            case ItemType.BulletPowerPlus:
                increaseBulletPower();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;
            case ItemType.BulletSpeedPlus:
                increaseBulletSpeed();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;
            case ItemType.IncreaseMaxHP:
                playerReference.increaseMAXHP();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;
            case ItemType.IncreaseMaxStrenght:
                playerReference.IncreaseStrength();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;
            case ItemType.SPEEDUP:
                playerReference.SpeedUP();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;
            case ItemType.FIRERATEUP:
                playerReference.FireRateUP();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;


            //TOP POWER UPS

            case ItemType.CHANGEBULLET:
                playerReference.ChangeBulletPattern();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;

            case ItemType.DOUBLESHOT:
                playerReference.DoubleShotActive();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;
            case ItemType.BURSTSHOT:
                playerReference.SetBurstShotActive();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;

            case ItemType.AUTOSHOT:
                playerReference.SetAutomaticShot();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;

            case ItemType.ROCKET:
                playerReference.SetRocketShot();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;
        }

        // itemSelected.Play();
        //GetComponent<SpriteRenderer>().enabled = false;
        //this.gameObject.SetActive(false);
    }
    public void OnItemPickUp3()
    {
        var selectedType = typeX3;
        switch (selectedType)
        {
            case ItemType.BulletPowerPlus:
                increaseBulletPower();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;
            case ItemType.BulletSpeedPlus:
                increaseBulletSpeed();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;
            case ItemType.IncreaseMaxHP:
                playerReference.increaseMAXHP();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;
            case ItemType.IncreaseMaxStrenght:
                playerReference.IncreaseStrength();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;
            case ItemType.SPEEDUP:
                playerReference.SpeedUP();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;
            case ItemType.FIRERATEUP:
                playerReference.FireRateUP();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;


            //TOP POWER UPS

            case ItemType.CHANGEBULLET:
                playerReference.ChangeBulletPattern();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;

            case ItemType.DOUBLESHOT:
                playerReference.DoubleShotActive();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;
            case ItemType.BURSTSHOT:
                playerReference.SetBurstShotActive();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;

            case ItemType.AUTOSHOT:
                playerReference.SetAutomaticShot();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;

            case ItemType.ROCKET:
                playerReference.SetRocketShot();
                spawnManagerReference.isPowerUpSelected = true;
                spawnManagerReference.intermission = 3;
                parentOjbect.SetActive(false);
                break;
        }

        // itemSelected.Play();
        //GetComponent<SpriteRenderer>().enabled = false;
        //this.gameObject.SetActive(false);
    }


    //Randomizer Function
    T RandomEnumValue<T>()// BOTH MIX OF DAMAGE AND STREGHT
    {
        var ItemTYPES = ItemType.GetValues(typeof(ItemType));
        var ItemSelected = (T)ItemTYPES.GetValue(_R.Next(ItemTYPES.Length));


        switch (waveRound)
        {

            case 1:
                {
                    ItemSelected = (T)ItemTYPES.GetValue(5);
                    break;
                }
            case 2:
                {
                    ItemSelected = (T)ItemTYPES.GetValue(3);
                    break;
                }
            case 3:
                {
                    ItemSelected = (T)ItemTYPES.GetValue(4);
                    break;
                }
            case 4:
                {
                    ItemSelected = (T)ItemTYPES.GetValue(2);
                    break;
                }
            case 5:
                {
                    ItemSelected = (T)ItemTYPES.GetValue(1);
                    break;
                }
            case 6:
                {
                    ItemSelected = (T)ItemTYPES.GetValue(7);
                    break;
                }
            case 7:
                {
                    ItemSelected = (T)ItemTYPES.GetValue(0);
                    break;
                }
            case 8:
                {
                    ItemSelected = (T)ItemTYPES.GetValue(6);
                    break;
                }
            case 9:
                {
                    ItemSelected = (T)ItemTYPES.GetValue(5);
                    break;
                }

        }


        return ItemSelected;

    }
    T RandomEnumValue2<T>()//CYCLE BULLET TYPE DISPLAY
    {
        var ItemTYPES = ItemType.GetValues(typeof(ItemType));
        var ItemSelected = (T)ItemTYPES.GetValue(_R.Next(ItemTYPES.Length));

        switch (waveRound)
        {

            case 1:
                {
                    ItemSelected = (T)ItemTYPES.GetValue(7);
                    break;
                }
            case 2:
                {
                    ItemSelected = (T)ItemTYPES.GetValue(8);
                    break;
                }
            case 3:
                {
                    ItemSelected = (T)ItemTYPES.GetValue(6);
                    break;
                }
            case 4:
                {
                    ItemSelected = (T)ItemTYPES.GetValue(5);
                    break;
                }
            case 5:
                {
                    ItemSelected = (T)ItemTYPES.GetValue(6);
                    break;
                }
            case 6:
                {
                    ItemSelected = (T)ItemTYPES.GetValue(9);
                    break;
                }
            case 7:
                {
                    ItemSelected = (T)ItemTYPES.GetValue(6);
                    break;
                }
            case 8:
                {
                    ItemSelected = (T)ItemTYPES.GetValue(10);
                    break;
                }
            case 9:
                {
                    ItemSelected = (T)ItemTYPES.GetValue(2);
                    break;
                }

        }


        return ItemSelected;


    }
    T RandomEnumValue3<T>()
    {
        var ItemTYPES = ItemType.GetValues(typeof(ItemType));
        var ItemSelected = (T)ItemTYPES.GetValue(_R.Next(ItemTYPES.Length));

        switch (waveRound)
        {

            case 1:
                {
                    ItemSelected = (T)ItemTYPES.GetValue(0);
                    break;
                }
            case 2:
                {
                    ItemSelected = (T)ItemTYPES.GetValue(1);
                    break;
                }
            case 3:
                {
                    ItemSelected = (T)ItemTYPES.GetValue(2);
                    break;
                }
            case 4:
                {
                    ItemSelected = (T)ItemTYPES.GetValue(3);
                    break;
                }
            case 5:
                {
                    ItemSelected = (T)ItemTYPES.GetValue(4);
                    break;
                }
            case 6:
                {
                    ItemSelected = (T)ItemTYPES.GetValue(5);
                    break;
                }
            case 7:
                {
                    ItemSelected = (T)ItemTYPES.GetValue(0);
                    break;
                }
            case 8:
                {
                    ItemSelected = (T)ItemTYPES.GetValue(1);
                    break;
                }
            case 9:
                {
                    ItemSelected = (T)ItemTYPES.GetValue(4);
                    break;
                }

        }


        return ItemSelected;


    }//STATS POWER UP DISPLAY
    //Set up type and sprite of button
    public ItemType RandomizePowerUPForDisplay()
    {
        var type = RandomEnumValue<ItemType>();
        SetImageTypeAndText(type, buttons[0]);
        return type;
    }
    public ItemType RandomizePowerUPForDisplay2()
    {
        var type = RandomEnumValue2<ItemType>();
        SetImageTypeAndText(type, buttons[1]);
        return type;
    }
    public ItemType RandomizePowerUPForDisplay3()
    {
        var type = RandomEnumValue3<ItemType>();
        SetImageTypeAndText(type, buttons[2]);
        return type;
    }
    public void SetImageTypeAndText(ItemType type, GameObject button)
    {

        switch (type)
        {
            

            case ItemType.BulletPowerPlus://0

                button.gameObject.GetComponent<Image>().sprite = Xprites[11];
                button.GetComponentInChildren<TextMeshProUGUI>().text = "Radioactive Touch: \n Increases the Projectile damage. ";
                break;

            case ItemType.BulletSpeedPlus://1

                button.GetComponent<Image>().sprite = Xprites[4];
                button.GetComponentInChildren<TextMeshProUGUI>().text = "Extended Momentum: \n Increase the Projectile speed.";
                break;

            case ItemType.IncreaseMaxHP://2
                button.GetComponent<Image>().sprite = Xprites[12];
                button.GetComponentInChildren<TextMeshProUGUI>().text = "Sustenance: \n Increases Player's Max HP  \n Restores HP. ";
                break;

            case ItemType.IncreaseMaxStrenght://3
                button.GetComponent<Image>().sprite = Xprites[3];
                button.GetComponentInChildren<TextMeshProUGUI>().text = "Immunity: \n Increases Player's Max Strength. ";
                break;

            case ItemType.SPEEDUP://4
                button.GetComponent<Image>().sprite = Xprites[0];
                button.GetComponentInChildren<TextMeshProUGUI>().text = "Accelerated Evolution: \n Increases Player's Max Speed. \n ";
                break;

            case ItemType.FIRERATEUP://5
                button.GetComponent<Image>().sprite = Xprites[13];
                button.GetComponentInChildren<TextMeshProUGUI>().text = "Trigger happy: \n Increased fire rate. ";
                break;

            case ItemType.CHANGEBULLET://6
                button.GetComponent<Image>().sprite = Xprites[2];
                button.GetComponentInChildren<TextMeshProUGUI>().text = "Enhanced bullet: \n Gives the player a new projectile pattern.  ";
                break;

            case ItemType.DOUBLESHOT://7
                button.GetComponent<Image>().sprite = Xprites[1];
                button.GetComponentInChildren<TextMeshProUGUI>().text = "Double powered: \n Shoot extra bullet. ";
                break;
            case ItemType.BURSTSHOT://8
                button.GetComponent<Image>().sprite = Xprites[5];
                button.GetComponentInChildren<TextMeshProUGUI>().text = "The Three Musketeers: \n Activate burst shot mode. ";
                break;

            case ItemType.AUTOSHOT://9
                button.GetComponent<Image>().sprite = Xprites[14];
                button.GetComponentInChildren<TextMeshProUGUI>().text = "Unstoppable: \n Activate Automatic shot mode. ";
                break;

            case ItemType.ROCKET://10
                button.GetComponent<Image>().sprite = Xprites[14];
                button.GetComponentInChildren<TextMeshProUGUI>().text = "Saturn-V: \n Activate Rocket shot mode. ";
                break;
        }

    }

    public void increaseBulletPower()
    {
        VaribleIncreaser._Bullet_Damage_Multiplier = VaribleIncreaser._Bullet_Damage_Multiplier + 0.12f;
    }
    public void increaseBulletSpeed()
    {
        VaribleIncreaser._BulletSpeed_Multiplier = VaribleIncreaser._BulletSpeed_Multiplier + 0.1f;

    }

}
