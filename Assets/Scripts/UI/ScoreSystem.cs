using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI killCountText;
    public static int killCount = 0;

    void Update() {
        killCountText.text = "Kill Count: " + killCount;
    }
}