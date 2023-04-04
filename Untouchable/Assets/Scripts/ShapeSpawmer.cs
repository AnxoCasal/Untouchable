using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSpawmer : MonoBehaviour
{
    public GameObject shape;

    float timer;
    public float spawn_interval;

    new Vector2[] positions = { new Vector2(0, 4.5f), new Vector2(2, 3.7f), new Vector2(4, 2), new Vector2(4.5f,0), new Vector2(0, -4.5f), new Vector2(-4.5f, 0),
    new Vector2(-2, 3.7f), new Vector2(-4, 2), new Vector2(-4, 2),
    new Vector2(2, -3.7f), new Vector2(4, -2), new Vector2(4, -2),
    new Vector2(-2, -3.7f), new Vector2(-4, -2), new Vector2(-4, -2)};

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > spawn_interval)
        {
            timer = 0;
            Instantiate(shape, positions[Random.Range(0,positions.Length-1)]*2, new Quaternion());
        }
    }
}
