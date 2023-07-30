using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;
    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(GameState.GenerateGrid);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(GameState newState)
    {
        State = newState;
        switch (newState) 
        {
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.SpawnHeroes:
                UnitManager.Instance.SpawnHeroes();
                break;
            case GameState.SpawnEnemies:
                UnitManager.Instance.SpawnEnemies();
                break;
            case GameState.PlayerTurn:
                UnitManager.Instance.ResetActionPoints();
                HandManager.Instance.DrawToFive();
                break;
            case GameState.EnemiesTurn:
                UnitManager.Instance.PlayEnemiesTurn();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

    }
}

public enum GameState{
    GenerateGrid = 0,
    SpawnHeroes = 1,
    SpawnEnemies = 2,
    PlayerTurn = 3,
    EnemiesTurn = 4,
}

