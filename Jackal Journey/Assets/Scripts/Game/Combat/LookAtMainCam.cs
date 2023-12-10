using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class LookAtMainCam : MonoBehaviour
{
    [SerializeField] public Transform cam;

    private void LateUpdate()
    {
        //transform.LookAt(cam);
        transform.LookAt(transform.position + cam.forward);
    }
    
}
   
