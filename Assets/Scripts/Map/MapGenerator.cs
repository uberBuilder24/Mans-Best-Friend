using UnityEngine;

public class MapGenerator : MonoBehaviour {
    [Header("Dimensions")]
    [SerializeField] private float tileWidth = 64f;
    [SerializeField] private float tileHeight = 64f;

    [Header("Terrain")]
    [SerializeField] private Texture2D terrainMap;
    public ColourToPrefab[] terrainMappings;

    [Header("Buildings")]
    [SerializeField] private Texture2D buildingMap;
    public ColourToPrefab[] buildingMappings;
    private int doorCount = 0;

    [Header("Details")]
    [SerializeField] private Texture2D detailMap;
    public ColourToPrefab[] detailMappings;
    
    void Awake() {
        GenerateMap();
        AstarPath.active.Scan();
    }

    void GenerateMap() {
        for (int x = 0; x < terrainMap.width; x++) {
            for (int y = 0; y < terrainMap.height; y++) {
                GenerateTile(x, y);
            }
        }
    }

    void GenerateTile(int x, int y) {
        Color pixelColour = terrainMap.GetPixel(x, y);
        if (pixelColour.a == 0) { return; } // Transparent Pixel

        foreach (ColourToPrefab terrainMapping in terrainMappings) {
            if (terrainMapping.colour.Equals(pixelColour)) {
                Vector2 position = new Vector2(x, y);

                if (x != 0) {
                    position.x = (tileWidth / 100) * (float)x;
                }
                if (y != 0) {
                    position.y = (tileHeight / 100) * (float)y;
                }

                HandleBuildings(x, y);
                HandleDetails(x, y);
                Instantiate(terrainMapping.prefab, position, Quaternion.identity, transform);
            }
        }
    }

    void HandleBuildings(int x, int y) {
        Color pixelColour = buildingMap.GetPixel(x, y);
        if (pixelColour.a == 0) { return; } // Transparent Pixel

        foreach (ColourToPrefab buildingMapping in buildingMappings) {
            if (buildingMapping.colour.Equals(pixelColour)) {
                Vector2 position = new Vector2(x, y);
                position.x *= tileWidth / 100;
                position.y *= tileHeight / 100 - 0.2f; // Subtract 0.2 because the vertical walls are 20 pixels taller

                GameObject wall = Instantiate(buildingMapping.prefab, position, Quaternion.identity, transform);

                if (buildingMapping.prefab.name.Contains("Door")) {
                    wall.GetComponent<DoorHandler>().doorId = doorCount;
                    doorCount++;
                }
            }
        }
    }

    void HandleDetails(int x, int y) {
        Color pixelColour = detailMap.GetPixel(x, y);
        if (pixelColour.a == 0) { return; } // Transparent Pixel

        foreach (ColourToPrefab detailMapping in detailMappings) {
            if (detailMapping.colour.Equals(pixelColour)) {
                Vector2 position = new Vector2(x, y);
                position.x *= tileWidth / 100;
                position.y *= tileHeight / 100;

                Instantiate(detailMapping.prefab, position, Quaternion.identity, transform);
            }
        }
    }
}