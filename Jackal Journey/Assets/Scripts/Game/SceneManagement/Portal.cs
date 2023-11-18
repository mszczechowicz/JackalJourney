
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        if (destinationMap == Map.None) return;
        StartCoroutine(GoToNextScene());
    }

    enum PortalId : byte
    {
        None, a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q, r, s, t, u, v, w, x, y, z,
    }
    public enum Map
    {
        None,
        Level101,
        Level103
    }

    [SerializeField] PortalId portalId = PortalId.None;

    [SerializeField] Map destinationMap = Map.None;
    [SerializeField] PortalId destinationPortalId = PortalId.None;

    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private GameObject Player;

    [SerializeField] float fadeOutTime = 1f;
    [SerializeField] float fadeInTime = 2f;
    [SerializeField] float fadeWaitTime = 2f;
    private IEnumerator GoToNextScene()
    {

        string sceneName = SceneManager.GetActiveScene().name;
        string destMapIndex = destinationMap.ToString();

       
        DontDestroyOnLoad(gameObject);

        Fader fader = FindObjectOfType<Fader>();
        yield return fader.FadeOut(fadeOutTime);
        //Save current level
        SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
        wrapper.Save();
        yield return SceneManager.LoadSceneAsync(destMapIndex);                

        //Load current level
        wrapper.Load();

        Portal otherPortal = GetOtherPortal();
        UpdatePlayerPosition(otherPortal);
        wrapper.Save();

        yield return new WaitForSeconds(fadeWaitTime);
        yield return fader.FadeIn(fadeInTime);

        Destroy(gameObject);
      
    }
    
    private void UpdatePlayerPosition(Portal otherPortal)
    {

        GameObject player = GameObject.FindWithTag("Player");
                 
            player.GetComponent<CharacterController>().enabled = false;
            player.GetComponent<ForceReceiver>().enabled = false;
          
            player.transform.position = otherPortal.SpawnPoint.position;
          
            player.GetComponent<CharacterController>().enabled = true;
            player.GetComponent<ForceReceiver>().enabled = true;
    }

    private Portal GetOtherPortal()
    {
        int sceneBuildIndex = SceneManager.GetActiveScene().buildIndex;


        foreach (Portal portal in FindObjectsByType<Portal>(FindObjectsSortMode.None))
        {
            if (sceneBuildIndex != portal.gameObject.scene.buildIndex) continue;
            if (portal.portalId == this.destinationPortalId) return portal;

        }
        return null;
    }
   

}
