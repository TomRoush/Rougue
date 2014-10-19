using UnityEngine;
using System.Collections;

//Script that takes care of Tiles. Currently, just used to delete them when levels are changed.


public class Tile : MonoBehaviour {

    void OnEnable()
    {
        MakeMap.OnDelete += SelfDestruct;
    }

    void OnDisable()
    {
        MakeMap.OnDelete  -= SelfDestruct;
    }

    void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
