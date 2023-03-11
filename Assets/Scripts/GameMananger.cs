using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.RestService;
using UnityEditorInternal.VersionControl;
using UnityEngine;

public class GameMananger : MonoBehaviour
{
    public Transform Tile;
    public Transform Grid;
    public int[] map;
    public Player player;

    public List<Transform> listTile;
    void Start()
    {
        map = new int[] {
            1,1,1,1,1,1,1,0,
            1,0,0,0,0,1,1,0,
            1,0,0,0,0,1,1,0,
            1,0,1,1,1,1,1,0,
            1,0,1,1,1,1,1,0,
            1,0,1,1,1,0,0,0,
            1,0,1,1,1,1,1,0,
            1,0,1,1,1,1,1,0,
        };

        MapSpawn();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {

        }
    }

    void FindPath()
    {
        var currPos = listTile.Find(d => d.transform.position.x == player.transform.position.x && d.transform.position.z == player.transform.position.z);
        foreach(var i in listTile)
        {

        }
    }

    void MapSpawn()
    {
        for (int i = 0; i < Mathf.Sqrt(map.Length); i++)
        {
            for (int j = 0; j < Mathf.Sqrt(map.Length); j++)
            {
                if (map[i * (int)Mathf.Sqrt(map.Length) + j] == 1)
                {
                    var tile = Instantiate(Tile, Grid);
                    tile.transform.position = new Vector3(i, 0, j);
                }
                else
                {
                    var tile = Instantiate(Tile, Grid);
                    tile.transform.position = new Vector3(i, -1, j);
                    listTile.Add(tile);
                }
            }
        }

        var obj = Instantiate(player);
        obj.transform.position = new Vector3(listTile[0].transform.position.x, 0, listTile[0].transform.position.z);
    }
}
