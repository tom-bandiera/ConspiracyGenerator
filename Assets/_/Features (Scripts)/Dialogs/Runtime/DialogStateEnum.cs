using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogs.Runtime
{
    public class DialogStateEnum : MonoBehaviour
    {
        #region Publics
	    public enum STATE {
            CONSPIRATOR,
            LINKABLE_LINE,
            LOOPING_ACTION,
            REACTION,
            SEMI_TERMINAL_ACTION,
            TERMINAL_ACTION,
            VICTIM_LINK_LINE,
            VICTIM
        }
        #endregion
    }

}
