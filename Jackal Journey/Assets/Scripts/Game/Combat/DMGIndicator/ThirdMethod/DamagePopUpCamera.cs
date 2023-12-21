using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopUpCamera : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera cam;

    // Funkcja Awake jest wywoływana podczas ładowania wystąpienia skryptu
    private void Awake()
    {

        cam = Camera.main;
    }



    // Update is called once per frame
    void Update()
    {
        transform.forward= cam.transform.forward;
    }
}
