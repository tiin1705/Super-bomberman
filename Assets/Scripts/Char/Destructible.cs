using UnityEngine;
using UnityEngine.Tilemaps;


public class Destructible : MonoBehaviour
{
    public Tilemap tilemap;
    private TileBase[,] previousTiles;


    [Header("ItemSpawn")]
    [Range(0f, 1f)]
    public float ItemSpawnChange = 0.2f;
    public GameObject[] spawnableItem;


    void Start()
    {
        CachePreviousTiles();
    }
    void CachePreviousTiles()
    {
        BoundsInt bounds = tilemap.cellBounds;
        previousTiles = new TileBase[bounds.size.x, bounds.size.y];

        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                Vector3Int cellPos = new Vector3Int(x, y, 0);
                previousTiles[x - bounds.min.x, y - bounds.min.y] = tilemap.GetTile(cellPos);
            }
        }
    }
    void Update()
    {
        CheckTileChanges();
    }

    void CheckTileChanges()
    {
        BoundsInt bounds = tilemap.cellBounds;

        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                Vector3Int cellPos = new Vector3Int(x, y, 0);
                TileBase currentTile = tilemap.GetTile(cellPos);

                if (currentTile == null && previousTiles[x - bounds.min.x, y - bounds.min.y] != null)
                {
                    Vector3 worldPos = tilemap.GetCellCenterWorld(cellPos);

                    SpawnItem(worldPos);
                }

                previousTiles[x - bounds.min.x, y - bounds.min.y] = currentTile;
            }
        }
    }




    private void SpawnItem(Vector3 pos)
    {
        if (spawnableItem.Length > 0 && Random.value < ItemSpawnChange)
        {
            int randomIndex = Random.Range(0, spawnableItem.Length);
            Instantiate(spawnableItem[randomIndex], pos, Quaternion.identity);
        }
    }
   
   
}
