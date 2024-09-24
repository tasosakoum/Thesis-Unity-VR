using UnityEngine;

public class MovableObject: MonoBehaviour, Interactable {
    public bool isMoving;
    private Transform originalParent;

    public void OnInteract() {
        Debug.Log("Interacted");
    }

    public void OnPickUp(Transform holdPoint) {
        isMoving = true;
        originalParent = transform.parent;
        transform.SetParent(holdPoint); 
        transform.localPosition = Vector3.zero;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void OnDrop() {
        isMoving = false;
        transform.SetParent(originalParent);
        GetComponent<Rigidbody>().isKinematic = false;
    }
}