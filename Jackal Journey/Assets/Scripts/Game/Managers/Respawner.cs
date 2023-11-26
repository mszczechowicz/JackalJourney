using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;

public class Respawner : MonoBehaviour
{
    [SerializeField] Transform respawnLocation;
    [SerializeField] float respawnDelay = 3;
    [SerializeField] float fadeTime = 1f;

    private void Awake()
    {
        GetComponent<Health>().onDie_UnityEvent.AddListener(Respawn);

    }

    private void Respawn()
    {
        StartCoroutine(RespawnRoutine());
    }


    private IEnumerator RespawnRoutine()
    {
       
        yield return new WaitForSeconds(respawnDelay);

        Fader fader = FindObjectOfType<Fader>();
        yield return fader.FadeOut(fadeTime);
       
        Debug.Log("Respawn()");
        GetComponent<CharacterController>().enabled = false;
        GetComponent<ForceReceiver>().enabled = false;
        this.transform.position = respawnLocation.position;

        GetComponent<CharacterController>().enabled = true;
        GetComponent<ForceReceiver>().enabled = true;
        //Do przerobienia reset zdrowia po œmierci. Jakie sa za³o¿enia projektowe?
         GetComponent<Health>().healthPoints += 100;
        yield return new WaitForSeconds(respawnDelay);
        yield return fader.FadeIn(fadeTime);
    }


    //[SerializeField] Transform respawnLocation;
    //[SerializeField] float respawnDelay = 3;
    //[SerializeField] float fadeTime = 0.2f;
    //[SerializeField] float healthRegenPercentage = 20;
    //[SerializeField] float enemyHealthRegenPercentage = 20;

    //private void Awake()
    //{
    //    GetComponent<Health>().onDie.AddListener(Respawn);
    //}

    //private void Start()
    //{
    //    if (GetComponent<Health>().IsDead)
    //    {
    //        Respawn();
    //    }
    //}

   

    //private IEnumerator RespawnRoutine()
    //{
    //    SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
    //    // savingWrapper.Save();
    //    yield return new WaitForSeconds(respawnDelay);
    //    Fader fader = FindObjectOfType<Fader>();
    //    yield return fader.FadeOut(fadeTime);
    //    RespawnPlayer();
    //    //ResetEnemies();
    //    // savingWrapper.Save();
    //    yield return fader.FadeIn(fadeTime);
    //}

    //private void ResetEnemies()
    //{
    //    foreach (AIController enemyControllers in FindObjectsOfType<AIController>())
    //    {
    //        Health health = enemyControllers.GetComponent<Health>();
    //        if (health && !health.IsDead())
    //        {
    //            enemyControllers.Reset();
    //            health.Heal(health.GetMaxHealthPoints() * enemyHealthRegenPercentage / 100);
    //        }
    //    }
    //}

    //private void RespawnPlayer()
    //{
    //    Vector3 postionDelta = respawnLocation.position - transform.position;
    //    GetComponent<NavMeshAgent>().Warp(respawnLocation.position);
    //    Health health = GetComponent<Health>();
    //    health.Heal(health.GetMaxHealthPoints() * healthRegenPercentage / 100);
    //    ICinemachineCamera activeVirtualCamera = FindObjectOfType<CinemachineBrain>().ActiveVirtualCamera;
    //    if (activeVirtualCamera.Follow == transform)
    //    {
    //        activeVirtualCamera.OnTargetObjectWarped(transform, postionDelta);
    //    }
    //}


}
