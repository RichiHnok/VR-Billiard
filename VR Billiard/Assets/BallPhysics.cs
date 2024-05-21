using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    public float speedX;
    public float speedY;
    public float speedZ;

    // private static float g = 100f;

    // private float slowForce = g*0.5f/((float)10e+6);
    private float slowForce = ((float)9e-5);

    //floatt is called before the first frame update
    void Start()
    {
        print("Check");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Vector3 pos1 = transform.position;
        if(speedX != 0 || speedZ != 0){
            if(Math.Abs(speedX) - slowForce >= 0.0001){
                speedX -= (speedX/Math.Abs(speedX))*slowForce;
            }else{
                speedX = 0;
            }
            
            if(Math.Abs(speedZ) - slowForce >= 0.0001){
                speedZ -= (speedZ/Math.Abs(speedZ))*slowForce;
            }else{
                speedZ = 0;
            }
        }

        transform.localPosition += new Vector3(speedX, 0, speedZ);
    }

    public bool processingColision = true;
    void OnTriggerEnter(Collider colidingObject)
    {
        print("Collision happened between " + this.gameObject + " and " + colidingObject.gameObject);

        if(colidingObject.gameObject.transform.position.y < this.gameObject.transform.position.y){
            speedY = 0;
        }

        if (colidingObject.gameObject.tag == "Ball")
        {
            // print(colidingObject.gameObject.GetComponent<BallPhysics>().processingColision);
            if(!colidingObject.gameObject.GetComponent<BallPhysics>().processingColision){
                processingColision = true;
            
                BallPhysics ball2 = colidingObject.gameObject.GetComponent<BallPhysics>();

                Vector3 ball1Pos = transform.position;
                Vector3 ball2Pos = colidingObject.gameObject.transform.position;

                double vx1 = speedX; // speed x1
                double vy1 = speedZ; // speed y1
                double vx2 = ball2.speedX; // speed x2
                double vy2 = ball2.speedZ; // spedd y2
                double x1 = ball1Pos.x;
                double y1 = ball1Pos.z;
                double x2 = ball2Pos.x;
                double y2 = ball2Pos.z;
                double dx = x2 - x1;
                double dy = y2 - y1;

                double u1x = (dx*dx*vx2 + dx*dy*(vy2 - vy1) - dy*dy*vx1)/(dx*dx + dy*dy);
                double u1y = (dy*dy*vy2 + dx*dy*(vx2 - vx1) - dx*dx*vy1)/(dx*dx + dy*dy);

                double u2x = (dx*dx*vx1 + dx*dy*(vy1 - vy2) - dy*dy*vx2)/(dx*dx + dy*dy);
                double u2y = (dy*dy*vy1 + dx*dy*(vx1 - vx2) - dx*dx*vy2)/(dx*dx + dy*dy);

                speedX = ((float) u1x);
                speedZ = ((float) u1y);

                colidingObject.gameObject.GetComponent<BallPhysics>().setSpeed(u2x, u2y);
            }else{
                colidingObject.gameObject.GetComponent<BallPhysics>().setColl(false);
                return;
            }
        }else if (colidingObject.gameObject.tag == "VerticalWall"){
            speedZ *= -1;
        }else if(colidingObject.gameObject.tag == "HorizontalWall"){
            speedX *= -1;
        }else if (colidingObject.gameObject.tag == "Tip"){

            Vector3 ball1Pos = transform.position;
            Vector3 ball2Pos = colidingObject.gameObject.transform.position;

            double vx1 = speedX; // speed x1
            double vy1 = speedZ; // speed y1
            double vx2 = colidingObject.gameObject.GetComponent<CalculateSpeed>().speedX; // speed x2
            double vy2 = colidingObject.gameObject.GetComponent<CalculateSpeed>().speedZ; // spedd y2
            double x1 = ball1Pos.x;
            double y1 = ball1Pos.z;
            double x2 = ball2Pos.x;
            double y2 = ball2Pos.z;
            double dx = x2 - x1;
            double dy = y2 - y1;

            double u1x = (dx*dx*vx2 + dx*dy*(vy2 - vy1) - dy*dy*vx1)/(dx*dx + dy*dy);
            double u1y = (dy*dy*vy2 + dx*dy*(vx2 - vx1) - dx*dx*vy1)/(dx*dx + dy*dy);

            speedX = ((float) u1x);
            speedZ = ((float) u1y);
        }else if(colidingObject.gameObject.tag == "Zone"){
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            this.gameObject.GetComponent<SphereCollider>().isTrigger = false;
            this.gameObject.GetComponent<BallPhysics>().enabled = false;
        }
    }

    void OnTriggerStay(Collider colidingObject){
        
        if(colidingObject.gameObject.transform.position.y < this.gameObject.transform.position.y){
            speedY = 0;
        }else{
            speedY = -0.01f;
        }
    }

    void OnTriggerExit(){
        speedY = -0.01f;
    }

    public void setColl(bool procCall){
        print("set procCall in " + this.gameObject);
        processingColision = procCall;
    }

    public void setSpeed(double vx, double vz){
        print("set speed in " + this.gameObject);
        speedX = ((float)vx);
        speedZ = ((float)vz);
    }
}
