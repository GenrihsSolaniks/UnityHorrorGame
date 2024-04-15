using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhoneControl : MonoBehaviour
{
    [SerializeField] private GameObject Phone;
    [SerializeField] private AudioSource message;
    [SerializeField] private int messageCounter = 1;
    [SerializeField] private AudioClip message1;
    [SerializeField] private AudioClip message2;
    [SerializeField] private AudioClip message3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && messageCounter > 1)
        {
            messageCounter = messageCounter - 1;
            Phone.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.G) && messageCounter < 3)
        {
            messageCounter = messageCounter + 1;
            Phone.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey("space"))
        {
            Phone.SetActive(true);
            switch (messageCounter)
            {
                case 1:
                    message.clip = message1;
                    message.PlayDelayed(1);
                    break;
                case 2:
                    message.clip = message2;
                    message.PlayDelayed(1);
                    break;
                case 3:
                    message.clip = message3;
                    message.PlayDelayed(1);
                    break;
            }
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Phone.SetActive(false);
        }
    }
}
