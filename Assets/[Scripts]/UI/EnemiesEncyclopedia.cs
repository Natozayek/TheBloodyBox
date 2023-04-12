using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesEncyclopedia : MonoBehaviour
{
    [SerializeField] GameObject encyclopedia;
    [SerializeField] GameObject _Tank, _Pred, _Explosive;
    public void ShowTank()
    {
        _Tank.SetActive(true);
    }
    public void CloseTank()
    {
        _Tank.SetActive(false);
    }
    public void ShowPred()
    {
        _Pred.SetActive(true);
    }

    public void ClosePred()
    {
        _Pred.SetActive(false);
    }

    public void ShowExplosive()
    {
        _Explosive.SetActive(true);
    }

    public void CloseExplosive()
    {
        _Explosive.SetActive(false);
    }
    public void Return()
    {
        encyclopedia.SetActive(false);
    }

}
