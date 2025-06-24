using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Transform player; 
    public float moveSpeed = 5f;

    private Vector2 dragInput = Vector2.zero;
    private bool isDragging = false;

    void Update()
    {
        if (isDragging && player != null)
        {
            Vector3 move = new Vector3(dragInput.x, 0f, dragInput.y) * moveSpeed * Time.deltaTime;
            player.position += move;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UpdateDrag(eventData);
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        dragInput = Vector2.zero;
    }

    private void UpdateDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint
        );

        Vector2 normalized = localPoint.normalized;

        // Clamp la direction pour éviter des mouvements trop forts
        dragInput = Vector2.ClampMagnitude(normalized, 1f);
    }
}
