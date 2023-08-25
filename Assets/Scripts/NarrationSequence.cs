using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "new narration sequence", menuName = "Scriptables/Create new narration sequence")]
public class NarrationSequence : ScriptableObject
{
    [TextArea(minLines: 3, maxLines: 10)]
    [SerializeField]
    private string[] npcSentences;

    [SerializeField]
    private string narratorName;


    [SerializeField]
    private PlayerChoice[] playerChoices;

    [SerializeField]
    private MyEvent myEvent;

    public void callEvent()
    {
        MyEvent.Invoke();
    }

    public string[] NpcSentences { get => npcSentences;}
    public PlayerChoice[] PlayerChoices { get => playerChoices;}
    public MyEvent MyEvent { get => myEvent;}
    public string NarratorName { get => narratorName; }
}

[Serializable]
public class MyEvent : UnityEvent { }
