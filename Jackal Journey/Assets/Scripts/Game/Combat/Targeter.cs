using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup cineTargetGroup;
    [SerializeField] private float targetCameraMemberWeight = 1f;
    [SerializeField] private float targetCameraMemberRadius = 8f;
    private Camera mainCamera;

    private List<Target> targets = new List<Target>();

    public Target CurrentTarget { get; private set; }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Target target)) { return; }
        targets.Add(target);
        target.OnDestroyed += RemoveTarget;
        //Debug.Log("adtarget");
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out Target target)) { return; }
       
        RemoveTarget(target);
        //Debug.Log("removetarget");
    }

    public bool SelectTarget()
    {

        Debug.Log("SelectTarget()");
        if (targets.Count == 0) { return false; }

        Target closestTarget = null;
        float closestTargetDistance = Mathf.Infinity;

        foreach (Target target in targets)
        {
            Vector2 viewPos = mainCamera.WorldToViewportPoint(target.transform.position);

            if (!target.GetComponentInChildren<Renderer>().isVisible)
            {
                continue;
            }

            Vector2 toCenter = viewPos - new Vector2(0.5f, 0.5f);
            if (toCenter.sqrMagnitude < closestTargetDistance)
            { 
                closestTarget = target;
                closestTargetDistance = toCenter.sqrMagnitude;
            
            }

        }
        if (closestTarget == null) { return false; }

        CurrentTarget = closestTarget;
        Debug.Log(" CurrentTarget = closestTarget;");

        cineTargetGroup.AddMember(CurrentTarget.transform, targetCameraMemberWeight,targetCameraMemberRadius);
        CurrentTarget.GetComponent<Target>().TargetImage.SetActive(true);
        Debug.Log("  cineTargetGroup.AddMember(CurrentTarget.transform, targetCameraMemberWeight,targetCameraMemberRadius);");
        return true;
    }

    public void CancelTargeting()        
    {
        Debug.Log("CancelTargeting()");
        if (CurrentTarget == null) { return; }
        cineTargetGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget.GetComponent<Target>().TargetImage.SetActive(false);
        CurrentTarget = null;
    }

    private void RemoveTarget(Target target)
    {
        Debug.Log(" RemoveTarget()");
        if (CurrentTarget == target)
        {
            cineTargetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }
        target.OnDestroyed -= RemoveTarget;
        targets.Remove(target);
    }

}
