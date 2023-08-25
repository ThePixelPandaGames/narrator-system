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
        if (!NarrationManager.Instance.is_npc_talking && !NarrationManager.Instance.is_player_talking)
        {
            NarrationManager.Instance.ContinueNarration();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(!NarrationManager.Instance.is_npc_talking && !NarrationManager.Instance.is_player_talking && allowContinuationOnEnter && Input.GetKeyDown(KeyCode.KeypadEnter)) {
            NarrationManager.Instance.ContinueNarration();
        }
    }
}
