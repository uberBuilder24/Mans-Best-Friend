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
                person.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic; // Set person to dynamic to allow collisions
                dog.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic; // Set dog to kinematic to prevent pushing
            } else {
                virtualCam.m_Follow = dog.transform;
                person.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic; // Set person to kinematic to prevent pushing
                dog.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic; // Set dog to dynamic to allow collisions
            }
            personEnabled = !personEnabled;
        }
    }
}