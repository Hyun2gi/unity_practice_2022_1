using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        //target의 위치에서 보정값을 더한값이다.
        transform.position = target.position + offset;
    }
}