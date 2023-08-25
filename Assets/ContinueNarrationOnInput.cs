using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ContinueNarrationOnInput : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private bool allowContinuationOnEnter = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        NarrationManager.Instance.ContinueNarration();
    }

    // Update is called once per frame
    void Update()
    {
        if(allowContinuationOnEnter && Input.GetKeyDown(KeyCode.KeypadEnter)) {
            NarrationManager.Instance.ContinueNarration();
        }
    }
}
