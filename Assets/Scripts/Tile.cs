using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Transform tileChild;
    public Transform bush;
    public Transform grass;

    public List<Sprite> sprites = new List<Sprite>();
    public void Bush()
    {
        bush.gameObject.SetActive(true);
        bush.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Count)];
    }

    public void Grass()
    {
        grass.gameObject.SetActive(true);
    }
}
