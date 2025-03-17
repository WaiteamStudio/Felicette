using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; 
    public float minX; 
    public float maxX;

    private Vector3 offset;

    void LateUpdate()
    {
        float targetX = player.position.x;

        float clampedX = Mathf.Clamp(targetX, minX, maxX);

        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
