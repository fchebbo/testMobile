using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBoiRepeat : MonoBehaviour
{
    private BoxCollider2D bc2d;
    private Rigidbody2D rb;
    private float speed = -5f;
    private float width;
    // Start is called before the first frame update
    void Start()
    {
        bc2d = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        width = bc2d.size.x;
        rb.velocity = new Vector2(speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -width * 2)
        {
            Reposition();
        }
    }

    private void Reposition()
    {
        print((width * 4f).ToString());
        Vector2 vector = new Vector2(width * 4f, 0);
        transform.position = (Vector2)transform.position + vector;

    }
}