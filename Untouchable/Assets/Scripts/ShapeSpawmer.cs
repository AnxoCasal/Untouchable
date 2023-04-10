using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSpawmer : MonoBehaviour
{
    public GameObject shape;

    float timer;
    public float spawn_interval;
    System.Random random = new System.Random();
    int yaux;
    int xaux;

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > spawn_interval)
        {
            timer = 0;
            xaux = random.Next(-10,11);
            
            if(xaux >= 0) {
                yaux =  10 - xaux;
            } 
            else
            {
                yaux = 10 -(- xaux);
            }

            if(random.Next(1,3) == 2) {
                yaux = -yaux;
            }

            Instantiate(shape, new Vector2(xaux,yaux), new Quaternion());
        }
    }
}
