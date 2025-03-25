using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToCamera : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.position = Camera.main.transform.position;
    }
}
