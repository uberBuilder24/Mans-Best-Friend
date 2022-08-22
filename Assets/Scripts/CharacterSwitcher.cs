using UnityEngine;

public class CharacterSwitcher : MonoBehaviour {
    public static bool personEnabled = false;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            personEnabled = !personEnabled;
        }
    }
}