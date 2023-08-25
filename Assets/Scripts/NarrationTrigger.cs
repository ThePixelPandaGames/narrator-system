using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NarrationTrigger : MonoBehaviour
{
    public NarrationSequence narrationSequence;


    void Start()
    {
        try
        {
            NarrationManager.Instance.StartNarration(narrationSequence);
        }catch(NullReferenceException e)
        {
            Debug.LogException(e);
            Debug.LogError("NarrationManager possibly not set up in your scene/project. Check if you have one (and only one) instance.");
        }
    }


}
