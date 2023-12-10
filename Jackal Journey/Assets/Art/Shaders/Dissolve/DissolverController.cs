using System.Collections;
using UnityEngine;
using UnityEngine.VFX;
public class DissolverController : MonoBehaviour
{

    public SkinnedMeshRenderer skinnedMesh; // ewentualnie skinned/MeshRenderer ( to co trzyma nasza teksture )
    public VisualEffect VFXGraph;
    public float dissolveRate = 0.0125f;
    public float refreshRate = 0.025f;

    private Material[] skinnedMaterials;


    void Start()
    {

        if (skinnedMesh != null)
        {
            skinnedMaterials = skinnedMesh.materials;
        }
        else
        {
            Debug.LogError("SkinnedMeshRenderer is not assigned to DissolverController.");
        }
    }

    public IEnumerator DissolveCo()
    {
        if (VFXGraph != null)
        {
            Debug.Log("Play particle enemy");
            VFXGraph.Play();
        }
        else
        {
            Debug.LogWarning("VFXGraph is not assigned to DissolverController.");
        }

        if (skinnedMaterials == null || skinnedMaterials.Length == 0)
        {
            Debug.LogError("No materials found on SkinnedMeshRenderer.");
            yield break; // Exit the coroutine if no materials are found.
        }


        float counter = 0;
            while (skinnedMaterials[0].GetFloat("_DissolveAmmount") < 1)
            {
                counter += dissolveRate;
                for (int i = 0; i < skinnedMaterials.Length; i++)
                {
                    skinnedMaterials[i].SetFloat("_DissolveAmmount", counter);
                }
                yield return new WaitForSeconds(refreshRate);
            }
        Debug.Log("DissolveCo completed.");
    }
}
