using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenScript : MonoBehaviour
{
    public ParticleSystem PoofEffect;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    float SecondsLeftTillPoof;
    // Start is called before the first frame update
    void Start()
    {
        SecondsLeftTillPoof = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        SecondsLeftTillPoof -= Time.deltaTime;
        if (SecondsLeftTillPoof < 0)
        {
            SecondsLeftTillPoof = Random.Range(0.75f,2);
            playEffectAtRandomSpot(PoofEffect);
        }
    }
    void playEffectAtRandomSpot(ParticleSystem particleSystem)
    {
        ParticleSystem poof = Instantiate(PoofEffect) as ParticleSystem;
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        poof.transform.position = new Vector2(randomX, randomY);
        poof.Play();
    }
}
