using UnityEngine;
using Cinemachine;

public class CharacterSwitcher : MonoBehaviour {
    [SerializeField] private GameObject person;
    [SerializeField] private GameObject dog;
    [SerializeField] private CinemachineVirtualCamera virtualCam;
    public static bool personEnabled = false;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            if (!personEnabled == true) {
                virtualCam.m_Follow = person.transform;
                person.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                dog.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            } else {
                virtualCam.m_Follow = dog.transform;
                person.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                dog.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
            personEnabled = !personEnabled;
        }
    }
}