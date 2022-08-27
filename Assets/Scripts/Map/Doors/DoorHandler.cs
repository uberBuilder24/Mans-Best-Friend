using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorHandler : MonoBehaviour {
    [SerializeField] private DoorManager doorPlacement;
    private CharacterSwitcher characterSwitcher;
    public int doorId;
    private int charactersFinished = 0;

    void Start() {
        characterSwitcher = GameObject.Find("Character Switcher").GetComponent<CharacterSwitcher>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            if (gameObject.name.Contains("Final")) {
                charactersFinished++;
                if (charactersFinished < 2) {
                    characterSwitcher.SwitchCharacters();
                } else {
                    charactersFinished = 0;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            } else {
                if (ScoreSystem.killCount >= doorPlacement.level1Conditions[doorId]) {
                    collision.gameObject.transform.position = doorPlacement.level1Destinations[doorId];
                }
            }
        }
    }
}