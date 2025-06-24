using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : MonoBehaviour
{
    public RectTransform background;  // Le cercle
    public RectTransform knob;        // Le bouton
    public Transform player;
    public float moveSpeed = 5f;

    private Vector2 inputDirection = Vector2.zero;
    private Canvas canvas;
    private Camera uiCamera;

    private bool isDragging = false;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        uiCamera = canvas.renderMode == RenderMode.ScreenSpaceCamera ? canvas.worldCamera : null;

        background.gameObject.SetActive(false);
    }

    void Update()
    {
        // Si clic gauche activer joystick
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clic gauche détecté");

            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                Input.mousePosition,
                uiCamera,
                out localPoint
            );

            background.anchoredPosition = localPoint;
            knob.anchoredPosition = Vector2.zero;
            background.gameObject.SetActive(true);
            isDragging = true;
        }

        // Si relâchement  désactiver joystick
        if (Input.GetMouseButtonUp(0))
        {
            inputDirection = Vector2.zero;
            knob.anchoredPosition = Vector2.zero;
            background.gameObject.SetActive(false);
            isDragging = false;
        }

        // Si on tient le clic, suivre la position
        if (isDragging)
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                background,
                Input.mousePosition,
                uiCamera,
                out localPoint
            );

            float radius = background.sizeDelta.x / 2f;
            inputDirection = Vector2.ClampMagnitude(localPoint, radius) / radius;
            knob.anchoredPosition = inputDirection * radius;

            // Déplacement joueur
            if (player != null && inputDirection != Vector2.zero)
            {
                Vector3 move = new Vector3(inputDirection.x, 0f, inputDirection.y) * moveSpeed * Time.deltaTime;
                player.position += move;
            }
        }
    }
}
