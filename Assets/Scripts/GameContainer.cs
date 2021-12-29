using UnityEngine;
using UnityEngine.Events;

public class GameContainer : MonoBehaviour
{
    [SerializeField] private UnityEvent m_onGameEnter;
    [SerializeField] private UnityEvent m_onGameExit;

    private bool m_entered = false;

    public void OnGameEnter()
    {
        if (!m_entered)
        {
            m_onGameEnter.Invoke();
            m_entered = true;
        }
    }

    public void OnGameExit()
    {
        if (m_entered)
        {
            m_onGameExit.Invoke();
            m_entered = false;
        }
    }
}