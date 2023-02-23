using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.Play(SoundManager.SoundName.BGM1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
