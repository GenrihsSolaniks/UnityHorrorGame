using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class shadowControl : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject shadow;
    private GameObject player;
    private componentManager.shadowEnemyComponent shadowAiControl;
    void Start()
    {
        shadow = this.gameObject;
        player = GameObject.Find("Player");
        shadowAiControl = new componentManager.shadowEnemyComponent(player, shadow.GetComponent<NavMeshAgent>(),50f);
    }

    // Update is called once per frame
    void Update()
    {
        shadowAiControl.moveEnemy();
    }
}
