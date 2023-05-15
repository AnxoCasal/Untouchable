using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TriangleMove : MonoBehaviour
{

    [Range(0f, 100f)]
    public float moveSpeed;

    Rigidbody2D body;

     int Hp = 3;
     float untouchableCD = 0;
     bool untouchableState = false;

    float dash_cd = 0f;

    public bool lvl2;
    public bool lvl3;

    public bool shield_active = false;
    public float shield_cd = 0f;
    public float shield_duration = 2f;

    public float ultimate_cd = 0f;

    GameObject canvas;
    GameObject hearth1;
    GameObject hearth2;
    GameObject hearth3;
    public Sprite fullHearth;
    public Sprite emptyHearth;

    GameObject dashBtn;
    GameObject shieldBtn;
    GameObject ultimateBtn;
    GameObject dashCount;
    GameObject shieldCount;
    GameObject ultimateCount;

    public Sprite dashBtnIn;
    public Sprite dashBtnOut;
    public Sprite shieldBtnIn;
    public Sprite shieldBtnOut;
    public Sprite ultimateBtnIn;
    public Sprite ultimateBtnOut;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        canvas = GameObject.Find("Canvas");
        hearth1 = canvas.transform.Find("Hearth 1").gameObject;
        hearth2 = canvas.transform.Find("Hearth 2").gameObject;
        hearth3 = canvas.transform.Find("Hearth 3").gameObject;

        dashBtn = canvas.transform.Find("DashBtn").gameObject;
        shieldBtn = canvas.transform.Find("ShieldBtn").gameObject;
        ultimateBtn = canvas.transform.Find("UltimateBtn").gameObject;
        dashCount = canvas.transform.Find("DashCD").gameObject;
        shieldCount = canvas.transform.Find("ShieldCD").gameObject;
        ultimateCount = canvas.transform.Find("UltimateCD").gameObject;
        dashCount = canvas.transform.Find("DashCD").gameObject;
        shieldCount = canvas.transform.Find("ShieldCD").gameObject;
        ultimateCount = canvas.transform.Find("UltimateCD").gameObject;
    }

    void Update()
    {

        switch (Hp)
        {
            case < 0:
            case 0:
                SceneManager.LoadScene("MainMenu");
                break;
            case 1:
                hearth1.GetComponent<Image>().sprite = fullHearth;
                hearth2.GetComponent<Image>().sprite = emptyHearth;
                hearth3.GetComponent<Image>().sprite = emptyHearth;
                break;
            case 2:
                hearth1.GetComponent<Image>().sprite = fullHearth;
                hearth2.GetComponent<Image>().sprite = fullHearth;
                hearth3.GetComponent<Image>().sprite = emptyHearth;
                break;
            case > 3:
            case 3:
                hearth1.GetComponent<Image>().sprite = fullHearth;
                hearth2.GetComponent<Image>().sprite = fullHearth;
                hearth3.GetComponent<Image>().sprite = fullHearth;
                break;
        }

        GetComponent<Renderer>().material.color = Color.white;
        if (untouchableState)
        {
            untouchableCD -= Time.deltaTime;
            if (untouchableCD < 0)
            {
                untouchableState = false;
                gameObject.GetComponent<Animator>().SetBool("Damaged", false);
            }
        }

        parpadeo();
        btnControl();

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

        if (lvl2)
        {
            shield_cd -= Time.deltaTime;
            if (Input.GetKey(KeyCode.Q) && shield_cd < 0)
            {
                shield_cd = 10f;
                shield();
            }
        }

        if (shield_active)
        {
            transform.Find("Shield").gameObject.SetActive(true);
            shield_duration -= Time.deltaTime;
            if (shield_duration < 0)
            {
                shield_active = false;
                transform.Find("Shield").gameObject.SetActive(false);
            }
        }


        if (lvl3)
        {
            ultimate_cd -= Time.deltaTime;
            if (Input.GetKey(KeyCode.E) && ultimate_cd < 0)
            {
                ultimate_cd = 30f;
                ultimate();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!untouchableState)
        {
            if (!shield_active)
            {
                Hp--;
                untouchableState = true;
                untouchableCD = 1;
                gameObject.GetComponent<Animator>().SetBool("Damaged", true);
            }
            else
            {
                //transform.Find("Shield").gameObject.SetActive(false);
                //shield_active = false;
                shield_cd -= 1;
            }
        }
    }

    public void btnControl()
    {
        switch (dash_cd)
        {
            case < 0:
                dashBtn.GetComponent<Image>().sprite = dashBtnOut;
                dashCount.GetComponent<Text>().text = "";
                break;
            case > 0:
                dashBtn.GetComponent<Image>().sprite = dashBtnIn; 
                dashCount.GetComponent<Text>().text = dash_cd.ToString("0");
                break;
        }

        switch (shield_cd)
        {
            case < 0:
                shieldBtn.GetComponent<Image>().sprite = shieldBtnOut;
                shieldCount.GetComponent<Text>().text = "";
                break;
            case > 0:
                shieldBtn.GetComponent<Image>().sprite = shieldBtnIn;
                shieldCount.GetComponent<Text>().text = shield_cd.ToString("0");
                break;
        }

        switch (ultimate_cd)
        {
            case < 0:
                ultimateBtn.GetComponent<Image>().sprite = ultimateBtnOut;
                ultimateCount.GetComponent<Text>().text = "";
                break;
            case > 0:
                ultimateBtn.GetComponent<Image>().sprite = ultimateBtnIn;
                ultimateCount.GetComponent<Text>().text = ultimate_cd.ToString("0");
                break;
        }
    }

    public void parpadeo()
    {
        if (untouchableState)
        {
            if (untouchableCD < 0.2f)
            {
                GetComponent<Renderer>().material.color = Color.red;
            }
            else
            if (untouchableCD < 0.4f)
            {
                GetComponent<Renderer>().material.color = Color.white;
            }
            else
            if (untouchableCD < 0.6f)
            {
                GetComponent<Renderer>().material.color = Color.red;
            }
            else
            if (untouchableCD < 0.8f)
            {
                GetComponent<Renderer>().material.color = Color.white;
            }
            else
            {
                GetComponent<Renderer>().material.color = Color.red;
            }

        }
    }

    private void dash()
    {
        Vector2 impulse = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            impulse.y = 10f;
            gameObject.GetComponent<Animator>().SetTrigger("Dash");
        }
        else if (Input.GetKey(KeyCode.S))
        {
            impulse.y = -10f;
            gameObject.GetComponent<Animator>().SetTrigger("Dash");
        }

        if (Input.GetKey(KeyCode.A))
        {
            impulse.x = -10f;
            gameObject.GetComponent<Animator>().SetTrigger("Dash");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            impulse.x = 10f;
            gameObject.GetComponent<Animator>().SetTrigger("Dash");
        }

        body.velocity = impulse;
    }

    private void shield()
    {
        shield_active = true;
        shield_duration = 2f;
    }

    private void ultimate()
    {
        transform.Find("Explosion").GetComponent<Animator>().SetTrigger("Explosion");
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }
    }
}
