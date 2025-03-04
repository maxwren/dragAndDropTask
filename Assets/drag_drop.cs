using UnityEngine;
using UnityEngine.EventSystems;
public class drag_drop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform r_transform;
    private CanvasGroup canvas_group;
    private bool is_caught;
    private bool is_dragging;
    [SerializeField] private Canvas main_canvas;
    [SerializeField] private float falling_speed;
    [SerializeField] private Transform ground;

    //Управление осуществленно через IPointer методы поскольку это упрощает процесс тестирования и ускоряет разработку
    //Вне тестового задания я бы использовал touch ивенты вместе с пакетом Input System

    private void Awake()
    {
        r_transform = GetComponent<RectTransform>();
        canvas_group = GetComponent<CanvasGroup>();
    }
    public void catch_object()
    {
        is_caught = true;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //we need our pointer to trigger events on the frame that catches this object so we need to allow pointer's raycast to go through this object and land on the frame object
        canvas_group.blocksRaycasts = false;

        is_dragging = true;
        is_caught = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //here we add the delta of our pointer to the position of the object to move it, and divide it by canvas' scale factor so that it works correctly with scaling on different resolutions
        r_transform.anchoredPosition += eventData.delta / main_canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvas_group.blocksRaycasts = true;
        is_dragging = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        is_caught = true;
    }
    private void Update()
    {
        if (is_caught || is_dragging)
        {
            return;
        }
        else if (transform.position.y > ground.position.y) // if the object is not being dragged or is caught by a frame, and it has not hit the ground yet, it falls down
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - falling_speed);
        }
    }
}
