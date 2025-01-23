using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dummiesman;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class jetLoad : MonoBehaviour
{
    private string jetpath;
    private string jsonpath;
    public List<GameObject> jetObjects = new List<GameObject>();
    private GameObject parentObj;
    private JObject jetJson;
    private Material mat;
    private DirectoryInfo dir;

    void Awake() 
    {
        if (UnityEngine.Application.isEditor == false)
        {
            jetpath = @"/data/local/tmp/obj/jets";
            jsonpath = @"/data/local/tmp/obj/jetData.json";
        }
        else
        {
            jetpath = @"C:\Users\Joseph\Desktop\Event_1096322990\jets";
            jsonpath = @"C:\Users\Joseph\Desktop\Event_1096322990\jetData.json";
        }
        //jetObjects = new List<GameObject>();
        dir = new DirectoryInfo(jetpath);
        mat = Resources.Load<Material>("Jet Material");
        parentObj = new GameObject();

        using (StreamReader file = File.OpenText(jsonpath))
        using (JsonTextReader reader = new JsonTextReader(file))
        {
            jetJson = (JObject)JToken.ReadFrom(reader);
        }

        int index = 0;

        foreach (FileInfo file in dir.GetFiles())
        {
            GameObject obj = new OBJLoader().Load(file.FullName);
            obj.transform.parent = parentObj.transform;
            GameObject child = obj.transform.GetChild(0).gameObject;
            child.AddComponent<JetComponent>();
            child.GetComponent<MeshRenderer>().material = mat;
            jetObjects.Add(child);
        }
        
        foreach (var item in jetJson["jetData"][0])
        {
            jetObjects[index].GetComponent<JetComponent>().id = item["id"].Value<int>();
            jetObjects[index].GetComponent<JetComponent>().et = item["et"].Value<double>();
            jetObjects[index].GetComponent<JetComponent>().eta = item["eta"].Value<double>();
            jetObjects[index].GetComponent<JetComponent>().theta = item["theta"].Value<double>();
            jetObjects[index].GetComponent<JetComponent>().phi = item["phi"].Value<double>();
            index++;
        }
    }

    void Start()
    {

    }
}
