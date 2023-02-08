using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BallToBasket : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject pivot;
    public Transform endOfBasket;
    private Camera cam;
    public float gain;
    private bool _isDragging;

    private SpringJoint _springJoint;

    private bool _canDrag;

    private bool _canThrow = true;

    private bool _inBasket = false;

    public Vector3 ballPlace;

    private int _kidScore = 0;

    private int _dollScore = 0;

    public Text textKidScore;

    public Text textDollScore;
    public GameObject dollHolding;
    public GameObject basket;
    private GameObject _prefab;

    public GameObject dastopaha;

    public GameObject charManager;
    public AudioClip[] musics;
    public AudioClip failSound;
    private int _musicTurn = 0;
    public AudioClip theme;
    private bool _firstTime = true;

    public GameObject successEffect;

    public GameObject failEffect;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        _springJoint = GetComponent<SpringJoint>();
        ballPlace = transform.position;
        //Debug.Log("ball place: "+ballPlace);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("ball: " + transform.position + ", endOfBasket: " + endOfBasket.position);
        /*if (!GetComponent<AudioSource>().isPlaying)
        {
            
        }*/
        if (_prefab == null)
        {
            _prefab = GameObject.FindWithTag("Prefab");
        }
        else
        {
            if (_firstTime)
            {
                Spawn(dastopaha, 0, 0, 0);
                Spawn(charManager, 0, 0, 0);
                Spawn(dollHolding, 0, 0.04f, 0);
                _firstTime = false;
                GetComponent<AudioSource>().PlayOneShot(theme);
            }
        }

        //DetectTouch();
        if (Input.anyKey)
        {
            if (_canThrow)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
                rb.AddForce(gain * (cam.transform.forward + cam.transform.up));
                _canThrow = false;
            }
        }

        /*if (!_canThrow)
        {
            CheckInBasket();
        }*/

        if (transform.position.y < endOfBasket.position.y - 10f && !_canThrow)
        {
            if (!_inBasket)
            {
                _dollScore++;
                textDollScore.text = _dollScore + "";
                PlayEffect(false);
            }
            ResetBall();
        }
    }

    private void DetectTouch()
    {
        if (!Touchscreen.current.primaryTouch.press.isPressed)
        {
            if (_isDragging)
            {
                LaunchPlayer();
            }

            _isDragging = false;
            return;
        }

        if (_canDrag)
        {
            _isDragging = true;
            rb.isKinematic = true;

            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 worldPosition = cam.ScreenToWorldPoint(touchPosition);
            worldPosition.z = transform.position.z;
            transform.position = worldPosition;
        }
    }

    private void LaunchPlayer()
    {
        throw new System.NotImplementedException();
    }

    private void LaunchBall()
    {
        rb.isKinematic = false;
        _canDrag = false;
    }

    private void DetachBall()
    {
        if (transform.position.z > pivot.transform.position.z && !_isDragging)
        {
        }
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Basket"))
        {
            _inBasket = true;
            
            Debug.Log("Collision!"+collision.gameObject.name);
        }
    }*/

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag.Equals("Basket"))
        {
            _inBasket = true;
            transform.SetParent(dollHolding.transform);
            rb.isKinematic = true;
            StartCoroutine(WaitAndReset());
            PlayEffect(true);
            _kidScore++;
            textKidScore.text = _kidScore + "";
            Debug.Log("Collision!"+collisionInfo.gameObject.name);
        }
    }

    private void Spawn(GameObject g, float x, float y, float z)
    {
        g.transform.parent = _prefab.transform;
        g.transform.localPosition = new Vector3(x, y, z);
        g.transform.localRotation = Quaternion.Euler(-90, -180, 0);
        g.SetActive(true);
    }

    private void ResetBall()
    {
        var randX = Random.Range(-20f, 20f);
        dollHolding.transform.localEulerAngles = new Vector3(-90 + randX, -180 + Random.Range(-30f, 30f), 0);
        basket.transform.localEulerAngles = new Vector3(2.285f - randX, 0, 0);
        rb.useGravity = false;
        rb.isKinematic = true;
        transform.SetParent(cam.transform);
        transform.localPosition = ballPlace;
        _canThrow = true;
        _inBasket = false;
    }

    private void CheckInBasket()
    {
        var p = transform.position - endOfBasket.position;
        if (p.x is < 0.011f and > -0.011f)
        {
            if (p.y is < 0.021f and > 0.011f)
            {
                if (p.z is < 0.011f and > 0)
                {
                    _inBasket = true;
                    transform.SetParent(dollHolding.transform);
                    rb.isKinematic = true;
                    StartCoroutine(WaitAndReset());
                    PlayEffect(true);
                    _kidScore++;
                    textKidScore.text = _kidScore + "";
                }
            }
        }
    }

    IEnumerator WaitAndReset()
    {
        yield return new WaitForSeconds(1.5f);
        ResetBall();
    }

    private void PlayEffect(bool isSuccess)
    {
        if (isSuccess)
        {
            successEffect.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(musics[_musicTurn]);
            _musicTurn++;
            if (_musicTurn >= musics.Length)
            {
                _musicTurn = 0;
            }
        }
        else
        {
            failEffect.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(failSound);
        }
    }
}