using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class FloatingMSG : MonoBehaviour, GameDMG
{
    private Rigidbody _rigidbody;
    private TMP_Text _damagevalue;

    public float InitialYVelocity = 7f;
    public float InitialXVelocityRange = 3f;
    public float LifeTime = 0.8f;


    private void Awake()
    {
        
        _rigidbody= GetComponent<Rigidbody>();
        _damagevalue = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        _rigidbody.velocity = new Vector3(Random.Range(-InitialXVelocityRange, InitialXVelocityRange), InitialYVelocity, InitialYVelocity);
        Destroy(gameObject, LifeTime);
    }

    public void SetMessage(string msg)
    { _damagevalue.SetText(msg); }

}
