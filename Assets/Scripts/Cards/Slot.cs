using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    // Start is called before the first frame update
    public bool HaveACard;
    public int? index;
    void Awake()
    {
        index = null;
        HaveACard = false;
    }
}
