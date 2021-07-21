using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCamera : MonoBehaviour
{
    public float rotateTime = 0.2f;
    private Transform player;
    private bool isRotating = false;

    float interpVelocity;

    public Vector3 offset;
    private Vector3 targetPos;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player").transform;
    }

    // Update is called once per frame

    void rotate()
    {
        if(Input.GetKeyDown(KeyCode.Q)&&!isRotating)
        {
            StartCoroutine(rotateAround(-45, rotateTime));
        }
        if(Input.GetKeyDown(KeyCode.E)&&!isRotating)
        {
            StartCoroutine(rotateAround(45, rotateTime));
        }
    }


    IEnumerator rotateAround(float angle,float time)
    {
        float number = 60 * time;
        float nextAngle = angle / number;
        isRotating = true;

        for(int i=0;i<number;i++)
        {
            transform.Rotate(new Vector3(0, 0, nextAngle));
            yield return new WaitForFixedUpdate();
        }
        isRotating = false;
    }
    void FixedUpdate()
    {
        rotate();
        Vector3 posNoZ = transform.position;
        posNoZ.z = player.position.z;

        Vector3 targetDirection = (player.position - posNoZ);

        interpVelocity = targetDirection.magnitude * 10f;

        targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

        transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);

    }
}
