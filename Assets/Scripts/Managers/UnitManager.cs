using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    private List<ScriptableUnit> _units;
    private List<GameObject> Heroes;
    private List<BaseEnemy> Enemies;
    public BaseHero SelectedHero;

    void Awake(){
        Instance = this;

        _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
    }

    public void SpawnHeroes()
    {
        var heroCount = 1;
        for (int i = 0; i < heroCount; i++){
            var RandomPrefab = GetRandomUnit<BaseHero>(Fraction.Hero);
            var spawnedHero = Instantiate(RandomPrefab);
            var spawnTile = GridManager.Instance.GetHeroSpawnTile();

            spawnTile.SetUnit(spawnedHero);
            if (spawnedHero.Weapon != null) Instantiate(spawnedHero.Weapon);
            if (spawnedHero.Armour != null) Instantiate(spawnedHero.Armour);
        }
        GameManager.Instance.ChangeState(GameState.SpawnEnemies);
        
    }
    public void SpawnEnemies()
    {
        var ememiesCount = 2;
        for (int i = 0; i < ememiesCount; i++){
            var RandomPrefab = GetRandomUnit<BaseEnemy>(Fraction.Enemy);
            var spawnedEnemy = Instantiate(RandomPrefab);
            var spawnTile = GridManager.Instance.GetEnemySpawnTile();

            spawnTile.SetUnit(spawnedEnemy);
        }
        GameManager.Instance.ChangeState(GameState.PlayerTurn);
    }

    private T GetRandomUnit<T>(Fraction fraction) where T : BaseUnit
    {
        Debug.Log(_units.Where(u => u.Fraction == fraction).OrderBy(o => Random.value).First().UnitPrefab);
        return (T)_units.Where(u => u.Fraction == fraction).OrderBy(o => Random.value).First().UnitPrefab;
    }
    public void SetSelectedHero(BaseHero hero){
        SelectedHero = hero;
        //MenuManager.Instance.ShowSelectedHero(hero);
        if (SelectedHero == null) GridManager.Instance.HighlightOff();
        else{
            Dictionary<Vector2, Tile> Tiles = GridManager.Instance.GetTilesAtRange(SelectedHero.OccupiedTile, SelectedHero.Points);
            GridManager.Instance.HighlightOn(Tiles);
        }

    }
    public void TryEndPlayerTurn(){
        var Heroes = FindObjectsOfType<BaseHero>(); 
        if (Heroes.All(hero => hero.Points <= 0)) GameManager.Instance.ChangeState(GameState.EnemiesTurn);
    }
    public void ResetActionPoints(){
        var Heroes = FindObjectsOfType<BaseHero>(); 
        foreach (BaseHero hero in Heroes){
            hero.Points = 6;
            hero.PayPoints(0);
        }
    }
    public void PlayEnemiesTurn(){
        var Enemies = FindObjectsOfType<BaseEnemy>(); 
        foreach (BaseEnemy enemy in Enemies){
            enemy.Play();
        }
        GameManager.Instance.ChangeState(GameState.PlayerTurn);
    }
}
