using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour {
    [Header("Movement")]
    [SerializeField] private GameObject alerted;
    [SerializeField] private Vector2 idlingDirection = new Vector2(0f, 1f);
    [SerializeField, Tooltip("Front, Back, Side")] private Sprite[] idleSprites;
    private Transform target;
    private AIPath aiPath;
    private AIDestinationSetter destinationSetter;
    private SpriteRenderer spriteRend;
    private Animator anim;
    private float direction = 2f;
    private bool knowsPlayer = false;

    [Header("Shooting")]
    [SerializeField] private float fireRate = 1f;
    private GameObject bullet;
    private float nextFireTime = 0f;

    private bool idleWallCollision;

    void Start() {
        aiPath = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        spriteRend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        
        target = GameObject.Find("Person").transform;
        bullet = GameObject.Find("Bullet");
    }

    void Update() {
        idleWallCollision = false;

        float movementX;
        float movementY;
        if (knowsPlayer) {
            movementX = aiPath.desiredVelocity.x * Time.deltaTime;
            movementY = aiPath.desiredVelocity.y * Time.deltaTime;
        } else {
            movementX = (aiPath.maxSpeed / 2) * idlingDirection.x * Time.deltaTime;
            movementY = (aiPath.maxSpeed / 2) * idlingDirection.y * Time.deltaTime;
        }

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

        if (IsOnScreen() && CharacterSwitcher.personEnabled == true && knowsPlayer == false) {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            nextFireTime = Time.time + 1f;
            alerted.SetActive(true);
            knowsPlayer = true;
            destinationSetter.target = target;
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

            if (Time.time >= nextFireTime && knowsPlayer) {
                nextFireTime = Time.time + 1f / fireRate;
                if (alerted.activeSelf == true) {
                    alerted.SetActive(false);
                }
                Shoot();
            }
        }
        
        transform.position += Vector3.right * movementX + Vector3.up * movementY;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 6 && knowsPlayer == false && idleWallCollision == false) {
            idlingDirection = -idlingDirection;
            idleWallCollision = true;
        }
    }

    void Shoot() {
        Vector3 dir = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle < 0) { angle += 360; }

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

    bool IsOnScreen() {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        return (screenPos.x > 0f && screenPos.x < Screen.width && screenPos.y > 0f && screenPos.y < Screen.height);
    }

    void HandleIdleSprites() {
        Vector3 dir = (target.position - transform.position).normalized;
        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90f;
        if (angle < 0) { angle += 360; }

        // Point towards the person
        if (angle >= 315 || angle <= 44) {
            direction = 0;
        } else if (angle >= 225 && angle <= 314) {
            direction = 1;
        } else if (angle >= 135 && angle <= 224) {
            direction = 2;
        } else if (angle >= 45 && angle <= 134) {
            direction = 3;
        }

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