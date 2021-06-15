using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonGod : MonoBehaviour
{
    [Header("Particles")]
    public GameObject fire1;
    public GameObject fire2;
    public GameObject runesParticles;
    public GameObject magicSwirlParticles;
    public GameObject fire3;
    public GameObject energyPull;
    public GameObject runesParticles2;


    [Header("Evil god")]
    public GameObject evilGod;

    private MeshRenderer meshRenderer;
    private bool isGodSummoned = false;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Left Controller"))
        {
            SummonEvilGod();
        }
    }

    public void SummonEvilGod()
    {
        StartCoroutine(SummonGodCoroutine());
    }

    IEnumerator SummonGodCoroutine()
    {
        fire1.SetActive(false);
        fire2.SetActive(false);

        yield return new WaitForSeconds(2f);

        meshRenderer.enabled = false;

        yield return new WaitForSeconds(2f);

        runesParticles.SetActive(true);

        yield return new WaitForSeconds(2f);

        magicSwirlParticles.SetActive(true);

        yield return new WaitForSeconds(2f);

        fire3.SetActive(true);

        yield return new WaitForSeconds(2f);

        energyPull.SetActive(true);

        yield return new WaitForSeconds(4f);

        evilGod.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        runesParticles2.SetActive(true);



    }
}
