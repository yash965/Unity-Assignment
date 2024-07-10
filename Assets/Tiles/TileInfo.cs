using UnityEngine;

public class TileInfo : MonoBehaviour
{
    // This is a container info for the tile coordinates. This has getter and setter method.
    // I have multiplied by 10 because I have taken the size of the tile as 10x10 (x and z coordinates).

    public int x, z;

    public void SetPosition(int x, int z)
    {
        this.x = x*10;
        this.z = z*10;
    }

    public Vector2 GetPosition()
    {
        return new Vector2(x, z);
    }
}
