using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    // This script instantiates a red Sphere on the tile which is blocked and that information is stored in ObstacleData
    // which has a boolean array and wherever the array is true the sphere is instantiated indicating the tile is blocked.


    public ObstacleData obstacleData;
    public GameObject obstaclePrefab;

    void Start()
    {
        GenerateObstacles();
    }

    void GenerateObstacles()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int z = 0; z < 10; z++)
            {
                int index = x + z * 10;     // For maintaining the index of the grid.
                if (obstacleData.obstacleGrid[index])
                {
                    Instantiate(obstaclePrefab, new Vector3(x*10, 0.5f, z*10), Quaternion.identity);
                }
            }
        }
    }
}
