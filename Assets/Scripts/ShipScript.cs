using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ShipScript : MonoBehaviour
{
    public GameObject RestartPanel;
    public GameObject deathEffect;
    public ParticleSystem moveEffect;
    ParticleSystem moveEffectz;
    private AudioSource ExplosionSoundSource;

    public float speed;

    bool isMoving = false;
    Vector3 touchPosition, whereToMove;
    float previousDistanceToTouchPos, currentDistanceToTouchPos;
    Touch touch;
    
    // public Joystick joystick;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ExplosionSoundSource = GetComponent<AudioSource>();
        
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
        //float horizontalMove = joystick.Horizontal;
        //float verticalMoveMove = joystick.Vertical;
        //transform.position += new Vector3(horizontalMove, verticalMoveMove, 0f) * speed * Time.deltaTime;
        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), 0);
    }

    private void FixedUpdate()
    {
        
    }

    public void GameOver()
    {
        CanvasMaster.instance.playGameOverMusic();
        RestartPanel.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "RingOfDeath" && CanvasMaster.instance.getGameOver()==false)
        {
            CanvasMaster.instance.stopAllMusic();
            ExplosionSoundSource.Play();
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            isMoving = false;
            rb.velocity = Vector2.zero;
            rb.gravityScale = 4f;
            //GetComponent<Renderer>().enabled = false;
            //Destroy(joystick);
            //joystick.enabled = false;
            CanvasMaster.instance.doGameOver();
           
        }
    }
}
