using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostObject : SignalSender {
    [SerializeField] private GameObject expectedObject;
    private bool canSendSignals = true;
    
    private void Start() {
        var renderer = GetComponent<Renderer>();
        var newMat = new Material(renderer.material);

        newMat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        newMat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        newMat.SetInt("_ZWrite", 0);
        newMat.DisableKeyword("_ALPHATEST_ON");
        newMat.EnableKeyword("_ALPHABLEND_ON");
        newMat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        newMat.renderQueue = (int) UnityEngine.Rendering.RenderQueue.Transparent;

        var color = newMat.color;
        color.a = 0.5f;
        newMat.color = color;

        renderer.material = newMat; 
    }
    
    private void OnTriggerStay(Collider other) {
        var objScript = other.GetComponent<MovableObject>();
        if (!other.gameObject.Equals(expectedObject)) return;
        
        other.transform.position = transform.position;
        other.transform.rotation = transform.rotation;

        HandleSignals();
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.Equals(expectedObject))
            canSendSignals = true;
    }

    private void HandleSignals() {
        if (!canSendSignals) return;

        SendSignals();
        canSendSignals = false;
    }
}