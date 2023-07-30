using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseCard : MonoBehaviour
{
    public string Descryption;
    public int cost;
    public int range;
    public int heal;
    public int attack;
    public Slot slot;
    [SerializeField] private GameObject _descryption, _cost;
    // Start is called before the first frame update
    void Awake(){
        
        BaseStats();
        SetDescrytion();
        slot = null;
        _descryption.GetComponentInChildren<Text>().text = Descryption;
        _cost.GetComponentInChildren<Text>().text = cost.ToString();

    }
    public virtual void BaseStats(){
        range = 1;
        attack = 2;
        heal = 0;
        cost = 2;
    }
    public virtual void SetDescrytion(){
        Descryption = "Range " + range.ToString() + "\nAttack " + attack.ToString();
    }
    void OnMouseDown(){
        Debug.Log("kikam!");
        if (GameManager.Instance.State != GameState.PlayerTurn) return;
        if (UnitManager.Instance.SelectedHero == null) return;
        if (UnitManager.Instance.SelectedHero.Points < cost) return;
        
        Special();
        PlayCard();
    }

    void OnMouseEnter(){
        Debug.Log("jestem na karcie");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayCard(){
        DeckManager.Instance.selectedCard = this;
        if(heal > 0){
            UnitManager.Instance.SelectedHero.Health += heal;
            UnitManager.Instance.SelectedHero.TakeDamge(0);
            UnitManager.Instance.SelectedHero.PayPoints(cost);
            HandManager.Instance.Remove(this);
            Destroy(gameObject);
        }
        GridManager.Instance.HighlightOff();
        if (range > 0 && attack > 0){
            var tiles = GridManager.Instance.GetEnemiesInRange(UnitManager.Instance.SelectedHero.OccupiedTile, range);
            GridManager.Instance.HighlightOn(tiles);
        }
        else{
            DeckManager.Instance.selectedCard = null;
            UnitManager.Instance.SelectedHero = null;
        }
    }
    public virtual void Special(){

    }
}
