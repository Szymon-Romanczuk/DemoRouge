using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance;
    public BaseCard selectedCard;
    public BaseCard Base;
    public BaseCard[] Deck;
    void Awake(){
        Instance = this;
        AddToDeck(Base);
        AddToDeck(Base);
        AddToDeck(Base);
        AddToDeck(Base);
        AddToDeck(Base);
        Debug.Log(Deck.Length);
    }
    public void AddToDeck(BaseCard card){
        Debug.Log(Deck.Length);
        Debug.Log(card);
        if(card != null){
            Array.Resize(ref Deck, Deck.Length + 1);
            Deck[Deck.Length - 1] = card;
        }
        Debug.Log(Deck.Length);
    }
    public void RemoveFromDeck(BaseCard card){
        int? index = GetIndexFromCard(card);
        if(index == null) return; 
        for (int a = (int) index; a < Deck.Length - 1; a++)
        {
            // moving elements downwards, to fill the gap at [index]
            Deck[a] = Deck[a + 1];
        }
        // finally, let's decrement Array's size by one
        Array.Resize(ref Deck, Deck.Length - 1);
    }
    
    public int? GetIndexFromCard(BaseCard card){
        for(int? i = 0; i<Deck.Length; i++){
            if(Deck[(int)i] == card) return i;
        }
        return null;
    }
}
