using UnityEngine;

public class MapGenerator : MonoBehaviour {
    [SerializeField] private Texture2D worldMap;
    [SerializeField] private Texture2D buildingMap;
    [SerializeField] private float tileWidth = 64f;
    [SerializeField] private float tileHeight = 64f;
    public ColourToTerrain[] terrainMappings;
    public ColourToTerrain[] buildingMappings;
    public TerrainToDetails[] detailMappings;

    void Start() {
        GenerateMap();
    }

    void GenerateMap() {
        for (int x = 0; x < worldMap.width; x++) {
            for (int y = 0; y < worldMap.height; y++) {
                GenerateTile(x, y);
            }
        }
    }

    void GenerateTile(int x, int y) {
        Color pixelColour = worldMap.GetPixel(x, y);
        if (pixelColour.a == 0) { return; } // Transparent Pixel

        foreach (ColourToTerrain terrainMapping in terrainMappings) {
            if (terrainMapping.colour.Equals(pixelColour)) {
                Vector2 position = new Vector2(x, y);

                if (x != 0) {
                    position.x = (tileWidth / 100) * (float)x;
                }
                if (y != 0) {
                    position.y = (tileHeight / 100) * (float)y;
                }

                bool buildingPixel = HandleBuildings(x, y);
                if (buildingPixel == false) { HandleDetails(terrainMapping.prefab.name, position); }
                Instantiate(terrainMapping.prefab, position, Quaternion.identity, transform);
            }
        }
    }

    bool HandleBuildings(int x, int y) {
        Color pixelColour = buildingMap.GetPixel(x, y);
        if (pixelColour.a == 0) { return false; } // Transparent Pixel

        foreach (ColourToTerrain buildingMapping in buildingMappings) {
            if (buildingMapping.colour.Equals(pixelColour)) {
                Vector2 position = new Vector2(x, y);
                position.x *= tileWidth / 100;
                position.y *= tileHeight / 100 - 0.2f;

                Instantiate(buildingMapping.prefab, position, Quaternion.identity, transform);
                return true;
            }
        }
        return false;
    }

    void HandleDetails(string terrain, Vector2 position) {
        int spawnDetail = Random.Range(0, 500);
        if (spawnDetail <= 10) {
            foreach (TerrainToDetails detailMapping in detailMappings) {
                if (detailMapping.terrain == terrain) {
                    Instantiate(detailMapping.prefab, new Vector2(position.x, position.y), Quaternion.identity, transform);
                }
            }
        }
    }
}