using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateSpeed : MonoBehaviour
{
    public float speedX;
    public float speedY;
    public float speedZ;
    private Vector3 lastPos;


    // Update is called once per frame
    void Update()
    {
    // //Find the difference
        Vector3 speed = this.transform.position - lastPos;
    // positionChange = currentPosition - lastPosition

    // //Make it a positive value if it is negative
    // speed = Mathf.Abs(positionChange)

    // //make it frame rate independent (Speed per second?)
    // speed /= time.deltatime

    // //Update the last position
        lastPos = this.transform.position;
        
        speedX = speed.x;
        speedY = speed.y;
        speedZ = speed.z;
    }
}
