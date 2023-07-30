using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject _marked;
    [SerializeField] private bool _isWalkable;
    public string TileName;
    public BaseUnit OccupiedUnit;
    public bool Walkabale(){
        return _isWalkable && OccupiedUnit == null;
    }
    public void SetUnit(BaseUnit unit)
    {
            if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
            unit.transform.position = transform.position;
            OccupiedUnit = unit;
            unit.OccupiedTile = this;
    }

    void OnMouseEnter(){
        _highlight.SetActive(true);
        MenuManager.Instance.ShowTileInfo(this);
    }
    public void HighlightOn(){
        _marked.SetActive(true);
    }
    public void HighlightOff(){
        _marked.SetActive(false);
    }

    
    void OnMouseExit(){
        _highlight.SetActive(false);
        MenuManager.Instance.ShowTileInfo(null);
    }
    void OnMouseDown(){
        if(GameManager.Instance.State != GameState.PlayerTurn) return;
        if (UnitManager.Instance.SelectedHero != null && _marked.activeInHierarchy == false) return;
        if (OccupiedUnit != null){
            if(OccupiedUnit.Fraction == Fraction.Hero){
                UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
                MenuManager.Instance.ShowSelectedHero(UnitManager.Instance.SelectedHero);
            }
            else{
                if (UnitManager.Instance.SelectedHero != null){
                    var enemy = (BaseEnemy) OccupiedUnit;
                    enemy.TakeDamge(DeckManager.Instance.selectedCard.attack);
                    UnitManager.Instance.SelectedHero.PayPoints(DeckManager.Instance.selectedCard.cost);
                    HandManager.Instance.Remove(DeckManager.Instance.selectedCard);
                    Debug.Log("autoDestrukcja!");
                    Destroy(DeckManager.Instance.selectedCard.gameObject);
                    DeckManager.Instance.selectedCard = null;
                    UnitManager.Instance.SetSelectedHero(null);
                }
            }
        }
        else{
            if(UnitManager.Instance.SelectedHero != null){
                int distance = GridManager.Instance.GetDistance(UnitManager.Instance.SelectedHero.OccupiedTile, this);
                UnitManager.Instance.SelectedHero.PayPoints(distance);
                SetUnit(UnitManager.Instance.SelectedHero);
                UnitManager.Instance.SetSelectedHero(null);

            }
        }
    }
}
