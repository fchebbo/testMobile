using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMainShotController : MonoBehaviour
{
    public float speed;
    private Vector3 playerPosition;

    private Vector3 direction;
    private Transform selfTransform;
    // Start is called before the first frame update
    void Start()
    {
        LockOnToPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void LockOnToPlayer()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        selfTransform = GetComponent<Transform>();
        direction = (playerPosition - transform.position).normalized;
    }
    private void OnEnable()
    {
        LockOnToPlayer();
        Invoke("Disable", 3f);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
}
