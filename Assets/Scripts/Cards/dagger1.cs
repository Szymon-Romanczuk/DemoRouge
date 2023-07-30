using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dagger1 : BaseCard
{
    public override void BaseStats(){
        range = 2;
        attack = 1;
        heal = 0;
        cost = 1;
    }
    public override void SetDescrytion(){
        Descryption = "Dobierz kartę\n" + "Range " + range.ToString() + "\nAttack " + attack.ToString();;
    }
    public override void Special(){
        HandManager.Instance.DrawACard();
    }
}
