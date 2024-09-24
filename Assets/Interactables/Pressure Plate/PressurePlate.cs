using UnityEngine;

public class PressurePlate : SignalSender {
    [SerializeField] private SerializableDictionary<GameObject, ObjectInteractStates> onStoppedBeingPressed;
    [SerializeField] private Material pressurePlateOffMaterial;
    [SerializeField] private Material pressurePlateOnMaterial;

    private int numberOfCollisions = 0;

    private void Start() {
        var startingMaterial = numberOfCollisions > 0 ? pressurePlateOnMaterial : pressurePlateOffMaterial;

        GetComponent<MeshRenderer>().material = startingMaterial;
    }
    
    private void OnTriggerEnter(Collider other) {
        if (numberOfCollisions <= 0) {
            SendSignals();
            GetComponent<MeshRenderer>().material = pressurePlateOnMaterial;
        }
        
        numberOfCollisions++;
    }

    private void OnTriggerExit(Collider other) {
        numberOfCollisions--;

        if (numberOfCollisions <= 0) {
            SendSignals(onStoppedBeingPressed);
            GetComponent<MeshRenderer>().material = pressurePlateOffMaterial;
        }
    }

}
