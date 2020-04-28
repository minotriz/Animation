using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera3ra : MonoBehaviour
{
    public Transform lookAt;
    Vector3 positionIncrease;

    public float distance;
    float fixedDist;

    float sensitiveWheel;


    // Start is called before the first frame update
    void Start()
    {
        positionIncrease = lookAt.forward * distance;
        transform.position = lookAt.position - positionIncrease;
        transform.LookAt(lookAt);

        sensitiveWheel = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        distance += Input.GetAxis("Mouse ScrollWheel") * sensitiveWheel;
        distance = Mathf.Clamp(distance, 2f, 6f);

        fixedDist = fixDistance();
    }

    void LateUpdate()
    {
        positionIncrease = lookAt.forward * fixedDist;

        transform.position = Vector3.Lerp(transform.position, lookAt.position - positionIncrease, 25f * Time.deltaTime);

        transform.LookAt(lookAt);

        transform.position = new Vector3(transform.position.x, 3, transform.position.z);
    }

    float fixDistance()
    {
        RaycastHit hit;

        if (Physics.Raycast(lookAt.position, -lookAt.forward, out hit, distance))
        {
            Debug.DrawLine(lookAt.position, hit.point, Color.red);
            return hit.distance;
        }
        return distance;
    }
}
