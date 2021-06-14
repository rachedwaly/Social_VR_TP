using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindTextureToPlane2 : MonoBehaviour
{
    // Start is called before the first frame update
    private Renderer myplane_renderer;
    private void Awake()
    {
        myplane_renderer = gameObject.GetComponent<Renderer>();

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Texture my_img = (Texture)Resources.Load("SelfieIRL");
        myplane_renderer.material.mainTexture = my_img;
    }
}
