using UnityEngine;

public class GridManager : MonoBehaviour
{
    // This script instantiates the tiles into the world and also sets the position of the tiles by using TileInfo component
    // and I have multiplied the positions of the tiles with 10 as the size of the tiles are 10x10.

    public GameObject tilePrefab;
    public int gridSize = 10;
    public Transform parent;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3(x*10, 0, z*10), Quaternion.identity, parent);
                tile.name = $"{x}, {z}";
                tile.AddComponent<TileInfo>().SetPosition(x, z);
            }
        }
    }
}
