using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHero : BaseUnit
{
    public int Points;
    public BaseItem Weapon;
    public BaseItem Armour;
    public void PayPoints(int value){
        Points -= value;
        UnitManager.Instance.TryEndPlayerTurn();
        MenuManager.Instance.ShowSelectedHero(this);
    }

    // Update is called once per frame

}
