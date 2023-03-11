using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.Experimental.RestService;
using UnityEditorInternal.VersionControl;
using UnityEngine;

public class GameMananger : MonoBehaviour
{
    public Transform Tile;
    public Transform Grid;
    public Transform Border;
    public int[] map;
    public Player player;
    public Player _player;
    public Player _player2;
    private Vector3 direction;

    public PlayerInput playerInput;

    public List<Transform> listTile;
    void Start()
    {
        map = new int[] {
            9,9,9,9,9,9,9,9,9,9,
            9,1,1,1,1,1,1,1,1,9,
            9,1,0,0,0,0,1,1,2,9,
            9,1,0,0,0,0,1,1,0,9,
            9,1,0,1,1,1,1,1,0,9,
            9,1,0,1,1,1,1,1,0,9,
            9,1,0,1,1,1,0,0,0,9,
            9,1,0,1,1,1,1,1,0,9,
            9,1,3,0,0,0,0,0,0,9,
            9,9,9,9,9,9,9,9,9,9,
        };

        MapSpawn();
    }
    void Update()
    {
        if (playerInput.swipeDown)
        {
            direction = new Vector3(1,0,0);
            _player.GetWayPoint(FindPath(direction, _player));
            _player2.GetWayPoint(FindPath(direction, _player2));
        }
        if (playerInput.swipeUp)
        {
            direction = new Vector3(-1, 0, 0);
            _player.GetWayPoint(FindPath(direction, _player));
            _player2.GetWayPoint(FindPath(direction, _player2));
        }
        if (playerInput.swipeRight)
        {
            direction = new Vector3(0, 0, 1);
            _player.GetWayPoint(FindPath(direction, _player));
            _player2.GetWayPoint(FindPath(direction * -1, _player2));
        }
        if (playerInput.SwipeLeft)
        {
            direction = new Vector3(0, 0, -1);
            _player.GetWayPoint(FindPath(direction, _player));
            _player2.GetWayPoint(FindPath(direction * -1, _player2));
        }

    }

    private void FixedUpdate()
    {
        CheckWin();
    }

    void CheckWin()
    {
        if(Vector3.Distance(_player.transform.position, _player2.transform.position) <= 0.5f)
        {
            _player.gameObject.SetActive(false);
            _player2.gameObject.SetActive(false);
        }
    }

    public List<Transform> FindPath(Vector3 dir, Player _player)
    {
        var path = new List<Transform>();
        var currPos = listTile.Find(d => d.transform.position.x == _player.transform.position.x && d.transform.position.z == _player.transform.position.z);
        for(int i = 0; i < listTile.Count; i++)
        {
            if(listTile[i].transform.position - currPos.position == dir)
            {
                path.Add(listTile[i]);
                currPos = listTile[i];
                i = -1;
            }
        }
        return path;
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
                    tile.GetComponent<Tile>().Bush();
                }

                if (map[i * (int)Mathf.Sqrt(map.Length) + j] == 9)
                {
                    var tile = Instantiate(Border, Grid);
                    tile.transform.position = new Vector3(i, 0, j);
                }

                if (map[i * (int)Mathf.Sqrt(map.Length) + j] != 9 && map[i * (int)Mathf.Sqrt(map.Length) + j] != 1)
                {
                    var tile = Instantiate(Tile, Grid);
                    tile.transform.position = new Vector3(i, -1, j);
                    tile.GetComponent<Tile>().Grass();
                    listTile.Add(tile);
                }

                if(map[i * (int)Mathf.Sqrt(map.Length) + j] == 2)
                {
                    _player = Instantiate(player);
                    _player.transform.position = new Vector3(i, 0, j);
                    _player.Dog1.SetActive(true);
                }


                if (map[i * (int)Mathf.Sqrt(map.Length) + j] == 3)
                {
                    _player2 = Instantiate(player);
                    _player2.transform.position = new Vector3(i, 0, j);
                    _player2.Dog2.SetActive(true);
                }
            }
        }
    }
}
