using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour {
    [SerializeField] private float startingHealth = 10f;
    [SerializeField] private float bulletDamage = 4f;
    private float health;

    void Awake() {
        health = startingHealth;
    }
    
    void Update() {
        if (health <= 0f) {
            if (gameObject.tag == "Player") {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            } else {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.name.Contains("Bullet") && collider.gameObject.transform.parent.name != gameObject.name) {
            health -= bulletDamage;
        }
    }
}