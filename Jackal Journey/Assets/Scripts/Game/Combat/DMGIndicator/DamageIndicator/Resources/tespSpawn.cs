using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tespSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public Transform test;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PopUpDamage.Create(test.transform.position, 300);
        }
    }
}
