using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Side : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private Material baseMaterial;
    [SerializeField] private Material orangeMaterial;
    
    // Start is called before the first frame update
    void Start()
    {
        material.color = baseMaterial.color;

        StartCoroutine(OrangeZone());
        
        if (GameObject.FindGameObjectWithTag("GameOver") == null)
        {
            StopAllCoroutines();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator OrangeZone()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2, 10));
            material.color = orangeMaterial.color;
        
            if (material.color == orangeMaterial.color)
            {
                yield return new WaitForSeconds(4);
            }

            material.color = baseMaterial.color;
        }
    }
}
