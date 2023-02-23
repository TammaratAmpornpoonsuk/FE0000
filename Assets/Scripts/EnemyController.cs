using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private int delayAttack;

    private GameController gc;
    private Vector3 defaultPos;
    private Vector3 newPos;
    private bool moved;
    private bool attacking;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("GameController");
        if (go != null)
        {
            gc = go.GetComponent<GameController>();
        }
        
        material.color = Color.white;
        defaultPos = transform.position;
        
        StartCoroutine(BossAttack(delayAttack,2,0.5f));
        
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("GameOver") == null)
        {
            StopAllCoroutines();
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

    IEnumerator BossAttack(int delayTime,int attackMode,float currentPos)
    {
        while (true)
        {
            yield return new WaitForSeconds(delayTime);
            attacking = true;
            while (attacking)
            {
                material.color = Color.red;
                yield return new WaitForSeconds(attackMode);
                newPos = new Vector3(defaultPos.x,defaultPos.y,1);
                transform.position = newPos;
                attacking = false;
            }
            moved = true;
            while (moved)
            {
                yield return new WaitForSeconds(currentPos);
                transform.position = defaultPos;
                material.color = Color.white;
                moved = false;
            }
        }
    }
}
