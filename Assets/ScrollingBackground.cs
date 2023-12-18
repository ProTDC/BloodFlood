using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    private float lengthX, lengthY, startPos;
    public GameObject cam;
    public float parallaxEffect;

    private void Start()
    {
        startPos = transform.position.x;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
        lengthY = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void FixedUpdate()
    {
        float tempX = (cam.transform.position.x * (1 - parallaxEffect));
        float tempY = (cam.transform.position.y * (1 - parallaxEffect));
        float distX = (cam.transform.position.x * parallaxEffect);
        float distY = (cam.transform.position.y * parallaxEffect);

        transform.position = new Vector3(startPos + distX, distY, transform.position.z);

        if (tempX > startPos + lengthX) 
        {
            startPos += lengthX;
        }
        else if (tempX < startPos - lengthX)
        {
            startPos -= lengthX;    
        }

        if (tempY > startPos + lengthY)
        {
            startPos += lengthY;
        }
        else if (tempY < startPos - lengthY)
        {
            startPos -= lengthY;
        }
    }

}
