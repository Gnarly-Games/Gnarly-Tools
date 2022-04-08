using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Sources.Utility
{
    [RequireComponent(typeof(Collider))]
    public class TriggerEventListener : MonoBehaviour
    {
        [Serializable]
        public class TriggerEvent
        {
            public enum EventType
            {
                Enter,
                Exit,
                Stay
            }

            public string Tag;
            public EventType Type;
            public UnityEvent<GameObject> Event;
        }

        public List<TriggerEvent> events;

        private void Start()
        {
            var collider = GetComponent<Collider>();
            if (!collider.isTrigger)
                Debug.LogError($"{gameObject.name} collider must be trigger");
        }

        private void OnTriggerEnter(Collider other)
        {
            foreach (var trigger in events)
            {
                if (trigger.Type == TriggerEvent.EventType.Enter)
                {
                    if (other.CompareTag(trigger.Tag))
                    {
                        trigger.Event.Invoke(other.gameObject);
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            foreach (var trigger in events)
            {
                if (trigger.Type == TriggerEvent.EventType.Exit)
                {
                    if (other.CompareTag(trigger.Tag))
                    {
                        trigger.Event.Invoke(other.gameObject);
                    }
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            foreach (var trigger in events)
            {
                if (trigger.Type == TriggerEvent.EventType.Stay)
                {
                    if (other.CompareTag(trigger.Tag))
                    {
                        trigger.Event.Invoke(other.gameObject);
                    }
                }
            }
        }
    }
}