using UnityEngine;

public class WirePoint : MonoBehaviour
{
    [SerializeField] public int wireId;
    [SerializeField] public bool isStartPoint;
    [SerializeField] private GameObject wireSprite;

    private bool isDragging = false;
    private Vector2 startPosition;
    private bool isConnected = false;
    private float wireStartScaleY;
    private SpriteRenderer wireSpriteRenderer;

    private void Start()
    {
        startPosition = transform.position;
        wireSprite.SetActive(false);

        wireStartScaleY = wireSprite.transform.localScale.y;

        wireSpriteRenderer = wireSprite.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isStartPoint && isDragging && !isConnected)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            UpdateWireSprite(startPosition, mousePosition);
        }
    }

    private void OnMouseDown()
    {
        if (isStartPoint && !isConnected)
        {
            isDragging = true;
            wireSprite.SetActive(true);
        }
    }

    private void OnMouseUp()
    {
        if (isStartPoint && isDragging && !isConnected)
        {
            isDragging = false;

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                WirePoint endPoint = hit.collider.GetComponent<WirePoint>();
                if (endPoint != null && !endPoint.isStartPoint && endPoint.wireId == wireId)
                {
                    isConnected = true;
                    UpdateWireSprite(startPosition, endPoint.transform.position);
                }
                else
                {
                    ResetWire();
                }
            }
            else
            {
                ResetWire();
            }
        }
    }

    private void UpdateWireSprite(Vector2 start, Vector2 end)
    {
        Vector2 direction = end - start;
        float length = direction.magnitude;

        wireSpriteRenderer.size = new Vector2(length * 6 + 5, wireSpriteRenderer.size.y);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        wireSprite.transform.rotation = Quaternion.Euler(0, 0, angle);

        wireSprite.transform.position = start;
    }

    private void ResetWire()
    {
        wireSpriteRenderer.size = new Vector2(1, wireSpriteRenderer.size.y);
        wireSprite.transform.rotation = Quaternion.identity;
        wireSprite.SetActive(false);
    }
}
