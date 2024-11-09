using UnityEngine;
using UnityEngine.Tilemaps;

public class Tilemap_Setting : MonoBehaviour
{
    public enum TileType
    {
        Path,      
        Obstacle, 
        Background
    }

    public TileType tileType;

    public bool IsWalkable()
    {
        return tileType == TileType.Path;  // 길(Path) 타입만 지나갈 수 있음
    }
}

