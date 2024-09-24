using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton : SignalSender, Interactable {
    
    public void OnInteract() {
        SendSignals();
    }
}

