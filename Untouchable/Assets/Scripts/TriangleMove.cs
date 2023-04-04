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

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        // CHECK DE INPUTS

        if (Input.GetKey(KeyCode.W))
        {
            body.AddForce(new Vector2(0,moveSpeed * Time.deltaTime), ForceMode2D.Impulse);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            body.AddForce(new Vector2(0, -moveSpeed * Time.deltaTime), ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.A))
        {
            body.AddForce(new Vector2(-moveSpeed * Time.deltaTime, 0), ForceMode2D.Impulse);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            body.AddForce(new Vector2(moveSpeed * Time.deltaTime, 0), ForceMode2D.Impulse);
        }

        // CHECK DE VELOCIDAD PUNTA

        if (body.velocity.y > topSpeed)
        {
            body.velocity = new Vector2(body.velocity.x, topSpeed);
        }
        else if (body.velocity.y < -topSpeed)
        {
            body.velocity = new Vector2(body.velocity.x, -topSpeed);
        }

        if (body.velocity.x > topSpeed)
        {
            body.velocity = new Vector2(topSpeed, body.velocity.y);
        }
        else if (body.velocity.x < -topSpeed)
        {
            body.velocity = new Vector2(-topSpeed, body.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hp--;
    }
}
