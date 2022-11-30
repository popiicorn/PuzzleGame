using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum TileType
{
    DEATH,
    ALIVE,
}

public class TileManager : MonoBehaviour
{
    public TileType type;
    public Sprite deathSprite;
    public Sprite aliveSprite;
    public GameObject lineParticle;
    public float reversetime;

    SpriteRenderer spriteRenderer;
    Vector2Int intPosition;

    public delegate void Clicked(Vector2Int center);
    public Clicked clicked;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    public void Init(TileType tileType, Vector2Int position)
    {
        intPosition = position;
        SetType(tileType);
    }

    void SetType(TileType tileType)
    {
        type = tileType;
        SetImage(tileType);
    }

    void SetImage(TileType tileType)
    {
        if (type == TileType.DEATH)
        {
            spriteRenderer.sprite = deathSprite;
        }
        else if (type == TileType.ALIVE)
        {
            spriteRenderer.sprite = aliveSprite;
        }
    }

    public void OnTile()
    {
        StartCoroutine(OnTileTime());
    }


    IEnumerator OnTileTime()
    {
        RecerseTile();
        LineFxActiveOn();
        Invoke("LineFxActiveOff", 0.3f);
        yield return new WaitForSeconds(reversetime);
        clicked(intPosition);

    }



    public void RecerseTile()
    {

        if (type == TileType.DEATH)
        {
            SetType(TileType.ALIVE);
        }
        else if (type == TileType.ALIVE)
        {
            SetType(TileType.DEATH);
        }
    }

    public void LineFxActiveOn()
        {
            lineParticle.SetActive(true);
        }

    public void LineFxActiveOff()
    {
        lineParticle.SetActive(false);
    }

}