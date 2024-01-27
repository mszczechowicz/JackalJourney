
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;

    private int damage;
    private float knockback;

    private List<Collider> alreadyCollideWith = new List<Collider>();

    private void OnEnable()
    {
        alreadyCollideWith.Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other == myCollider) { return; }

        if(other.tag == myCollider.tag) { return; }

        if(alreadyCollideWith.Contains(other)) { return; }

        alreadyCollideWith.Add(other);

        if (other.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage);

           if (other.tag == "Player") { return; }
           health.CreatePopUp(damage);
        
        }
        if (other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            forceReceiver.AddForce(direction * knockback);
        }
    }

    public void SetAttack(int damage, float knockback)
    { 
    this.damage = damage;
        this.knockback = knockback;
    }
    
}
