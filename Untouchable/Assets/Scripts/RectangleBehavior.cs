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
        int random = Random.Range(0, 3);
        if (random == 0)
        {
            target = new Vector2(transform.position.x, transform.position.y);
        } 
        else if (random == 1)
        {
            target = new Vector2(transform.position.x-3, transform.position.y+3);
        } 
        else
        {
            target = new Vector2(transform.position.x+3, transform.position.y-3);
        }

        Vector3 vectorToTarget = new Vector3(0, 0, 0) - new Vector3(target.x, target.y, 0);
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, 1);
    }

    void Update()
    {
        float step = Time.deltaTime * speed;


       transform.position = Vector2.MoveTowards(transform.position, new Vector2(-target.x, -target.y), step);
        if (transform.position == new Vector3(-target.x, -target.y,0))
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
