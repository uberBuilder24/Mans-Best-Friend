using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DoorHandler : MonoBehaviour {
    [SerializeField] private DoorManager doorPlacement;
    private TextMeshProUGUI errorMessage;
    private CharacterSwitcher characterSwitcher;
    public int doorId;
    private int charactersFinished = 0;

    void Start() {
        errorMessage = GameObject.Find("Error Message").GetComponent<TextMeshProUGUI>();
        characterSwitcher = GameObject.Find("Character Switcher").GetComponent<CharacterSwitcher>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            if (gameObject.name.Contains("Final")) {
                charactersFinished++;
                if (charactersFinished < 2) {
                    collision.gameObject.SetActive(false);
                    characterSwitcher.SwitchCharacters();
                } else {
                    charactersFinished = 0;
                    CharacterSwitcher.personEnabled = false;

                    if (SceneManager.GetActiveScene().buildIndex + 1 <= SceneManager.sceneCount) {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    } else {
                        ScoreSystem.killCount = 0;
                        SceneManager.LoadScene(0);
                    }
                }
            } else {
                if (ScoreSystem.killCount >= doorPlacement.level1Conditions[doorId]) {
                    errorMessage.text = "";
                    collision.gameObject.transform.position = doorPlacement.level1Destinations[doorId];
                } else {
                    errorMessage.text = "You missed an enemy!";
                }
            }
        }
    }
}