using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armour1 : BaseCard
{
    // Start is called before the first frame update
    public override void BaseStats(){
        range = 0;
        attack = 0;
        heal = 3;
        cost = 1;
    }
    public override void SetDescrytion(){
        Descryption = "Heal 3";
    }
}
