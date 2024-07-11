using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour, AI
{
    // This script is for the enemy to follow the player. What it does is, it gets a nearest neighbouring position of player
    // and move towards it.


    private Vector2Int position;
    private GridManager gridManager;
    private GridPathFinding pathfinding;

    public void Initialize(Vector2Int startPosition, GridManager gridManager, GridPathFinding pathfinding)
    {
        this.position = startPosition;
        this.gridManager = gridManager;
        this.pathfinding = pathfinding;
        transform.position = new Vector3(startPosition.x, 0.5f, startPosition.y);
    }

    public void MoveTowards(Vector2Int targetPosition)
    {
        List<Vector2Int> path = pathfinding.FindPath(position, targetPosition);

        StartCoroutine(ChangePos(path));
    }

    private IEnumerator ChangePos(List<Vector2Int> path)
    {
        foreach (Vector2Int pos in path)
        {
            if (path != null && path.Count > 1)
            {
                position = pos; 
                transform.position = new Vector3(position.x, 0.5f, position.y);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    public Vector2Int GetPosition()
    {
        return position;
    }
}
