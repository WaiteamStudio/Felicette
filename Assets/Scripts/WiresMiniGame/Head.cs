using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField] private SpriteRenderer targetSpriteRenderer;

    private void Update()
    {
        if (targetSpriteRenderer != null)
        {
            Vector2 spriteSize = targetSpriteRenderer.size;

            Vector2 localAttachmentPoint = new Vector2(spriteSize.x, 0);

            Vector2 worldAttachmentPoint = targetSpriteRenderer.transform.TransformPoint(localAttachmentPoint);

            transform.position = worldAttachmentPoint;
        }
    }
}
