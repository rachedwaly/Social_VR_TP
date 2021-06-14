namespace OpenCvSharp
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using OpenCvSharp;
    using UnityEngine.UI;

    public class BindTextureToPlane : MonoBehaviour
    {
        // Start is called before the first frame update
        private Renderer myplane_renderer;
        private static BindTextureToPlane instance;
        private void Awake()
        {
            myplane_renderer = gameObject.GetComponent<Renderer>();

        }


        // Update is called once per frame
        void Update()
        {
            Texture my_img = (Texture)Resources.Load("SelfieInGame");
            myplane_renderer.material.mainTexture = my_img;

        }

        /*
        private void function()
        {
            Texture my_img = (Texture)Resources.Load("zall");
            Mat mat = Unity.TextureToMat((Texture2D)my_img);
            Mat grayMat = new Mat();
            Cv2.CvtColor(mat, grayMat, ColorConversionCodes.BGR2GRAY);
            Texture2D texture = Unity.MatToTexture(grayMat);
            myplane_renderer.material.mainTexture = texture;
        }
        public static void Test()
        {
            instance.function();
        }
        */



    }
}
