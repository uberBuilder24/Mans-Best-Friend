using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour {
    [SerializeField] private AIPath aiPath;
    [SerializeField, Tooltip("Front, Back, Side")] private Sprite[] idleSprites;
    private SpriteRenderer spriteRend;
    private Animator anim;
    private float direction = 2f;

    void Start() {
        spriteRend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update() {
        float movementX = aiPath.desiredVelocity.x * Time.deltaTime;
        float movementY = aiPath.desiredVelocity.y * Time.deltaTime;

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
            anim.enabled = true;
            if (direction == 0) {
                spriteRend.flipX = false;
                anim.Play("Base Layer.EnemyBack", 0);
            } else if (direction == 1) {
                spriteRend.flipX = false;
                anim.Play("Base Layer.EnemySide", 0);
            } else if (direction == 2) {
                spriteRend.flipX = false;
                anim.Play("Base Layer.EnemyFront", 0);
            } else if (direction == 3) {
                spriteRend.flipX = true;
                anim.Play("Base Layer.EnemySide", 0);
            }
        } else {
            anim.enabled = false;
            HandleIdleSprites();
        }
        
        transform.position += Vector3.right * movementX + Vector3.up * movementY;
    }

    void HandleIdleSprites() {
        if (direction == 0) {
            spriteRend.flipX = false;
            spriteRend.sprite = idleSprites[1];
        } else if (direction == 1) {
            spriteRend.flipX = false;
            spriteRend.sprite = idleSprites[2];
        } else if (direction == 2) {
            spriteRend.flipX = false;
            spriteRend.sprite = idleSprites[0];
        } else if (direction == 3) {
            spriteRend.flipX = true;
            spriteRend.sprite = idleSprites[2];
        }
    }
}