using UnityEngine;
using UnityEngine.Events;

public class ClickableObject : MonoBehaviour
{
    [SerializeField] private UnityEvent m_onClick;

    private void OnMouseDown()
    {
        m_onClick.Invoke();
    }
}