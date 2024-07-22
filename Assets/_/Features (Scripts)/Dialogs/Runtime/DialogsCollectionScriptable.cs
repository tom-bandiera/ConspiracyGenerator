using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogs.Runtime
{
    [CreateAssetMenu]
    public class DialogCollectionScriptable : ScriptableObject
    {
        #region Publics

        public DialogStateEnum.STATE m_state;
        public List<Line> m_lines = new List<Line>();
        public List<Transition> m_transitions = new List<Transition>();

        [System.Serializable]
        public struct Line
        {
            public string m_text;
            public AudioClip m_sane;
            public AudioClip m_crazy;
        }

        [System.Serializable]
        public struct Transition
        {
            public string m_name;
            public DialogCollectionScriptable m_dialogCollection;
            [Range(0f, 1f)] public float m_probability;
        }

        #endregion

        #region Unity API

        #endregion

        #region Main methods

        #endregion

        #region Utils

        #endregion

        #region Privates & Protected

        // [SerializeField] private Line[] m_lines;

        #endregion


    }
}

