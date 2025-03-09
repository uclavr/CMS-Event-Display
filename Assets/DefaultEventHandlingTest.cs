//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;

//public class DefaultEventHandlingTest : MonoBehaviour
//{

//    private bool meshColliderEnabled = true;
//    List<GameObject> hoverObjs = new List<GameObject>();
//    public GameObject allObjectsInScene;

//    private List<GameObject> AllChilds(GameObject root)
//    {
//        List<GameObject> result = new List<GameObject>();
//        if (root.transform.childCount > 0)
//        {
//            foreach (Transform VARIABLE in root.transform)
//            {
//                Searcher(result, VARIABLE.gameObject);
//            }
//        }
//        return result;
//    }

//    private void Searcher(List<GameObject> list, GameObject root)
//    {
//        list.Add(root);
//        if (root.transform.childCount > 0)
//        {
//            foreach (Transform VARIABLE in root.transform)
//            {
//                Searcher(list, VARIABLE.gameObject);
//            }
//        }
//    }

//    void Start()
//    {

//    }

//    void Awake()
//    {
//        List<GameObject> objects = AllChilds(allObjectsInScene);
//        foreach (var obj in objects)
//        {
//            print("obj name: " + obj.name);
//            if (obj.name.ToLower() == "default" || obj.name == "CubeBoi 1 (1)" || obj.name == "test_jets")
//            {
//                AddComponents(obj);
//            }
//        }

//    }
//    void AddComponents(GameObject targetObject)
//    {
//        if (targetObject.GetComponent<hoverOBJ>() == null)
//        {
//            targetObject.AddComponent<hoverOBJ>();
//        }
//        if (targetObject.GetComponent<MeshCollider>() == null)
//        {
//            MeshCollider collder = targetObject.AddComponent<MeshCollider>();
//            if (GetComponent<Collider>() != null)
//            {
//                GetComponent<Collider>().enabled = true;
//            }
//        }
//        if (targetObject.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>() == null)
//        {
//            targetObject.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
//        }
//    }


//    public void ToggleBtnOBJHover()
//    {
//        print("hover toggle");
//    }

//}


// NATHAN'S TESTING
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class DefaultEventHandlingTest : MonoBehaviour
{
    public GameObject Event; // Reference to the parent GameObject
    private bool meshColliderEnabled = true;
    List<GameObject> hoverObjs = new List<GameObject>();
    //private int temp = 0; //DEBUGGING

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
            if (child.gameObject.name.ToLower() == "default" || child.gameObject.name.ToLower() == "CubeBoi 1 (1)" || child.gameObject.name.ToLower() == "test_jets")
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
        // Only add components if they are not already added... andrew changed this to be a bit more specific just in case 
        if (targetObject.GetComponent<MeshCollider>() == null)
        {
            targetObject.AddComponent<MeshCollider>();
        }
        if (targetObject.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>() == null)
        {
            targetObject.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        }
        if (targetObject.GetComponent<hoverOBJ>() == null)
        {
            targetObject.AddComponent<hoverOBJ>();
            //// DEBUGGING
            //HeadsetDebuggerText(targetObject, temp);
            //temp += 1;
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
                print("bruh");
            }
        }

    }
}

//end of nathan's testing