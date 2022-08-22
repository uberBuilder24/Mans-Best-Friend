using UnityEngine;

public class DogMovement : MonoBehaviour {
    [SerializeField] private float movementSpeed = 2.25f;
    [SerializeField, Tooltip("Front, Back, Side")] private Animation[] animations = new Animation[3];
    private float direction = 0f;

    void Update() {
        float movementX = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        float movementY = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;

        if (movementY > 0.01f) {
            direction = 0;
        }
        if (movementY < -0.01f) {
            direction = 2;
        }
        if (movementX > 0.01f) {
            direction = 1;
        }
        if (movementX < -0.01f) {
            direction = 3;
        }
        
        if (Mathf.Abs(movementX) > 0.01f || Mathf.Abs(movementY) > 0.01f) {
            if (direction == 0) {
                Debug.Log("Up Animation");
            } else if (direction == 1) {
                Debug.Log("Right Animation");
            } else if (direction == 2) {
                Debug.Log("Down Animation");
            } else if (direction == 3) {
                Debug.Log("Left Animation");
            }
        }
        
        transform.position += Vector3.right * movementX + Vector3.up * movementY;
    }
}