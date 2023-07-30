using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _tileObject, _tileUnitObject, _points;
    public static MenuManager Instance;
    void Awake(){
        Instance = this;
    }
    // Start is called before the first frame update

    public void ShowSelectedHero(BaseHero hero){
        if (hero == null){
            _points.SetActive(false);
            return;
        }
        _points.GetComponentInChildren<Text>().text = hero.Points.ToString();
        _points.SetActive(true);
    }


     public void ShowTileInfo(Tile tile){
        if (tile == null){
            _tileObject.SetActive(false);
            _tileUnitObject.SetActive(false);
            return;
        }
        _tileObject.GetComponentInChildren<Text>().text = tile.TileName;
        _tileObject.SetActive(true);

        if(tile.OccupiedUnit != null){
             _tileUnitObject.GetComponentInChildren<Text>().text = tile.OccupiedUnit.UnitName;
            _tileUnitObject.SetActive(true);
        }
    }
}
