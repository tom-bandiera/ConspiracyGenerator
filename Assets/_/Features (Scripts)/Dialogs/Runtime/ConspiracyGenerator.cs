using TMPro;
using UnityEngine;

namespace Dialogs.Runtime
{
    public class ConspiracyGenerator : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity API

            // Start is called before the first frame update
            void Start()
            {
                m_currentState = DialogStateEnum.STATE.CONSPIRATOR;
            }

    		// Update is called once per frame
    		void Update()
    		{
                m_stateText.text = "State: " + m_currentState.ToString();
                StateMachine();
    		}
		
        #endregion

        #region Main methods
	    
        private void StateMachine()
        {
            switch (m_currentState)
            {
                case(DialogStateEnum.STATE.CONSPIRATOR):

                    GenerateDialog(m_conspiratorsScriptable);

                    m_currentState = DialogStateEnum.STATE.LOOPING_ACTION;
                    
                    break;
                case (DialogStateEnum.STATE.LOOPING_ACTION):

                    Debug.Log(m_audioSource.isPlaying);

                    break;
            }
        }

        #endregion

        #region Utils

        private int GenerateRandom(int range)
        {
            return Random.Range(0, range);
        }

        private void GenerateDialog(DialogCollectionScriptable dialogCollection)
        {

            var dialog = dialogCollection.m_lines[GenerateRandom(dialogCollection.m_lines.Count)];

            m_lineText.text = dialog.m_text;
            m_audioSource.clip = m_isCrazy ? dialog.m_crazy : dialog.m_sane;
            m_audioSource.Play();
        }

        #endregion

        #region Privates & Protected

        [SerializeField] private TMP_Text m_stateText;
        [SerializeField] private TMP_Text m_lineText;
        [SerializeField] private DialogCollectionScriptable m_conspiratorsScriptable;
        [SerializeField] private DialogCollectionScriptable m_loopingActionsScriptable;
        [SerializeField] private AudioSource m_audioSource;

        private DialogStateEnum.STATE m_currentState;
        private bool m_isCrazy;

        private float m_currentDialogCounter;
        private float m_currentDialogLength;
        private int m_lastDialogIndex;
        


        #endregion
    }
}
