using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable {
    void OnInteract();

    void OnPickUp(Transform holdPoint) {
        
    }

    void OnDrop() {
        
    }
}
