using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingOfDeath : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public float minSpeed;
    public float maxSpeed;

    float speed;

    Vector2 targetPosition;
    // Start is called before the first frame update

    private float initializationTime;

    public float secondsToMaxDifficulty;
    void Start()
    {
        targetPosition = GetRandomPosition();
        initializationTime = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Vector2)transform.position != targetPosition)
        {
            speed = Mathf.Lerp(minSpeed, maxSpeed, GetDifficultyPercent());
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            targetPosition = GetRandomPosition();
        }
    }

    Vector2 GetRandomPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        return new Vector2(randomX, randomY);
    }

    float GetDifficultyPercent ()
    {
        return Mathf.Clamp01((Time.timeSinceLevelLoad - initializationTime) / secondsToMaxDifficulty);
    }
}
