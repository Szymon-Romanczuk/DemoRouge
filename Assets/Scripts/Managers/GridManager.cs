using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _ground, _wall;
    [SerializeField] private Transform _cam;
    private Dictionary<Vector2, Tile> _tiles;

    void Awake(){
        Instance = this;
    }

    public void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if(y == 0 || x == 0 || y == _height - 1 || x == _width - 1)
                {
                    var spawnTile = Instantiate(_wall, new Vector3(x, y), Quaternion.identity);
                    spawnTile.name = $"Wall {x} {y}";
                    _tiles[new Vector2(x, y)] = spawnTile;
                }
                else
                {
                    var spawnTile = Instantiate(_ground, new Vector3(x, y), Quaternion.identity);
                    spawnTile.name = $"Tile {x} {y}";
                    _tiles[new Vector2(x, y)] = spawnTile;
                }
              
            }
        }
        _cam.transform.position = new Vector3((float)_width/2 -0.5f, (float)_height/2 - 3.0f, -10);
        GameManager.Instance.ChangeState(GameState.SpawnHeroes);
    }

    public Tile GetHeroSpawnTile()
    {
        return _tiles.Where(t => t.Key.x == 1 && t.Value.Walkabale()).OrderBy(t => UnityEngine.Random.value).First().Value;
    }
    public Tile GetEnemySpawnTile()
    {
        return _tiles.Where(t => t.Key.x > _width / 2 && t.Value.Walkabale()).OrderBy(t => UnityEngine.Random.value).First().Value;
    }
    public Tile GetTileAtPosition(Vector2 pos)
    {
        if(_tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }
    public Dictionary<Vector2, Tile> GetTilesAtRange(Tile tile, int range){
        //return;
        Vector2 pos = GetPositionFromTile(tile);
        Dictionary<Vector2, Tile> Tiles = _tiles.Where(t => Math.Abs(t.Key.x - pos.x) + Math.Abs(t.Key.y - pos.y) <= range && t.Value.Walkabale()).ToDictionary(e => e.Key, e => e.Value);
        return Tiles;
    }
    public Dictionary<Vector2, Tile> GetEnemiesInRange(Tile tile, int range){
        Vector2 pos = GetPositionFromTile(tile);
        Dictionary<Vector2, Tile> Tiles = _tiles.Where(t => Math.Abs(t.Key.x - pos.x) + Math.Abs(t.Key.y - pos.y) <= range && t.Value.OccupiedUnit != null).ToDictionary(e => e.Key, e => e.Value);
        Tiles = Tiles.Where(t => t.Value.OccupiedUnit.Fraction == Fraction.Enemy).ToDictionary(e => e.Key, e => e.Value);
        return Tiles;
    }
    public void HighlightOn(Dictionary<Vector2, Tile> Tiles){
        foreach (KeyValuePair<Vector2, Tile> t in Tiles){
            t.Value.HighlightOn();
        }
    }

    public Vector2 GetPositionFromTile(Tile tile){
        return _tiles.Where(t => t.Value == tile).First().Key;
    }
    public void HighlightOff(){
        foreach (KeyValuePair<Vector2, Tile> t in _tiles)
        {
             t.Value.HighlightOff();
        }
    }
    public int GetDistance(Tile first, Tile second){
        Vector2 distance = GetPositionFromTile(first) - GetPositionFromTile(second);
        return (int) (Math.Abs(distance.x) + Math.Abs(distance.y));
    }
}
