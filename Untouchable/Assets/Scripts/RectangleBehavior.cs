using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RectangleBehavior : MonoBehaviour
{
    Vector2 target;
    public float speed;

    private void Start()
    {
        target = new Vector2( transform.position.x, transform.position.y);
    }

    void Update()
    {
        float step = Time.deltaTime * speed;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(-target.x, -target.y), step);
        if(transform.position == new Vector3(-target.x, -target.y,0))
        {
            Destroy(gameObject);
        }
    }

        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
