using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCheck : MonoBehaviour
{
    public float gravity = -3f;
    private GameController gc;
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("GameController");
        if (go != null)
        {
            gc = go.GetComponent<GameController>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            gc.Live();
        }
    }

    public void Attract(Transform item)
    {
        Vector3 gravityDirection = (item.position - transform.position).normalized;
        item.GetComponent<Rigidbody>().AddForce(gravityDirection * gravity);
    }
}
