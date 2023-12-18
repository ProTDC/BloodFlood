using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected TilemapVisualizer tilemapVisualizer = null;
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    public void GenerateDungeon()
    {
        GameObject.Find("Grid").GetComponent<Grid>().cellSize = new Vector2(1, 1);
        tilemapVisualizer.Clear();
        RunProceduralGeneration();
        GameObject.Find("Grid").GetComponent<Grid>().cellSize = new Vector2(1, 1);
    }

    protected abstract void RunProceduralGeneration();
}
