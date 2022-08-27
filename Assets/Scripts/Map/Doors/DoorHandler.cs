using UnityEngine;

public class DoorHandler : MonoBehaviour {
    public DoorManager doorPlacement;
    public int doorId;

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            if (doorId > doorPlacement.level1Conditions.Length - 1) {
                Debug.Log("Last Door!");
            } else {
                collision.gameObject.transform.position = doorPlacement.level1Destinations[doorId];
            }
        }
    }
}