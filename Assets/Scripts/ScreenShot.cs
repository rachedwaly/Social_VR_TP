namespace OpenCvSharp
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public class ScreenShot : MonoBehaviour
    {

        private Camera myCamera;
        private static ScreenShot instance;
        private bool takeScreenShotFrame;
        
        private Camera virtualCamera;
        private Transform target;
        private Vector3 screenPos;

        private void Awake()
        {
            instance = this;
            myCamera = gameObject.GetComponent<Camera>();

            //virtualCamera = GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/RightHandAnchor/VirtualCamera").GetComponent<Camera>();
            target = GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/head").GetComponent<Transform>();

            Handler.onScreenShotTaken += TakeScreenshot;

        }

        private void OnPostRender()
        {
            if (takeScreenShotFrame)
            {
                
                takeScreenShotFrame = false;
                RenderTexture renderTexture = myCamera.targetTexture;

                Texture2D screenshot = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
                UnityEngine.Rect rect = new UnityEngine.Rect(0, 0, renderTexture.width, renderTexture.height);
                screenshot.ReadPixels(rect, 0, 0);
                

                Mat matc=Unity.TextureToMat(screenshot);
                Cv2.Circle(matc, new Point(screenPos.x, screenPos.y), 10, 1);
                screenshot = Unity.MatToTexture(matc);
                

                byte[] byteArray = screenshot.EncodeToPNG();
                System.IO.File.WriteAllBytes(Application.dataPath + "/Resources/SelfieInGame.png", byteArray);
                AssetDatabase.Refresh();

            }
        }

        private void TakeScreenshot()
        {
            Debug.Log("zallsqdsqds");
            takeScreenShotFrame = true;
            screenPos = myCamera.WorldToScreenPoint(target.position);
            screenPos.y = virtualCamera.pixelHeight - screenPos.y;

        }

        


    }
}