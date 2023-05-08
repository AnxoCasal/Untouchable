using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriangleMove : MonoBehaviour
{

    [Range(0f, 100f)]
    public float moveSpeed;

    [Range(0f, 10f)]
    public float topSpeed;

    Rigidbody2D body;

    public int Hp;

    float dash_cd = 0f;
    bool shield_active = false;
    float shield_cd = 0f;
    float shield_duration = 1f;
    public float ultimate_cd = 20f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {


        // CHECK DE INPUTS

        gameObject.GetComponent<Animator>().SetBool("Moving", false);

        if (Input.GetKey(KeyCode.W))
        {
            body.AddForce(new Vector2(0, moveSpeed * Time.deltaTime), ForceMode2D.Impulse);
            gameObject.GetComponent<Animator>().SetBool("Moving", true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            body.AddForce(new Vector2(0, -moveSpeed * Time.deltaTime), ForceMode2D.Impulse);
            gameObject.GetComponent<Animator>().SetBool("Moving", true);
        }

        if (Input.GetKey(KeyCode.A))
        {
            body.AddForce(new Vector2(-moveSpeed * Time.deltaTime, 0), ForceMode2D.Impulse);
            gameObject.GetComponent<Animator>().SetBool("Moving", true);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            body.AddForce(new Vector2(moveSpeed * Time.deltaTime, 0), ForceMode2D.Impulse);
            gameObject.GetComponent<Animator>().SetBool("Moving", true);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        dash_cd -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && dash_cd < 0)
        {
            dash_cd = 2f;
            dash();
        }

        shield_cd -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Q) && shield_cd < 0)
        {
            shield_cd = 6f;
            shield();
        }

        if (shield_active)
        {
            GetComponent<Renderer>().material.color = Color.cyan;
            shield_duration -= Time.deltaTime;
            if(shield_duration < 0)
            {
                shield_active = false;
            }
        } else
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }



        ultimate_cd -= Time.deltaTime;
        if (Input.GetKey(KeyCode.E) && ultimate_cd < 0)
        {
            ultimate_cd = 20f;
            ultimate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!shield_active)
        {
            Hp--;
        } else
        {
            shield_active = false;
        }
    }

    private void dash()
    {
        Vector2 impulse = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            impulse.y = 10f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            impulse.y = -10f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            impulse.x = -10f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            impulse.x = 10f;
        }

        body.velocity = impulse;
    }

    private void shield()
    {
        shield_active = true;
        shield_duration = 0.5f;
    }

    private void ultimate()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }
    }
}
