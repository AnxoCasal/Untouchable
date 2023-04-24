using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBehavior : MonoBehaviour
{
    Transform parent;
    public float velocidad = 2f;
    public float radio = 2f;
    public float angulo = 0f;


    private void Start()
    {
        parent = transform.parent;
    }

    void Update()
    {
        movimiento_circular();
    }

    public void movimiento_circular()
    {
        float x = Mathf.Sin(angulo * Mathf.Deg2Rad) * radio;
        float y = Mathf.Cos(angulo * Mathf.Deg2Rad) * radio;

        transform.position = new Vector3(parent.position.x + x, parent.position.y + y, parent.position.z);

        angulo += velocidad * Time.deltaTime;

        if (angulo >= 360f) angulo -= 360f;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
