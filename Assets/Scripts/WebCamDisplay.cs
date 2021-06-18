namespace OpenCvSharp
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using OpenCvSharp;
    using UnityEditor;

    public class WebCamDisplay : MonoBehaviour, IWebCamDisplay
    {
        public Camera selfieCam, selfieCam2;

        private WebCamTexture webCamTexture;
        private bool takeSelfie = false;
        private Camera virtualCamera, virtualCamera2;
        private Transform target;
        private Vector3 screenPos;
        private Rect rect1;

        // Start is called before the first frame update

        void Awake()
        {
            selfieCam = GameObject.Find("SelfieCamera").GetComponent<Camera>();
            virtualCamera = GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/RightHandAnchor/VirtualCamera").GetComponent<Camera>();
            target = GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/head").GetComponent<Transform>();
            selfieCam2 = GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/RightHandAnchor/VirtualCamera2").GetComponent<Camera>();

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

            this.GetComponent<MeshRenderer>().material.mainTexture = webCamTexture;
            webCamTexture.Play();
            
        }


        private void Update()
        {
            if (takeSelfie == true)
            {
                takeSelfie = false;


                selfieCam.CopyFrom(virtualCamera);
                selfieCam2.CopyFrom(virtualCamera2);


                var currentRT = RenderTexture.active;
                RenderTexture.active = selfieCam.targetTexture;

                // Render the camera's view.
                selfieCam.Render();


                // Make a new texture and read the active Render Texture into it.
                Texture2D image = new Texture2D(selfieCam.targetTexture.width, selfieCam.targetTexture.height);
                image.ReadPixels(new UnityEngine.Rect(0, 0, selfieCam.targetTexture.width, selfieCam.targetTexture.height), 0, 0);
                image.Apply();

           
                // Replace the original active Render Texture.
                RenderTexture.active = currentRT;

                currentRT = RenderTexture.active;
                RenderTexture.active = selfieCam2.targetTexture;

                // Render the camera's view.
                selfieCam2.Render();


                // Make a new texture and read the active Render Texture into it.
                Texture2D image2 = new Texture2D(selfieCam2.targetTexture.width, selfieCam2.targetTexture.height);
                image2.ReadPixels(new UnityEngine.Rect(0, 0, selfieCam2.targetTexture.width, selfieCam2.targetTexture.height), 0, 0);
                image2.Apply();

         

                // Replace the original active Render Texture.
                RenderTexture.active = currentRT;


                Texture2D texture2D = new Texture2D(webCamTexture.width, webCamTexture.height);
                texture2D.SetPixels32(webCamTexture.GetPixels32());

                Debug.Log("zall3 " + selfieCam.targetTexture.width);
                Debug.Log("zall3 " + selfieCam.targetTexture.height);
                Debug.Log("zall3 " + webCamTexture.width);
                Debug.Log("zall3 " + webCamTexture.height);



                screenPos = selfieCam.WorldToScreenPoint(target.position);
                screenPos.y = selfieCam.pixelHeight - screenPos.y;

                Mat test = Unity.TextureToMat(texture2D);
                Cv2.Circle(test, new Point(screenPos.x + 50, screenPos.y), 5, new Scalar(255,0,0));

                //Cv2.Rectangle(test, new Rect((int)screenPos.x, (int)screenPos.y, -150, -150), new Scalar(255, 0, 0));
                rect1 = new Rect((int)(test.Width * 0.20), (int)(test.Height * 0.20), (int)(test.Width * 0.6), (int)(test.Height * 0.79));
                Cv2.Rectangle(test, new Rect((int)(test.Width * 0.20), (int)(test.Height * 0.20), (int)(test.Width * 0.6), (int)(test.Height * 0.79)), new Scalar(255, 0, 0));
                texture2D = Unity.MatToTexture(test);

                Mat test1 = Unity.TextureToMat(image) ;
                Cv2.Circle(test1, new Point((int)(screenPos.x-(test1.Width*1.0)/4.0),(int)( screenPos.y - (test1.Height * 1.0) / 4.0)), 5, new Scalar(255,0,0));
                image = Unity.MatToTexture(test1);


                byte[] byteArray = texture2D.EncodeToPNG();
                System.IO.File.WriteAllBytes(Application.dataPath + "/Resources/SelfieIRL.png", byteArray);
                AssetDatabase.Refresh();

                byte[] byteArray2 = image.EncodeToPNG();
                System.IO.File.WriteAllBytes(Application.dataPath + "/Resources/VSelfieIRL.png", byteArray2);
                AssetDatabase.Refresh();

                byte[] byteArray3 = image2.EncodeToPNG();
                System.IO.File.WriteAllBytes(Application.dataPath + "/Resources/VSelfieIRLBG.png", byteArray3);
                AssetDatabase.Refresh();

                
            }
        }

        public void takeIRLSelfie()
        {
            takeSelfie = true;
        }



        private void removeUser()
        {
           

            Texture2D real_selfie = LoadPNG(Application.dataPath + "/Resources/SelfieIRL.png");

            Texture2D virtual_selfie = LoadPNG(Application.dataPath + "/Resources/VSelfieIRL.png");

            Debug.Log("zall4 " + real_selfie.width);
            Debug.Log("zall4 " + real_selfie.height);
            Debug.Log("zall4 " + virtual_selfie.width);
            Debug.Log("zall4 " + virtual_selfie.height);




            //Texture2D my_img = LoadPNG(Application.dataPath + "/Resources/zall1.png");
            Mat real_matc = Unity.TextureToMat(real_selfie);
            Mat virtual_matc = Unity.TextureToMat(virtual_selfie);
            Debug.Log("zall1 " + real_matc.Size());
            Debug.Log("zall2 " + virtual_matc.Size());


            Mat result = new Mat(real_matc.Size(), MatType.CV_8UC1);

            //Rect rect = new Rect(2, 2, matc.Width - 2, matc.Height - 2);


            

            int x = System.Math.Min((int)(screenPos.x - (real_matc.Width / 5.0)), 0);
            int y = System.Math.Min((int)(screenPos.y - real_matc.Height / 5.0), 0);
            int w = (int)(real_matc.Width * (2.0 / 5.0));
            int h = (int)(real_matc.Height *(2.0 / 5.0));

            //Rect rect = new Rect((int)(real_matc.Width * (1.0 / 4.0)), (int)(real_matc.Height * (1.0 / 4.0)),(int)(real_matc.Width * (1.0 / 2.0)) , (int)(real_matc.Height * (1.0 / 2.0)));
            Rect rect = new Rect((int)screenPos.x, (int)screenPos.y, -150, -150);
            //Rect rect = new Rect((int)rectangle.p1.x, (int)rectangle.p1.y, (int)rectangle.GetWidth(), (int)rectangle.GetHeight());
            
            Cv2.GrabCut(virtual_matc, result, rect1, new Mat(), new Mat(), 3, GrabCutModes.InitWithRect);
            Mat mask1 = ((result & 1)) * 255;
            Mat mask2 = new Mat(mask1.Size(), MatType.CV_8UC1); ;
            mask2 = 255 - mask1;

            //applying the mask
            Mat cropped = new Mat();

            real_matc.CopyTo(cropped, mask2);
            virtual_matc.CopyTo(cropped, mask1);


            Debug.Log("zall" + x);
            Debug.Log("zall" + y);

            //Mat grayMat = new Mat();
            // Mat result_inpaint = new Mat();
            // Cv2.Inpaint(cropped, newResult, result_inpaint, 5, InpaintMethod.Telea);

            //Cv2.Rectangle(result_inpaint, rect, 1);
            //  Cv2.Circle(result_inpaint, new Point(screenPos.x, screenPos.y), 10, 1);

            //Cv2.Rectangle(cropped, rect, )



            byte[] byteArray5 = Unity.MatToTexture(cropped).EncodeToJPG();
            //byte[] byteArray2 = Unity.MatToTexture(cropped).EncodeToJPG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/Resources/result.png", byteArray5);
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