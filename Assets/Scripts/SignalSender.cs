using System;
using UnityEngine;

[Serializable]
public abstract class SignalSender : MonoBehaviour
{
    [SerializeField] private SerializableDictionary<GameObject, ObjectInteractStates> receiversToSignals;

    protected void SendSignals(SerializableDictionary<GameObject, ObjectInteractStates> signals = null) {
        foreach (var receiver in signals ?? receiversToSignals) {
            var signalReceiver = receiver.key.GetComponent<SignalReceiver>();
            
            if (signalReceiver != null) 
                signalReceiver.OnSignalReceived(receiver.value);
        }
    }
}
