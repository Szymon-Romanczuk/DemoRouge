using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseUnit : MonoBehaviour
{
    public string UnitName;
    public Tile OccupiedTile;
    public Fraction Fraction;
    public int Health;
    [SerializeField] private GameObject _health;
    // Start is called before the first frame update
    void Awake()
    {
        InfoUpdate();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamge(int damage){
        Health -= damage;
        InfoUpdate();
        if (Health <= 0){
            Destroy(gameObject);
        }
    }
    public void InfoUpdate(){
        _health.SetActive(false);
        _health.GetComponentInChildren<Text>().text = this.Health.ToString();
        _health.SetActive(true);
    }
}
