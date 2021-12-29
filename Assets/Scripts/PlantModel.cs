using UnityEngine;

public class PlantModel : MonoBehaviour
{
    private static class AnimationKeys
    {
        public const string Speed = nameof(Speed);
        public const string GetIn = nameof(GetIn);
        public const string GetOut = nameof(GetOut);
        public const string Jump = nameof(Jump);
        public const string Emotion = nameof(Emotion);

        public const int EmotionNormal = (int) EmotionalState.Normal;
        public const int Happy = (int) EmotionalState.Happy;
        public const int Sad = (int) EmotionalState.Sad;
    }

    [SerializeField] private Transform m_enterPotTarget;
    [SerializeField] private Animator m_animator;
    
    public float speed;

    public void UpdateAnimator()
    {
        m_animator.SetFloat(AnimationKeys.Speed, speed);
    }

    public void EnterPot()
    {
        m_animator.SetTrigger(AnimationKeys.GetIn);
    }

    public void ExitPot()
    {
        m_animator.SetTrigger(AnimationKeys.GetOut);
    }
}