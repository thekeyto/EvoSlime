using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public GameObject target;
    float interpVelocity;

    public Vector3 offset;
    private Vector3 targetPos;

    public Transform rightTop;
    public Transform leftBottom;

    // Use this for initialization
    void Start()
    {
        targetPos = transform.position;
    }

    // Update is called once per frame

    float checkx()
    {
        if (target.transform.position.x-3 < leftBottom.position.x) return leftBottom.position.x+3;
        if (target.transform.position.x+3 < leftBottom.position.x) return rightTop.position.x-3;
        return target.transform.position.x;
    }

    float checky()
    {
        if (target.transform.position.y - 3 < leftBottom.position.y) return leftBottom.position.y + 3;
        if (target.transform.position.y + 3 < leftBottom.position.y) return rightTop.position.y - 3;
        return target.transform.position.y;
    }

    void FixedUpdate()
    {
        if (target)
        {
            Vector3 posNoZ = transform.position;
            posNoZ.z = target.transform.position.z;

            Vector3 targetDirection = (new Vector3(checkx(),checky(),0) - posNoZ);

            interpVelocity = targetDirection.magnitude * 5f;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);

        }
    }
}
