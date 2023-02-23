using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Platform : MonoBehaviour
{
    [SerializeField] private Material platformMaterial;
    [SerializeField] private Material baseMaterial;
    [SerializeField] private PhysicMaterial redZoneMaterial;
    [SerializeField] private PhysicMaterial greenZoneMaterial;

    private GameController gc;

    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("GameController");
        if (go != null)
        {
            gc = go.GetComponent<GameController>();
        }

        platformMaterial.color = baseMaterial.color;
        GetComponent<Collider>().material = greenZoneMaterial;

        StartCoroutine(RedZone());
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("GameOver") == null)
        {
            StopAllCoroutines();
        }
    }

    IEnumerator RedZone()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 20));
            platformMaterial.color = Color.red;
            GetComponent<Collider>().material = redZoneMaterial;
        
            if (platformMaterial.color == Color.red)
            {
                yield return new WaitForSeconds(4);
                GetComponent<Collider>().enabled = false;
                GetComponent<Renderer>().enabled = false;
            }

            yield return new WaitForSeconds(1.5f);
            GetComponent<Collider>().enabled = true;
            GetComponent<Renderer>().enabled = true;
            platformMaterial.color = baseMaterial.color;
            GetComponent<Collider>().material = greenZoneMaterial;
        }
    }
}
