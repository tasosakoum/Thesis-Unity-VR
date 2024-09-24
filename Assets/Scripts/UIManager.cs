using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI infoText;
    private float deltaTime = 0.0f;

    private void Update() {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        var fps = 1.0f / deltaTime;

        infoText.text = $"Barrels Spawned: {Spawner.itemsSpawned}\nFPS: {Mathf.Ceil(fps)}";
    }
}
