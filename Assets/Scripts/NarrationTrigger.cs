using System;
using UnityEngine;

public class NarrationTrigger : MonoBehaviour
{
    [SerializeField]
    private NarrationSequence narrationSequence;



    void Start()
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
