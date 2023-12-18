using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSpawner : MonoBehaviour
{
    [SerializeField] Vector3 _initialPosition;
    [SerializeField] GameObject _messagePrefab;

    public void SpawnDamage(string msg)
    {
        var msgObj = Instantiate(_messagePrefab, GetSpawnPosition(),Quaternion.identity);
        var inGameMessage = msgObj.GetComponent<GameDMG>();
        inGameMessage.SetMessage(msg);
    }

    private Vector3 GetSpawnPosition()
    { return transform.position + (Vector3)_initialPosition; }
}
