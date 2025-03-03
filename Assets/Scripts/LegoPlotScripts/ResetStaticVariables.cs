using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetStaticVariables : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UnityEngine.Debug.Log("Scene Loaded: " + scene.name);
        ResetStatics(); // Reset flags whenever the scene changes
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe from scene loaded event to avoid memory leaks
    }

    private void ResetStatics() // Method to reset flag1 and flag2
    {
        legoEnergyDisplay.flag1 = 0;
        legoEnergyDisplay.flag2 = 0;
        legoEnergyDisplay.jsonFile = null;
        legoEnergyDisplay.jsonText = null;
        legoEnergyDisplay.cubeLego = null;
        legoEnergyDisplay.toggleGroup = null;
        legoEnergyDisplay.jetToggle = null;

        UnityEngine.Debug.Log("Static variables reset.");
    }
}
