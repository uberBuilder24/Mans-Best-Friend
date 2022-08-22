using UnityEngine;

public class MapGenerator : MonoBehaviour {
    [SerializeField] private Texture2D map;
    public ColourToTerrain[] terrainMappings;
    public TerrainToDetails[] detailMappings;

    void Start() {
        GenerateMap();
    }

    void GenerateMap() {
        for (int x = 0; x < map.width; x++) {
            for (int y = 0; y < map.height; y++) {
                GenerateTile(x, y);
            }
        }
    }

    void GenerateTile(int x, int y) {
        Color pixelColour = map.GetPixel(x, y);

        if (pixelColour.a == 0) { return; } // Transparent Pixel

        foreach (ColourToTerrain terrainMapping in terrainMappings) {
            if (terrainMapping.colour.Equals(pixelColour)) {
                Vector2 position = new Vector2(x, y);

                if (x != 0) {
                    position.x = 1.235f * (float)x;
                }
                if (y != 0) {
                    position.y = 1.435f * (float)y;
                }

                int spawnDetail = Random.Range(0, 100);
                if (spawnDetail <= 10) {
                    foreach (TerrainToDetails detailMapping in detailMappings) {
                        if (detailMapping.terrain == terrainMapping.prefab.name) {
                            Instantiate(detailMapping.prefab, new Vector2(position.x, position.y + Random.Range(0.25f, 0.75f)), Quaternion.identity, transform);
                        }
                    }
                }
                
                Instantiate(terrainMapping.prefab, position, Quaternion.identity, transform);
            }
        }
    }
}