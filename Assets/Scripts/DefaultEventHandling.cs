using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DefaultEventHandling : MonoBehaviour
{
    public GameObject Event; // Reference to the parent GameObject
    private bool meshColliderEnabled = true;
    List<GameObject> hoverObjs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // Start processing from the Event GameObject's children
        ProcessChildren(Event.transform, 0); // Start with the top level (0)
    }

    // Method to recursively process children, grandchildren, and great-grandchildren
    void ProcessChildren(Transform parentTransform, int currentLevel)
    {
        // If we've processed 3 levels, stop further processing
        if (currentLevel > 3) return;

        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Transform child = parentTransform.GetChild(i);  // Get child Transform

            // Check if the current object (child, grandchild, etc.) is named "default"
            if (child.gameObject.name.ToLower() == "default")
            {
                // Add the required components to the object named "default"
                AddComponents(child.gameObject);
                
            }

            // Now, process the children of this object (grandchildren, etc.)
            ProcessChildren(child, currentLevel + 1);
        }
    }

    // Method to add components to a GameObject if they are not already added
    void AddComponents(GameObject targetObject)
    {
        // Only add components if they are not already added
        if (targetObject.GetComponent<hoverOBJ>() == null)
        {
            targetObject.AddComponent<MeshCollider>();
            targetObject.AddComponent<hoverOBJ>();
            targetObject.AddComponent<XRSimpleInteractable>();
            hoverObjs.Add(targetObject);
        }
    }

    
    public void ToggleBtnOBJHover()
    {
        meshColliderEnabled = !meshColliderEnabled; // Flip the state
        foreach (var item in hoverObjs)
        {
            MeshCollider meshCollider = item.GetComponent<MeshCollider>();

            if (meshCollider != null) // If a MeshCollider exists
            {
                meshCollider.enabled = meshColliderEnabled; // Enable/Disable MeshCollider based on the state
            }
        }
    }
}
