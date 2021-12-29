using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlantState
{
    AtHome,
    OutsideWander,
    OutsidePlay
}

public enum EmotionalState
{
    Normal,
    Sad,
    Happy
}

public class PlantController : MonoBehaviour
{
    [SerializeField] private PlantModel[] m_models;
    [SerializeField] private int m_currentModelIndex;
    [SerializeField] private UnityEngine.AI.NavMeshAgent m_agent;
    [SerializeField] private Transform m_positionTarget;
    [SerializeField] private EmotionalState m_emotionalState;
    [SerializeField] private PlantState m_state;

    private Vector3 m_previousPosition;

    private Coroutine m_coroutine = null;

    private PlantModel Model { get => m_models[m_currentModelIndex]; }

    public void GoToTarget()
    {
        m_agent.SetDestination(m_positionTarget.position);
    }

    public void SetEmotionalState(EmotionalState state)
    {
        m_emotionalState = state;
    }

    public void GoHome()
    {
        if (m_coroutine != null)
        {
            m_coroutine = StartCoroutine(GoHomeCoroutine());
        }
    }

    public void GoOutside()
    {
        Model.ExitPot();
        m_state = PlantState.OutsideWander;
    }

    private IEnumerator GoHomeCoroutine()
    {
        // m_agent.SetDestination(Model.EnterPotTarget.position);
        // while (Vector3.Distance(Model.transform.position, .position) > 0.1f)
        // {
        //     yield return null;
        // }
        Model.EnterPot();
        m_state = PlantState.AtHome;
        m_coroutine = null;
        yield break;
    }

    public void SetModel(PlantModel model)
    {
        for (int i = 0; i < m_models.Length; i++)
        {
            bool active = model == m_models[i];
            m_models[i].gameObject.SetActive(active);
            if (active)
            {
                m_currentModelIndex = i;
            }
        }
    }

    private void Update()
    {
        Model.speed = Vector3.Distance(m_previousPosition, transform.position) * Time.deltaTime;
        m_previousPosition = transform.position;
        Model.UpdateAnimator();
    }
}
