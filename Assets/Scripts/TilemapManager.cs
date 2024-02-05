using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : Singleton<TilemapManager>
{

    [SerializeField] private Tilemap backGround, arena, path;
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3Int getTotalTileMapsSize()
    {
        return backGround.size;
    }
}
