using UnityEngine;
using Cinemachine;

public class CharacterSwitcher : MonoBehaviour {
    [SerializeField] private Transform person;
    [SerializeField] private Transform dog;
    [SerializeField] private CinemachineVirtualCamera virtualCam;
    public static bool personEnabled = false;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            personEnabled = !personEnabled;
            if (personEnabled == true) {
                virtualCam.m_Follow = person;
            } else {
                virtualCam.m_Follow = dog;
            }
        }
    }
}