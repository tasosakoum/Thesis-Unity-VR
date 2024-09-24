using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class SignalReceiver: MonoBehaviour {
    public abstract void OnSignalReceived(ObjectInteractStates nextState);
}
