using UnityEngine;

public class HeadCollider : MonoBehaviour {
    [SerializeField] private int triggeredLayer;
    private SpriteRenderer spriteRend;
    private int originalLayer;

    void Start() {
        spriteRend = GetComponentInParent<SpriteRenderer>();
        originalLayer = spriteRend.sortingOrder;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        spriteRend.sortingOrder = triggeredLayer;
    }

    void OnTriggerExit2D(Collider2D collider) {
        spriteRend.sortingOrder = originalLayer;
    }
}