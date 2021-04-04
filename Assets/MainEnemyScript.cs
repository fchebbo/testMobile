using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainEnemyScript : MonoBehaviour
{
    private float bossSpeed = 3f;
    private float XfinalPos = 7.5f;
    private Vector2 targetPosition;
    public AudioSource hitSound;
    public GameObject hitEffect;
    public Text bossHPText;
    public float hitPoints;
    public ObjectPooler mainShotPooler;
    public Transform mainShotPosition;
    public float TimeBetweenMainShots;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = new Vector2(XfinalPos, 0f);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, bossSpeed);
        updateBossHP(0);
        InvokeRepeating("ShootMain", 3f, TimeBetweenMainShots);
    }

    //Shoots the main projectile at the player (the thing that keeps the player on his toes!)
    void ShootMain()
    {
        GameObject bullet = mainShotPooler.getPooledObject();
        if (bullet == null)
        {
            return;
        }
        bullet.transform.position = mainShotPosition.position;
        bullet.transform.rotation = mainShotPosition.rotation;
        bullet.SetActive(true);
    }

    // Update is called once per frame
    // In here we update the boss's position
    void Update()
    {
        if ((Vector2)transform.position != targetPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, bossSpeed * Time.deltaTime);
        }
        else
        {
            targetPosition = GetBossTargetPosition();
        }
        
    }

    Vector2 GetBossTargetPosition()
    {
        float randomY = Random.Range(-2.75f, 2.75f);
        return new Vector2(XfinalPos, randomY);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.tag == "PlayerBullet" && CanvasMaster.instance.getGameOver() == false)
        {
            collision.gameObject.SetActive(false);
            hitSound.Play();
            Instantiate(hitEffect, collision.transform.position, Quaternion.identity);
            updateBossHP(-5);
            //Insaniate(hitEffect, collision.transform.position, Rot)
            //GetComponent<Renderer>().enabled = false;
            //Destroy(joystick);
            //joystick.enabled = false;
            //CanvasMaster.instance.doGameOver();

        }
    }

    void updateBossHP(float hp)
    {
        hitPoints += hp;
        bossHPText.text = "BOSS LIFE: " + hitPoints;
    }
}
