using UnityEngine;

public class PersonController : MonoBehaviour {
    [Header("Movement")]
    [SerializeField] private float movementSpeed = 2.25f;
    [SerializeField, Tooltip("Front, Back, Side")] private Sprite[] idleSprites;
    private SpriteRenderer spriteRend;
    private Animator anim;
    private float direction = 2f;

    [Header("Shooting")]
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private GameObject bullet;
    private float nextFireTime = 0f;

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
                HandleIdleSprites();

                if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime) {
                    nextFireTime = Time.time + 1f / fireRate;
                    Vector3 aim = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Shoot(new Vector3(aim.x, aim.y, 0f));
                }
            }
            
            transform.position += Vector3.right * movementX + Vector3.up * movementY;
        } else {
            anim.enabled = false;
            HandleIdleSprites();
        }
    }

    void Shoot(Vector3 target) {
        Vector3 dir = (target - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle < 0) { angle += 360; }

        // Point the player towards the target
        if (Mathf.Abs(target.x - transform.position.x) > Mathf.Abs(target.y - transform.position.y)) {
            if (target.x > transform.position.x) {
                direction = 1;
            } else {
                direction = 3;
            }
        } else {
            if (target.y > transform.position.y) {
                direction = 0;
            } else {
                direction = 2;
            }
        }

        // Set the bullet to spawn near the gun
        Vector3 bulletSpawn = transform.position;
        if (direction == 0) {
            bulletSpawn = new Vector3(0.22f, 0.035f, 0f);
        } else if (direction == 1) {
            bulletSpawn = new Vector3(0.1f, -0.07f, 0f);
        } else if (direction == 2) {
            bulletSpawn = new Vector3(-0.06f, -0.08f, 0f);
        } else if (direction == 3) {
            bulletSpawn = new Vector3(-0.1f, -0.07f, 0f);
        }

        Instantiate(bullet, transform.position + bulletSpawn, Quaternion.Euler(0f, 0f, angle), transform);
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