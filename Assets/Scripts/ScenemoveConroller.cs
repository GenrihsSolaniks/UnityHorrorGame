using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenemoveConroller : MonoBehaviour
{
    [SerializeField] private string sceneToGo;
    private Vector3 prevPosition;
    //[SerializeField] private Vector3 newPosition;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject thisZone;
    private bool allowTeleport = false;
    private IEnumerator scenePause;
    [SerializeField] private static int telepCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (telepCount > 0)
        {
            player.GetComponent<Transform>().position = thisZone.GetComponent<Transform>().position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.F) && allowTeleport == true)
        {
            SceneManager.LoadScene(sceneToGo);
            telepCount = 10;
        }
    }

    private void OnTriggerEnter(Collider TeleportationZone)
    {
        allowTeleport = true;
    }

    private void OnTriggerExit(Collider TeleportationZone)
    {
        allowTeleport = false;
    }
}