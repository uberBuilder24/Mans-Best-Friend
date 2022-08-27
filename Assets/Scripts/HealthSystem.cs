using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour {
    [SerializeField] private float startingHealth = 10f;
    [SerializeField] private float bulletDamage = 4f;
    public float health;

    void Awake() {
        health = startingHealth;
    }
    
    void Update() {
        if (health <= 0f) {
            if (gameObject.tag == "Player") {
                ScoreSystem.killCount = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            } else {
                ScoreSystem.killCount++;
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