using System;
using UnityEngine;
using UnityEngine.Events;


namespace LightWeightNarrationTool
{
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

        // update v1.1
        [SerializeField]
        private NarrationSequence nextNarrationSequence;

        [SerializeField]
        private Sprite npcImage;

        public void CallEvent()
        {
            MyEvent.Invoke();
        }

        public string[] NpcSentences { get => npcSentences; }
        public PlayerChoice[] PlayerChoices { get => playerChoices; }
        public MyEvent MyEvent { get => myEvent; }
        public string NarratorName { get => narratorName; }

        // update v1.1
        public Sprite NpcImage { get => npcImage; set => npcImage = value; }
        public NarrationSequence NextNarrationSequence { get => nextNarrationSequence; set => nextNarrationSequence = value; }


    }

    [Serializable]
    public class MyEvent : UnityEvent { }
}
