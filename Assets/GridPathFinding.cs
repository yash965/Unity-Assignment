using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GridPathFinding : MonoBehaviour
{
    // This is the PathFinding script and to explain this I will divide it with different functions that I have used -
    // 1. MovePlayerToTarget(position) - It updates the position of the player according to the path given by FindPath method.

    // 2. FindPath(start, goal) - It finds the positions that the player can move by using the GetNeighbour method and
    // does this at every position till the goal position is achieved and store it in a list for the player to follow through.

    // 3. GetNeighbors(position) - It explores all the direction to the position to move next from the current position and
    // checks if the positions are not out of grid and it is not a obstacle by using the method IsObstacle.

    // 4. IsObstacle(position) - This checks if the given position has a obstacle or not. If not it returns true and otherwise
    //    false.

    // 5. GetAdjacentPosition(playerPos, enemyPos) - This checks the neighbouring positions of the player and compare the distance
    //    with the enemy position and returns the shortest one and move towards it.


    public Player player;
    public Enemy enemy;
    public ObstacleData obstacleData;
    public GridManager gridManager;

    private bool isMoving = false;

    void Start()
    {
        // Initialize enemy position
        Vector2Int enemyStartPosition = new Vector2Int(0, 0);
        enemy.Initialize(enemyStartPosition, gridManager, this);
    }

    void Update()
    {
        if (!isMoving && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                TileInfo tileInfo = hit.collider.GetComponent<TileInfo>();
                if (tileInfo != null)
                {
                    Vector2Int targetPosition = new Vector2Int(tileInfo.x, tileInfo.z);
                    StartCoroutine(MovePlayerToTarget(targetPosition));
                }
            }
        }
    }

    private IEnumerator MovePlayerToTarget(Vector2Int targetPosition)
    {
        isMoving = true;
        List<Vector2Int> path = FindPath(player.GetPosition(), targetPosition);
        if (path != null)
        {
            foreach (Vector2Int step in path)
            {
                player.SetPosition(step.x, step.y);
                yield return new WaitForSeconds(0.1f);
            }

            // Move the enemy after the player has moved
            Vector2Int enemyTargetPosition = GetClosestAdjacentPosition(player.GetPosition(), enemy.GetPosition());
            enemy.MoveTowards(enemyTargetPosition);
        }
        isMoving = false;
    }

    private Vector2Int GetClosestAdjacentPosition(Vector2Int playerPosition, Vector2Int enemyPosition)
    {
        List<Vector2Int> neighbors = GetNeighbors(playerPosition);
        Vector2Int closestPosition = neighbors[0];
        float closestDistance = Vector2Int.Distance(closestPosition, enemyPosition);

        foreach (Vector2Int neighbor in neighbors)
        {
            float distance = Vector2Int.Distance(neighbor, enemyPosition);
            if (distance < closestDistance)
            {
                closestPosition = neighbor;
                closestDistance = distance;
            }
        }

        return closestPosition;
    }

    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int goal)
    {
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        Dictionary<Vector2Int, Vector2Int?> cameFrom = new Dictionary<Vector2Int, Vector2Int?>();
        queue.Enqueue(start);
        cameFrom[start] = null;

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();

            if (current == goal)
            {
                List<Vector2Int> path = new List<Vector2Int>();
                while (current != start && cameFrom[current].HasValue)
                {
                    path.Add(current);
                    current = cameFrom[current].Value;
                }
                path.Add(start);
                path.Reverse();
                return path;
            }

            foreach (Vector2Int next in GetNeighbors(current))
            {
                if (!cameFrom.ContainsKey(next) && !IsObstacle(next))
                {
                    queue.Enqueue(next);
                    cameFrom[next] = current;
                }
            }
        }
        return null;
    }

    private List<Vector2Int> GetNeighbors(Vector2Int position)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>
        {
            new Vector2Int(position.x + 10, position.y),
            new Vector2Int(position.x - 10, position.y),
            new Vector2Int(position.x, position.y + 10),
            new Vector2Int(position.x, position.y - 10)
        };

        neighbors.RemoveAll(n => n.x < 0 || n.x >= gridManager.gridSize * 10 || n.y < 0 || n.y >= gridManager.gridSize * 10);
        return neighbors;
    }

    private bool IsObstacle(Vector2Int position)
    {
        int index = position.x / 10 + (position.y / 10) * gridManager.gridSize;
        return obstacleData.obstacleGrid[index];
    }
}
