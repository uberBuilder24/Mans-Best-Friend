using UnityEngine;

public class BulletHandler : MonoBehaviour {
    private AudioSource audioSource;
    private bool isClone;

    void OnEnable() {
        audioSource = GetComponent<AudioSource>();
        isClone = gameObject.name.Contains("Clone");

        if (isClone) {
            audioSource.Play();
        }
    }

    void Update() {
        if (isClone) {
            transform.position += transform.right * 5f * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (isClone) {
            if (!collider.gameObject.name.Contains("Bullet") && !collider.gameObject.name.Contains(transform.parent.name)) {
                Destroy(gameObject);
            }
        }
    }
}