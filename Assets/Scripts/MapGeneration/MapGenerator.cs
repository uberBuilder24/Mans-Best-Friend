using UnityEngine;

public class MapGenerator : MonoBehaviour {
    [SerializeField] private Texture2D map;
    public ColourToPrefab[] terrainMappings;

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

        foreach (ColourToPrefab terrainMapping in terrainMappings) {
            if (terrainMapping.colour.Equals(pixelColour)) {
                Vector2 position = new Vector2(x, y);
                if (x != 0) {
                    position.x = 1.235f * (float)x;
                }
                if (y != 0) {
                    position.y = 1.435f * (float)y;
                }
                
                Instantiate(terrainMapping.prefab, position, Quaternion.identity, transform);
            }
        }
    }
}