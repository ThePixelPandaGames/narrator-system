using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NarrationTrigger : MonoBehaviour
{
    public NarrationSequence narrationSequence;


    void Start()
    {
        NarrationManager.Instance.StartNarration(narrationSequence);
    }


}
