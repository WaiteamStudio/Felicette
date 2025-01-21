using UnityEngine;

public class CameraController : MonoBehaviour, ICameraController
{
    public void FocusOn(Transform target)
    {
        if (target != null)
        {
            Camera.main.transform.position = new Vector3(target.position.x, target.position.y, Camera.main.transform.position.z);
        }
    }
}
