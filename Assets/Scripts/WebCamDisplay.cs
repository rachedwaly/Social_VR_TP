namespace OpenCvSharp
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using OpenCvSharp;
    using UnityEditor;

    public class WebCamDisplay : MonoBehaviour, IWebCamDisplay
    {
        
        private WebCamTexture webCamTexture;
        private bool takeSelfie = false;
        private Camera virtualCamera;
        private Transform target;
        private Vector3 screenPos;
        private int heightCamera, widthCamera;



        // Start is called before the first frame update

        void Awake()
        {
            
            virtualCamera = GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/RightHandAnchor/VirtualCamera").GetComponent<Camera>();
            target = GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/head").GetComponent<Transform>();

            //MouseSelector.onPointsSelected += removeUser;
            Handler.takeSelfie += takeIRLSelfie;
            Handler.removeUser += removeUser;
        }

        void Start()
        {
            AttachWebcamTexture();
        }

        public void AttachWebcamTexture()
        {
            webCamTexture = new WebCamTexture();
            widthCamera = webCamTexture.width;
            heightCamera = webCamTexture.height;
            this.GetComponent<MeshRenderer>().material.mainTexture = webCamTexture;
            webCamTexture.Play();
        }


        private void Update()
        {
            if (takeSelfie == true)
            {

                takeSelfie = false;
                Texture2D texture2D = new Texture2D(webCamTexture.width, webCamTexture.height);
                texture2D.SetPixels32(webCamTexture.GetPixels32());

                screenPos = virtualCamera.WorldToScreenPoint(target.position);
                screenPos.y = virtualCamera.pixelHeight - screenPos.y;


                byte[] byteArray = texture2D.EncodeToPNG();
                System.IO.File.WriteAllBytes(Application.dataPath + "/Resources/SelfieIRL.png", byteArray);
                AssetDatabase.Refresh();
            }
        }

        public void takeIRLSelfie()
        {
            takeSelfie = true;
        }



        private void removeUser()
        {
           

            Texture2D my_img = LoadPNG(Application.dataPath + "/Resources/SelfieIRL.png");
            
            //Texture2D my_img = LoadPNG(Application.dataPath + "/Resources/zall1.png");
            Mat matc = Unity.TextureToMat(my_img);

            var result = new Mat(matc.Size(), MatType.CV_8UC1);

            //Rect rect = new Rect(2, 2, matc.Width - 2, matc.Height - 2);




            Debug.Log("x" + screenPos.x);
            Debug.Log("y" + screenPos.y);
            Debug.Log("zall1" + ((float) (matc.Width *2.0)/10.0));
            Debug.Log("zall1" + (matc.Width * 2.0 / 10.0));
            Debug.Log("zall2" + matc.Height );

            Rect rect = new Rect((int)(screenPos.x-(matc.Width/5.0)),(int)(screenPos.y- matc.Height/5.0), (int) (matc.Width*(2.0/5.0)),(int) (matc.Height*(2.0/5.0)));
            //Rect rect = new Rect((int)rectangle.p1.x, (int)rectangle.p1.y, (int)rectangle.GetWidth(), (int)rectangle.GetHeight());
            
            Cv2.GrabCut(matc, result, rect, new Mat(), new Mat(), 3, GrabCutModes.InitWithRect);
            Mat newResult = ((result & 1)) * 255;
            Debug.Log("zal");

            //inverting mask
            Mat newResult1 = new Mat();
            newResult1 = 255 - newResult;

            Mat cropped = new Mat();
            //applying the mask
            matc.CopyTo(cropped, newResult1);


            //matc.CopyTo(cropped, newResult);



            Mat grayMat = new Mat();

            Mat result_inpaint = new Mat();
            Cv2.Inpaint(cropped, newResult, result_inpaint, 5, InpaintMethod.Telea);

            //Cv2.Rectangle(result_inpaint, rect, 1);
            Cv2.Circle(result_inpaint, new Point(screenPos.x, screenPos.y), 10, 1);

            byte[] byteArray2 = Unity.MatToTexture(result_inpaint).EncodeToJPG();
            //byte[] byteArray2 = Unity.MatToTexture(cropped).EncodeToJPG();
           
            System.IO.File.WriteAllBytes(Application.dataPath + "/Resources/SelfieIRL.png", byteArray2);
            AssetDatabase.Refresh();

        }



        private Texture2D LoadPNG(string filePath)
        {

            Texture2D tex = null;
            byte[] fileData;

            if (System.IO.File.Exists(filePath))
            {
                fileData = System.IO.File.ReadAllBytes(filePath);
                tex = new Texture2D(2, 2);
                tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
            }
            return tex;
        }
    }
}