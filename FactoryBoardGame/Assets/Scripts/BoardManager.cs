using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private Tile prefab;

    void Start()
    {
        generateMap();
        Camera.main.transform.position = new Vector3(width / 2 - 0.5f, height / 2 - 0.5f, -10);
    }

    public void generateMap()
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                var createdTile = Instantiate(prefab, new Vector3(i, j, 0), Quaternion.identity);
                createdTile.setTileName($"Tile{i}{j}");
                createdTile.transform.Rotate(90, 180, 0);
            }
        }
    }
}
