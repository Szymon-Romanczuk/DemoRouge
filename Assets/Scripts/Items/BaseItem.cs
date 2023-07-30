using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public BaseCard[] Cards;
    public Inventory Inventory;
    void Awake(){
        foreach(BaseCard Card in Cards){
            DeckManager.Instance.AddToDeck(Card);
        }
        Debug.Log("Jestem dagerem!");
    }
    void Remove(){
       foreach(BaseCard Card in Cards){
            DeckManager.Instance.AddToDeck(Card);
        }
    }
}
public enum Inventory{
    Weapon = 1,
    Armour = 2,
};