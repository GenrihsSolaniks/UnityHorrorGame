using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class PickingKeys : MonoBehaviour
{
    string message = "Click tab to pick keys";
    bool displayMessage = false;
    void Start()
    {
        name = this.gameObject.name;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            displayMessage = true;
        }

    }
    private void OnTriggerStay(Collider other)
    { 
        if (other.gameObject.name == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Debug.Log(this.gameObject.name);
                Destroy(this.gameObject);

            }
        }
    }
    void OnGUI()
    {
        if (displayMessage)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 100), message); 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            displayMessage = false;
           
        }
    }
}
