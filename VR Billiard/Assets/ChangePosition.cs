using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject rightHand = GameObject.FindGameObjectWithTag("SphereR");
        transform.position = rightHand.transform.position;
        GameObject leftHand = GameObject.FindGameObjectWithTag("SphereL");
        Vector3 newPos = rightHand.transform.position - leftHand.transform.position;
        transform.rotation = Quaternion.LookRotation(newPos, Vector3.up);
    }
}
