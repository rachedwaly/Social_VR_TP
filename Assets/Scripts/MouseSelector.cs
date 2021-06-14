using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSelector : MonoBehaviour
{
    private Vector2[] rectanglePoints = new Vector2[4];
    private int counter = 0;

    public int cameraResolutionWidth = 1920;
    public int cameraResolutionHeight = 1080;

    public static Action<Rectangle> onPointsSelected;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null && hit.transform.gameObject.GetComponent<Renderer>().material.mainTexture != null)
                {
                    Debug.Log("X pixels: " + hit.textureCoord.x * cameraResolutionWidth + " Y pixels: " + hit.textureCoord.y * cameraResolutionHeight);
                    rectanglePoints[counter] = new Vector2(hit.textureCoord.x * cameraResolutionWidth, hit.textureCoord.y * cameraResolutionHeight);

                    counter++;
                    if(counter == 4)
                    {
                        counter = 0;
                        Rectangle rectangle = new Rectangle(rectanglePoints[0], rectanglePoints[1], rectanglePoints[2], rectanglePoints[3]);
                        onPointsSelected?.Invoke(rectangle);

                        Debug.Log("Centroid X: " + rectangle.GetCentroid().x + " Centroid Y: " + rectangle.GetCentroid().y);
                        Debug.Log("Width: " + rectangle.GetWidth());
                        Debug.Log("Height: " + rectangle.GetHeight());
                    }
                }
            }
        }
    }
}

public struct Rectangle
{
    public Vector2 p1, p2, p3, p4;

    public Rectangle(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4) : this()
    {
        this.p1 = p1;
        this.p2 = p2;
        this.p3 = p3;
        this.p4 = p4;
    }

    float width { get; set; }
    float height { get; set; }
    Vector2 centroid { get; set; }

    public Vector2 GetCentroid()
    {
        centroid = new Vector2((p1.x + p2.x) / 2f, (p2.y + p3.y) / 2f);
        return centroid;
    }

    public float GetWidth()
    {
        width = (p2.x - p1.x);
        return width;
    }

    public float GetHeight()
    {
        height = (p3.y - p2.y);
        return height;
    }
}
