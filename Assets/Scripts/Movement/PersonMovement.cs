using UnityEngine;

public class PersonMovement : MonoBehaviour {
    [SerializeField] private float movementSpeed = 2.25f;
    [SerializeField, Tooltip("Front, Back, Side")] private Sprite[] idleSprites;
    private SpriteRenderer spriteRend;
    private Animator anim;
    private float direction = 2f;

    void Start() {
        spriteRend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update() {
        if (CharacterSwitcher.personEnabled == true) {
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
                anim.enabled = true;
                if (direction == 0) {
                    spriteRend.flipX = false;
                    anim.Play("Base Layer.PersonBack", 0);
                } else if (direction == 1) {
                    spriteRend.flipX = false;
                    anim.Play("Base Layer.PersonSide", 0);
                } else if (direction == 2) {
                    spriteRend.flipX = false;
                    anim.Play("Base Layer.PersonFront", 0);
                } else if (direction == 3) {
                    spriteRend.flipX = true;
                    anim.Play("Base Layer.PersonSide", 0);
                }
            } else {
                anim.enabled = false;
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
            
            transform.position += Vector3.right * movementX + Vector3.up * movementY;
        } else {
            anim.enabled = false;
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
}