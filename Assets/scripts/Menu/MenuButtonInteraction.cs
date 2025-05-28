using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuButtonInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Hover Events")]
    [SerializeField] private UnityEvent onHoverEnter;
    [SerializeField] private UnityEvent onHoverExit;

    [Header("Key Pressed Events")]
    [SerializeField]
    private UnityEvent OnKeyPressed;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            OnKeyPressed.Invoke();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onHoverEnter.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onHoverExit.Invoke();
    }
}
