using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObject : MonoBehaviour
{
    public GameObject fractured;
    public GameObject savefractured;
    public int myLayer;



    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.gameObject.layer == myLayer)
        {
            BreakTheThing();
        }
    }



    public void BreakTheThing()
    {

        savefractured = Instantiate(fractured, transform.position, transform.rotation);
        Destroy(gameObject);

        Destroy(savefractured, 10);
    }
}
