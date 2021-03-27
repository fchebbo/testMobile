using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testShip : MonoBehaviour
{
    public float speed = 5.0f;
    public Joystick joystick;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMove = joystick.Horizontal;
        float verticalMoveMove = joystick.Vertical;
        transform.position += new Vector3(horizontalMove, verticalMoveMove, 0f) * speed * Time.deltaTime;
    }

    public void moveShip(Vector2 direction)
        {
             
        }
}
