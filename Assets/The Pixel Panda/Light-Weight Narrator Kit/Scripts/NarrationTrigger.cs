using System;
using UnityEngine;

namespace LightWeightNarrationTool
{
    public class NarrationTrigger : MonoBehaviour
    {
        enum TriggerEvent
        {
            OnStart,
            OnTriggerEnter,
            OnCollisionEnter,
            OnMouseClick,
            OnTriggerEnter2D,
            OnCollisionEnter2D
        }


        [SerializeField]
        private NarrationSequence narrationSequence;

        [SerializeField]
        private TriggerEvent triggerEvent;

        void Start()
        {
            if (triggerEvent == TriggerEvent.OnStart)
            {
                TriggerNarration();
            }
        }

        private void OnMouseDown()
        {
            if (triggerEvent == TriggerEvent.OnMouseClick)
            {
                TriggerNarration();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (triggerEvent == TriggerEvent.OnTriggerEnter)
            {
                TriggerNarration();

            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (triggerEvent == TriggerEvent.OnCollisionEnter)
            {
                TriggerNarration();

            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (triggerEvent == TriggerEvent.OnTriggerEnter2D)
            {
                TriggerNarration();

            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (triggerEvent == TriggerEvent.OnCollisionEnter2D)
            {
                TriggerNarration();

            }
        }

        private void TriggerNarration()
        {
            try
            {
                NarrationManager.Instance.StartNarration(narrationSequence);
            }
            catch (NullReferenceException e)
            {
                Debug.LogException(e);
                Debug.LogError("NarrationManager possibly not set up in your scene/project. Check if you have one (and only one) instance.");
            }
        }
    }
}
