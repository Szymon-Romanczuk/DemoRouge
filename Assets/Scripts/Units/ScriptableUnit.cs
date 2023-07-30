using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Scriptable Unit")]
public class ScriptableUnit : ScriptableObject
{
    public Fraction Fraction;
    public BaseUnit UnitPrefab;

}

public enum Fraction
{
    Hero = 0,
    Enemy = 1,
}