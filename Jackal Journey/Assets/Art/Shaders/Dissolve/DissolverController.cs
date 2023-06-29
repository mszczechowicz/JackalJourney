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
    Animator m_Animator;
    



    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        if (skinnedMesh != null) 
        {
            skinnedMaterials  = skinnedMesh.materials;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(DissolveCo());
            }
        //if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("SpiderDeath"))
        //{
        //    StartCoroutine(DissolveCo());
        //}
    }
    IEnumerator DissolveCo()
    {
        if(VFXGraph != null)
        {
            VFXGraph.Play();
        }
        float counter = 0;
        while (skinnedMaterials[0].GetFloat("_DissolveAmmount") < 1)
        {
            counter += dissolveRate;
            for(int i = 0; i < skinnedMaterials.Length; i++)
            {
                skinnedMaterials[i].SetFloat("_DissolveAmmount", counter);
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
