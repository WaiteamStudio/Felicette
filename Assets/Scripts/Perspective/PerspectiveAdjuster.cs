using UnityEngine;

public class PerspectiveAdjuster : MonoBehaviour
{
    [SerializeField] private float perspectiveScale;
    [SerializeField] private float scaleRatio;

    private void Update()
    {
        AdjustPerspective();
    }

    private void AdjustPerspective()
    {
        Vector3 scale = transform.localScale;
        scale.x = perspectiveScale * (scaleRatio - transform.position.y);
        scale.y = perspectiveScale * (scaleRatio - transform.position.y);
        transform.localScale = scale;
    }

    public void ChangePerspectiveScale(float newPerspectiveScale)
    {
        perspectiveScale = newPerspectiveScale;
    }

    public void ChangeScaleRatio(float newScaleRatio)
    {
        scaleRatio = newScaleRatio;
    }
}

