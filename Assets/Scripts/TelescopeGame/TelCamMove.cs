using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelCamMove : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;
    public Vector3 offset;

    void Update()
    {
        transform.position = new Vector3(target.position.x + offset.x, target.position.y + offset.y, offset.z);
    }


}
