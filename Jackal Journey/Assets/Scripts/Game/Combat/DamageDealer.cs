using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private Collider myCollider;
    [SerializeField] public int damage;


    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage);



        }
    }
}
