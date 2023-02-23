using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private string bossName;
    
    public FallingCheck fallingCheck;
    private Transform grenadeTransform;
    private Rigidbody rb;
    private GameController gc;
    Vector3 grenadeForce = Vector3.down;
    private float grenadeSpeed = 3f;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        grenadeTransform = transform;
        rb.useGravity = false;
        rb.AddForce(grenadeForce * grenadeSpeed);
        
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

    void FixedUpdate()
    {
        fallingCheck.Attract(grenadeTransform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.instance.Play(SoundManager.SoundName.Hit);
            gc.Attack(bossName);
            Destroy(gameObject);
        }

        if (other.CompareTag("FallingCheck"))
        {
            Destroy(gameObject);
        }
    }
}
