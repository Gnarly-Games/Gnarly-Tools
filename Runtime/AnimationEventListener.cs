using UnityEngine;
using UnityEngine.Events;

namespace Sources.Utility
{
    public class AnimationEventListener : MonoBehaviour
    {
        public UnityEvent onTriggerA;
        public UnityEvent onTriggerB;

        // Called from animation
        public void TriggerA()
        {
            onTriggerA?.Invoke();
        }

        public void TriggerB()
        {
            onTriggerB?.Invoke();
        }
    }
}