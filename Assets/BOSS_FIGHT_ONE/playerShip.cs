using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShip : MonoBehaviour
{
    // defines how fast the ship can move
    public float speed;

    public ObjectPooler playerBulletPooler;
    Touch touch;
    public Transform firePosition;
    float previousDistanceToTouchPos, currentDistanceToTouchPos;
    public ParticleSystem moveEffect;
    public AudioSource shootSound;
    bool isMoving = false;
    Vector3 touchPosition, whereToMove;
    // Start is called before the first frame update
    Rigidbody2D rb;

    private bool isAlive = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Shoot after every 1 second
        InvokeRepeating("Shoot", 2f, .25f);
    }

    void Shoot()
    {
        GameObject bullet = playerBulletPooler.getPooledObject();
        if (isAlive && bullet == null)
        {
            return;
        }
        bullet.transform.position = firePosition.position;
        bullet.transform.rotation = firePosition.rotation;
        shootSound.Play();
        bullet.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
            currentDistanceToTouchPos = (touchPosition - transform.position).magnitude;

        if (Input.touchCount > 0 && !CanvasMaster.instance.getGameOver())
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                print("MOVING");
                touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0;

                ParticleSystem moveEffectz = Instantiate(moveEffect)
                as ParticleSystem;
                moveEffectz.transform.position = touchPosition;
                moveEffectz.Play();

                previousDistanceToTouchPos = 0;
                currentDistanceToTouchPos = 0;
                isMoving = true;

                whereToMove = (touchPosition - transform.position).normalized;

                rb.velocity = new Vector2(whereToMove.x * speed, whereToMove.y * speed);
            }
        }

        if (currentDistanceToTouchPos > previousDistanceToTouchPos)
        {
            isMoving = false;
            rb.velocity = Vector2.zero;
        }

        if (isMoving)
        {
            previousDistanceToTouchPos = (touchPosition - transform.position).magnitude;
        }

    }
}
