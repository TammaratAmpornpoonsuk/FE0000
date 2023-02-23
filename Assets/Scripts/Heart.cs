using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public FallingCheck fallingCheck;
    private Transform heartTransform;
    private Rigidbody rb;
    private GameController gc;
    private Vector3 heartForce = Vector3.down;
    private float heartSpeed = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        heartTransform = transform;
        rb.useGravity = false;
        rb.AddForce(heartForce * heartSpeed);
        
        GameObject go = GameObject.FindGameObjectWithTag("GameController");
        if (go != null)
        {
            gc = go.GetComponent<GameController>();
        }
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("GameOver") == null)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        fallingCheck.Attract(heartTransform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.instance.Play(SoundManager.SoundName.GetItem);
            gc.GetHeart();
            Destroy(gameObject);
        }

        if (other.CompareTag("FallingCheck"))
        {
            Destroy(gameObject);
        }
    }
}
