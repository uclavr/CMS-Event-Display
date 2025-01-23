using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dataPathTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!File.Exists("/data/local/tmp/tracks.obj"))
        {
            SceneManager.LoadScene("2E2M");
        }
        else
        {
            transform.position = new Vector3(0.0f,1000.0f,0.0f );
        }
    }
}
