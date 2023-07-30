using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Slot slot1, slot2, slot3, slot4, slot5;
    public HashSet<int> Indexes;
    public static HandManager Instance;
    void Awake()
    {
        Instance = this;
        Indexes = new HashSet<int>();
    }
    public void DrawToFive(){
        for(int i = 0; i < 10; i++){
            DrawACard();
        }
    }
    public void DrawACard(){
        Slot slot = null;
        if (slot5.HaveACard == false)   slot = slot5;
        if (slot4.HaveACard == false)   slot = slot4;
        if (slot3.HaveACard == false)   slot = slot3;
        if (slot2.HaveACard == false)   slot = slot2;
        if (slot1.HaveACard == false)   slot = slot1;
        if (slot == null) return;
        
        int index = GetARandomIndex(DeckManager.Instance.Deck);
        BaseCard Card = GetCard(DeckManager.Instance.Deck, index);
        var card = Instantiate(Card, slot.transform);
        slot.HaveACard = true;
        slot.index = (int?)index;
        card.slot = slot;
    }

    private BaseCard GetCard(BaseCard[] Cards, int index){
        Indexes.Add(index);
        return Cards[index];
    }
    private int GetARandomIndex(BaseCard[] Cards){
        int index = UnityEngine.Random.Range(0, Cards.Length);
        while(Indexes.Contains(index)){
            index = UnityEngine.Random.Range(0, Cards.Length);
        }
        return index;
    }
    public void Remove(BaseCard card){
        Debug.Log("remove a card");
        card.slot.HaveACard = false;
        Indexes.Remove((int)card.slot.index);        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
