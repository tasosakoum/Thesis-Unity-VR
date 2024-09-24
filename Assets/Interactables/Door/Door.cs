using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : SignalReceiver {
    [SerializeField] private bool isClosed;
    private bool isLocked;
    private bool previousClosedStatus;
    private bool previousLockedStatus;
    private const float DoorAnimationDuration = 0.5f;

    private Coroutine currentCoroutine = null;

    private void Start() {
        previousClosedStatus = isClosed;
        previousLockedStatus = isLocked;
    }

    void CloseDoor(bool lockDoor = false, bool unlockDoor = false) {
        if (currentCoroutine != null) return;
        
        if (unlockDoor) isLocked = false;
        if (isClosed || isLocked) return;
        
        currentCoroutine = StartCoroutine(AnimateDoorPosition(transform.position, new Vector3(transform.position.x, transform.position.y + transform.localScale.y, transform.position.z)));
        
        isClosed = true;
        isLocked = lockDoor;
    }

    void OpenDoor(bool lockDoor = false, bool unlockDoor = false) {
        if (currentCoroutine != null) return;
        
        if (unlockDoor) isLocked = false;
        if (isLocked || !isClosed) return; 
        
        currentCoroutine = StartCoroutine(AnimateDoorPosition(transform.position, new Vector3(transform.position.x, transform.position.y - transform.localScale.y, transform.position.z)));
        
        isClosed = false;
        isLocked = lockDoor;
    }
    
    IEnumerator AnimateDoorPosition(Vector3 startPosition, Vector3 endPosition) {
        float elapsed = 0;

        while (elapsed < DoorAnimationDuration) {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsed / DoorAnimationDuration);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = endPosition;

        currentCoroutine = null;
    }
    
    public override void OnSignalReceived(ObjectInteractStates nextState) {
        if (nextState != ObjectInteractStates.Reset && !isLocked) {
            previousClosedStatus = isClosed;
            previousLockedStatus = isLocked;
        }
        
        switch (nextState) {
            case ObjectInteractStates.Open:
                OpenDoor();
                break;
            case ObjectInteractStates.Close:
                CloseDoor();
                break;
            case ObjectInteractStates.SwitchState:
                if (isClosed) OpenDoor(); else CloseDoor();
                break;
            case ObjectInteractStates.OpenAndLock:
                OpenDoor(lockDoor: true);
                break;
            case ObjectInteractStates.CloseAndLock:
                CloseDoor(lockDoor: true);
                break;
            case ObjectInteractStates.OpenAndUnlock:
                OpenDoor(unlockDoor: true);
                break;
            case ObjectInteractStates.CloseAndUnlock:
                CloseDoor(unlockDoor: true);
                break;
            case ObjectInteractStates.Reset:
                if (previousClosedStatus)
                    CloseDoor(previousLockedStatus, !previousLockedStatus);
                else 
                    OpenDoor(previousLockedStatus, !previousLockedStatus);
                
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(nextState), nextState, null);
        }
    }
}
