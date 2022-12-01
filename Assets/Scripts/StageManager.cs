using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StageManager : MonoBehaviour
{
    public TextAsset[] stageFiles;
    TileType[,] tileTable;
    TileManager[,] tileTableObj;

    public TileManager tilePrefab;

    public delegate void StageClear();
    public StageClear stageClear;


    public void CreateStage()
    {
        Vector2 halfSize;
        float tileSize = tilePrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        halfSize.x = tileSize * (tileTable.GetLength(0) / 2);
        halfSize.y = tileSize * (tileTable.GetLength(1) / 2);

        for (int y = 0; y < tileTable.GetLength(1); y++)
        {
            for (int x = 0; x < tileTable.GetLength(0); x++)
            {
                Vector2Int position = new Vector2Int(x, y);
                TileManager tile = Instantiate(tilePrefab);
                tile.Init(tileTable[x, y], position);
                tile.clicked += ClickedTile;
                Vector2 setPosition = (Vector2)position * tileSize - halfSize;
                setPosition.y *= -1;
                tile.transform.position = setPosition;
                tileTableObj[x, y] = tile;
            }
        }
    }

    public void LoadStageFromText(int loadStage)
    {
        string[] lines = stageFiles[loadStage].text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        int columns = 5;
        int rows = 5;
        tileTable = new TileType[columns, rows];
        tileTableObj = new TileManager[columns, rows];
        for (int y = 0; y < rows; y++)
        {
            string[] values = lines[y].Split(new[] { ',' });
            for (int x = 0; x < columns; x++)
            {
                if (values[x] == "0")
                {
                    tileTable[x, y] = TileType.DEATH;
                }
                if (values[x] == "1")
                {
                    tileTable[x, y] = TileType.ALIVE;
                }
            }
        }
    }

    public void ClickedTile(Vector2Int center)
    {
        ReverseTiles(center);
        if (IsClear())
        {
            stageClear();
        }
    }

    void ReverseTiles(Vector2Int center)
    {
        Vector2Int[] around =
        {
            center + Vector2Int.up,
            center + Vector2Int.down,
            center + Vector2Int.right,
            center + Vector2Int.left,
        };

        foreach (Vector2Int position in around)
        {
            if (position.x < 0 || tileTableObj.GetLength(0) <= position.x)
            {
                continue;
            }
            if (position.y < 0 || tileTableObj.GetLength(1) <= position.y)
            {
                continue;
            }
            tileTableObj[position.x, position.y].RecerseTile();
        }
    }

    bool IsClear()
    {
        for (int y = 0; y < tileTable.GetLength(1); y++)
        {
            for (int x = 0; x < tileTable.GetLength(0); x++)
            {
                if (tileTableObj[x, y].type == TileType.ALIVE)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void DestroyStage()
    {
        for (int y = 0; y < tileTable.GetLength(1); y++)
        {
            for (int x = 0; x < tileTable.GetLength(0); x++)
            {
                Destroy(tileTableObj[x, y].gameObject);
            }
        }
    }

    void DebugTable()
    {
        for (int y = 0; y < 5; y++)
        {
            string debugText = "";
            for (int x = 0; x < 5; x++)
            {
                debugText += tileTable[x, y] + ",";
            }
            Debug.Log(debugText);
        }
    }


}