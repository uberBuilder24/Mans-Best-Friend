using UnityEngine;
using UnityEngine.UI;

public class DogController : MonoBehaviour {
    [SerializeField] private float movementSpeed = 2.25f;
    [SerializeField] private Slider healthBar;
    [SerializeField, Tooltip("Front, Back, Side")] private Sprite[] idleSprites;
    private HealthSystem healthSystem;
    private SpriteRenderer spriteRend;
    private Animator anim;
    private float direction = 1f;

    void Start() {
        healthSystem = GetComponent<HealthSystem>();
        spriteRend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update() {
        if (CharacterSwitcher.personEnabled == false) {
            float movementX = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
            float movementY = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
            healthBar.value = healthSystem.health;

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
                    anim.Play("Base Layer.DogBack", 0);
                } else if (direction == 1) {
                    spriteRend.flipX = false;
                    anim.Play("Base Layer.DogSide", 0);
                } else if (direction == 2) {
                    spriteRend.flipX = false;
                    anim.Play("Base Layer.DogFront", 0);
                } else if (direction == 3) {
                    spriteRend.flipX = true;
                    anim.Play("Base Layer.DogSide", 0);
                }
            } else {
                anim.enabled = false;
                HandleIdleSprites();
            }
            
            transform.position += Vector3.right * movementX + Vector3.up * movementY;
        } else {
            anim.enabled = false;
            HandleIdleSprites();
        }
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