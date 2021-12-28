using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DayCycleColors : MonoBehaviour
{
    [SerializeField] private AnimationCurve m_saturation;
    [SerializeField] private Gradient m_gradient;

    [SerializeField] private VolumeProfile m_volume;

    [SerializeField, Range(0f, 1f)] private float m_time = 1f;

    private bool m_hasColors;
    private ColorAdjustments m_colors;

    private void Awake()
    {
        m_hasColors = m_volume.TryGet<ColorAdjustments>(out m_colors);
    }

    private void Update()
    {
        SetTime(m_time);
    }

    private void SetTime(float t)
    {
        if (m_hasColors)
        {
            m_colors.saturation.value = m_saturation.Evaluate(t);
            m_colors.colorFilter.value = m_gradient.Evaluate(t);
        }
    }
}
