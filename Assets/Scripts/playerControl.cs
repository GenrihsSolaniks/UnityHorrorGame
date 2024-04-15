using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private float speed=10f;
    [SerializeField] private float mouseSensitivity = 10f;
    private GameObject player;
    private GameObject camera;
    private componentManager.movementComponent playerMovementComponent;
    private componentManager.limitedMovementComponent cameraRotationComponent;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = GameObject.Find("Player");
        playerMovementComponent = new componentManager.movementComponent(player);
        camera = GameObject.Find("Main Camera");
        cameraRotationComponent = new componentManager.limitedMovementComponent(camera);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        playerMovementComponent.moveObject(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        //player.transform.Rotate(Vector3.up*mouseX * mouseSensitivity);
        playerMovementComponent.rotateObject(0,mouseX,0);
        cameraRotationComponent.rotateObject(-mouseY, 0, 0);

    }
}
