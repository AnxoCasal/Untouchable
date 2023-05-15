using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonBehavior : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 target;
    public float speed;
    public float timeStop;
    bool turn = false;
    float timer;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        target = new Vector2(transform.position.x, transform.position.y);

        if(transform.position.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void Update()
    {

        if (Vector2.Distance(transform.position, new Vector2(0, 0)) < 3.25f && !turn)
        {
            timer += Time.deltaTime;
            if (timer > timeStop)
            {
                turn = true;
                target = GameObject.FindGameObjectWithTag("Player").transform.position;
                rb.velocity = Vector3.zero;
                rb.AddForce((target - new Vector2(transform.position.x, transform.position.y)) * speed);
            }
        }
        else if (!turn)
        {
            float step = Time.deltaTime * (speed / 10);
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(-target.x, -target.y), step);
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
