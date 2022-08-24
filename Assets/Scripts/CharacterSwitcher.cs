using UnityEngine;
using Cinemachine;

public class CharacterSwitcher : MonoBehaviour {
    [SerializeField] private GameObject person;
    [SerializeField] private GameObject dog;
    [SerializeField] private BoxCollider2D personHeadCollider;
    [SerializeField] private BoxCollider2D dogHeadCollider;
    [SerializeField] private CinemachineVirtualCamera virtualCam;
    public static bool personEnabled = false;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            if (!personEnabled == true) {
                virtualCam.m_Follow = person.transform;
                person.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic; // Set person to dynamic to allow collisions
                dog.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic; // Set dog to kinematic to prevent pushing
                personHeadCollider.enabled = true; // Enable the person's head collider so he can naturally walk around bushes
                dogHeadCollider.enabled = false; // Disable the dog's head collider to prevent the dog running over his face
            } else {
                virtualCam.m_Follow = dog.transform;
                person.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic; // Set person to kinematic to prevent pushing
                dog.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic; // Set dog to dynamic to allow collisions
                personHeadCollider.enabled = false; // Disable the person's head collider to prevent the dog running over his face
                dogHeadCollider.enabled = true; // Enable the dog's head collider so he can naturally walk around bushes
            }
            personEnabled = !personEnabled;
        }
    }
}