using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "new narration sequence", menuName = "Scriptables/Create new narration sequence")]
public class NarrationSequence : ScriptableObject
{
    [TextArea(minLines:3, maxLines:10)]
    public string[] npc_sentences;
    public PlayerChoice[] player_choices;

    
    public MyEvent myEvent;

    public void callEvent()
    {
        myEvent.Invoke();
    }

}

[Serializable]
public class MyEvent : UnityEvent { }
