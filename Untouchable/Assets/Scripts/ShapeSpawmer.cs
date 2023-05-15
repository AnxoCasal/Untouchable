using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeSpawmer : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;

    float enemy1Fr = 3;
    float enemy2Fr = 6;
    float enemy3Fr = 5;
    float enemy4Fr = 6;

    float enemy1timer = 0;
    float enemy2timer = 0;
    float enemy3timer = 0;
    float enemy4timer = 0;

    public bool lvl1;
    public bool lvl2;

    System.Random random = new System.Random();
    int yaux;
    int xaux;

    public float globalTimer;

    GameObject canvas;
    GameObject cronoText;
    float minutos = 0;
    float segundos = 0;

    bool[] flags = { true, true, true, true, true, true, true };

    private void Start()
    {
        canvas = GameObject.Find("Canvas");
        cronoText = canvas.transform.Find("Crono").gameObject;
    }

    void Update()
    {
        if (flags[6])
        {
            if (enemy1 != null)
            {
                enemy1timer += Time.deltaTime;
                if(spawn(enemy1, enemy1timer, enemy1Fr))
                {
                    enemy1timer -= enemy1timer;
                }
            };
            if (enemy2 != null)
            {
                enemy2timer += Time.deltaTime;
                if (spawn(enemy2, enemy2timer, enemy2Fr))
                {
                    enemy2timer -= enemy2timer;
                }
            };
            if (enemy3 != null)
            {
                enemy3timer += Time.deltaTime;
                if (spawn(enemy3, enemy3timer, enemy3Fr))
                {
                    enemy3timer -= enemy3timer;
                }
            };
            if (enemy4 != null)
            {
                enemy4timer += Time.deltaTime;
                if (spawn(enemy4, enemy4timer, enemy4Fr))
                {
                    enemy4timer -= enemy4timer;
                }
            };

            speedUp();
            crono();
        }
        else
        {
            canvas.transform.Find("WinPanel").gameObject.SetActive(true);
            canvas.transform.Find("WinBtn").gameObject.SetActive(true);
        }
    }

    public bool spawn(GameObject enemy, float timer, float spawnFr)
    {
        if (timer > spawnFr)
        {
            timer = 0;
            xaux = random.Next(-20, 21);

            if (xaux >= 0)
            {
                yaux = 20 - xaux;
            }
            else
            {
                yaux = 20 - (-xaux);
            }

            if (random.Next(1, 3) == 2)
            {
                yaux = -yaux;
            }

            Instantiate(enemy, new Vector2(xaux, yaux), new Quaternion());
            return true;
        }
        else
        {
            return false;
        }
    }

    public void crono()
    {
        segundos += Time.deltaTime;
        if (segundos > 60)
        {
            segundos -= 60;
            minutos += 1;
        }
        cronoText.GetComponent<Text>().text = string.Format("{0:00}:{1:00}", 2 - minutos, 60 - segundos);
        if (!flags[6])
        {
            cronoText.GetComponent<Text>().text = string.Format("{0:00}:{1:00}", 0, 0);
        }
    }

    public void speedUp()
    {
        globalTimer += Time.deltaTime;

        if (globalTimer > 30 && flags[1])
        {
            enemy2Fr = 5.2f;
            enemy1Fr = 2.3f;
            flags[1] = false;
        }
        else
        if (globalTimer > 60 && flags[2])
        {
            enemy2Fr = 5f;
            enemy1Fr = 2f;
            flags[2] = false;
        }
        else
        if (globalTimer > 90 && flags[3])
        {
            enemy2Fr = 4.8f;
            enemy1Fr = 1.8f;
            flags[3] = false;
        }
        else
        if (globalTimer > 120 && flags[4])
        {
            enemy2Fr = 4f;
            enemy1Fr = 1.5f;
            flags[4] = false;
        }
        else if (globalTimer > 150 && flags[5])
        {
            enemy2Fr = 3.5f;
            enemy1Fr = 1.25f;
            flags[5] = false;
        }
        else if (globalTimer > 180)
        {
            flags[6] = false;

            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(enemy);
            }

            if (lvl1)
            {
                PlayerPrefs.SetInt("lvl1pass",1);
                PlayerPrefs.Save();
            }

            if (lvl2)
            {
                PlayerPrefs.SetInt("lvl2pass", 1);
                PlayerPrefs.Save();
            }
        }

    }
}
