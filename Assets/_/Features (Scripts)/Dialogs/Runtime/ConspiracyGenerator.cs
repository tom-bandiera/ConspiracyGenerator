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
                m_currentDialogCollection = m_conspiratorsScriptable;
                m_canPlayAudio = true;
            }

            // Update is called once per frame
            void Update()
            {
                m_stateText.text = "State: " + m_currentState.ToString();

                if (!m_isConspiracyOver)
                {
                    AudioStateMachine();
                }
                
            }
    		    
		
        #endregion

        #region Main methods
	    
        private void AudioStateMachine()
        {
            if (m_currentAudioState != m_audioState.PLAYING && m_canPlayAudio)
            {
                // Pick a random line in current dialog collection
                m_currentLine = GetRandomLine(m_currentDialogCollection);
                // Render text and play audio
                PlayLine(m_currentLine);
                m_canPlayAudio = false;

            } else if (m_currentAudioState == m_audioState.PLAYING && m_audioSource.isPlaying == false) {
                m_currentAudioState = m_audioState.ENDED;

            } else if (m_currentAudioState == m_audioState.ENDED) {
                
                if (m_audioEndedCounter >= m_delayBetweenLines)
                {



                    if (m_currentState != DialogStateEnum.STATE.TERMINAL_ACTION)
                    {
                        // Pick next DialogCollection in current DialogCollection possible transitions
                        m_currentDialogCollection = GetRandomDialogCollectionFromTransitions(m_currentDialogCollection);
                        m_canPlayAudio = true;
                        m_iterations++;
                        if (m_iterations >= m_crazyIterationLimit)
                        {
                            m_isCrazy = true;
                        }

                    } else
                    {
                        m_isConspiracyOver = true;
                    }

                    m_currentState = m_currentDialogCollection.m_state;
                    m_audioEndedCounter = 0;

                } else {
                    m_audioEndedCounter += Time.deltaTime;

                }
            }
        }

        public void NewConspiracy()
        {
            m_currentState = DialogStateEnum.STATE.CONSPIRATOR;
            m_currentDialogCollection = m_conspiratorsScriptable;
            m_iterations = 0;
            m_isCrazy = false;
            m_canPlayAudio = true;
            m_audioEndedCounter = 0;
            m_isConspiracyOver = false;
        }

        #endregion

        #region Utils

        private int GenerateRandomIndex(int range)
        {
            return Random.Range(0, range);
        }

        private DialogCollectionScriptable.Line GetRandomLine(DialogCollectionScriptable dialogCollection)
        {
            return dialogCollection.m_lines[GenerateRandomIndex(dialogCollection.m_lines.Count)];
        }

        private void PlayLine(DialogCollectionScriptable.Line line)
        {
            m_lineText.text = line.m_text;
            m_audioSource.clip = m_isCrazy ? line.m_crazy : line.m_sane;
            m_audioSource.Play();

            m_currentAudioState = m_audioState.PLAYING;
        }

        private DialogCollectionScriptable GetRandomDialogCollectionFromTransitions(DialogCollectionScriptable dialogCollection)
        {
            int index = 0;
            float randomMax = 0;

            for (int i = 0; i < dialogCollection.m_transitions.Count; i++)
            {
                randomMax += dialogCollection.m_transitions[i].m_probability;
            }

            float random = Random.Range(0, randomMax);

            for (int i = 0; i < dialogCollection.m_transitions.Count; i++)
            {
                if (random < dialogCollection.m_transitions[i].m_probability)
                {
                    index = i;
                    break;
                }
            }

            return dialogCollection.m_transitions[index].m_dialogCollection;
        }

        #endregion

        #region Privates & Protected

        [SerializeField] private TMP_Text m_stateText;
        [SerializeField] private TMP_Text m_lineText;
        [SerializeField] private DialogCollectionScriptable m_conspiratorsScriptable;
        [SerializeField] private AudioSource m_audioSource;
        [SerializeField] private float m_delayBetweenLines;
        [SerializeField] private int m_crazyIterationLimit;

        private DialogStateEnum.STATE m_currentState;
        private DialogCollectionScriptable.Line m_currentLine;
        private DialogCollectionScriptable m_currentDialogCollection;

        private bool m_isCrazy;
        private bool m_canPlayAudio;
        private bool m_isConspiracyOver;

        private int m_iterations;
        private int m_lastDialogIndex;
        private float m_audioEndedCounter;

        private enum m_audioState
        {
            NONE,
            PLAYING,
            ENDED
        }

        private m_audioState m_currentAudioState;

        #endregion
    }
}
