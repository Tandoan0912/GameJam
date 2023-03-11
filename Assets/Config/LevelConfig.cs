using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
[CreateAssetMenu(menuName = "Game/Level Config")]
public class LevelConfig : ScriptableObject
{
    public List<LevelMap> levelMaps = new List<LevelMap>();

    [Button]
    void Add()
    {
        var map = new int[] {
9,9,9,9,9,9,9,9,9,9,
9,2,1,0,0,0,0,0,0,9,
9,0,1,0,1,1,1,1,1,9,
9,0,1,0,0,0,0,1,3,9,
9,0,1,0,1,1,0,1,0,9,
9,0,1,0,1,1,0,1,0,9,
9,0,0,0,1,1,0,1,0,9,
9,0,1,1,0,0,0,1,0,9,
9,0,0,1,0,0,0,0,0,9,
9,9,9,9,9,9,9,9,9,9,
        };
        var level = new LevelMap();
        level.map = map;
        levelMaps.Add(level);
    }
}

[System.Serializable]
public class LevelMap
{
    public int[] map;
}
