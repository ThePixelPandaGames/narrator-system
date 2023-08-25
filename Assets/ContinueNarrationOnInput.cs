using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ContinueNarrationOnInput : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private bool allowContinuationOnEnter = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        try
        {
            if (NarrationManager.Instance.NoOneIsTalking())
            {
                NarrationManager.Instance.ContinueNarration();
            }
        }
        catch (NullReferenceException e)
        {
            Debug.LogException(e);
            Debug.LogError("NarrationManager possibly not set up in your scene/project. Check if you have one (and only one) instance.");
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            try
            {
                if (NarrationManager.Instance.NoOneIsTalking() && allowContinuationOnEnter)
                {
                    NarrationManager.Instance.ContinueNarration();
                }
            }
            catch (NullReferenceException e)
            {
                Debug.LogException(e);
                Debug.LogError("NarrationManager possibly not set up in your scene/project. Check if you have one (and only one) instance.");

            }
        }
    }
}
