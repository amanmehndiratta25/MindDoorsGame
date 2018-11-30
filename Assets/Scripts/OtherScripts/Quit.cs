using UnityEngine;
using System.Collections;

public class Quit : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Application.loadedLevel == 1)
            {
                Application.Quit();
            }
            
            else if(Application.loadedLevel == 2)
            {
                Application.LoadLevel(1);
            }
        }

    }
}