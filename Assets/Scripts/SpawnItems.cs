using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnItems : MonoBehaviour
{
    [SerializeField] private GameObject heart;
    [SerializeField] private GameObject leftGrenade;
    [SerializeField] private GameObject midGrenade;
    [SerializeField] private GameObject rightGrenade;
    
    [SerializeField] private GameObject leftBoss;
    [SerializeField] private GameObject midBoss;
    [SerializeField] private GameObject rightBoss;
    
    [SerializeField] private Vector3 center;
    [SerializeField] private Vector3 size;
    

    private Vector3 pos;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWave(heart));
        StartCoroutine(SpawnWave(leftGrenade));
        StartCoroutine(SpawnWave(midGrenade));
        StartCoroutine(SpawnWave(rightGrenade));
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("GameOver") == null)
        {
            StopAllCoroutines();
        }
    }

    public void Spawn(GameObject item)
    {
        pos = center + new Vector3(Random.Range(-size.x / 2,size.x / 2), size.y/2, 0);
        Instantiate(item, pos, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center,size);
    }

    IEnumerator SpawnWave(GameObject item)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10, 25));
            Spawn(item);

            yield return new WaitForSeconds(5);
        }
    }
}
