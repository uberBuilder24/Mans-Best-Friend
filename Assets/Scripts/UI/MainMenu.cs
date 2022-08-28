using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    void Start() {
        playButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(ExitGame);
    }

    public void StartGame() {
        CharacterSwitcher.personEnabled = true;
        SceneManager.LoadScene(1);
    }

    public void ExitGame() {
        Application.Quit();
    }
}