using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;

public class BaseEnemy : BaseUnit
{
    // Start is called before the first frame update
    private BaseHero Target; 
    void Start()
    {

    }
    public void Play(){
        SetTarget();
        Move(2);
        Attack(2, 1, 1);
    }
    private void Attack(int range, int attack, int targets){
        SetTarget();
        if (GridManager.Instance.GetDistance(Target.OccupiedTile, this.OccupiedTile) <= range){
            Target.TakeDamge(attack);
            Debug.Log("Badumc " + attack.ToString());
        }
    }
    private void Move(int value){
        Dictionary<Vector2, Tile> Tiles = GridManager.Instance.GetTilesAtRange(OccupiedTile, 1);
        Tile tile = Tiles.Where(t => t.Value.OccupiedUnit == null).OrderBy(t => GridManager.Instance.GetDistance(t.Value, Target.OccupiedTile)).First().Value;
        Debug.Log(this.UnitName + " " + tile + value);
        if (tile != null){
            tile.SetUnit(this);
        }
        value--;
        if (value > 0) Move(value);
    }
    private void SetTarget(){
        var Heroes = FindObjectsOfType<BaseHero>();
        int distance = int.MaxValue;
        BaseHero target = null;
        foreach (BaseHero hero in Heroes){
            int tmpDistance = GridManager.Instance.GetDistance(hero.OccupiedTile, OccupiedTile);
            if (distance > tmpDistance){
                distance = tmpDistance;
                target = hero;
            }
        }
        Target = target;
    }
}
