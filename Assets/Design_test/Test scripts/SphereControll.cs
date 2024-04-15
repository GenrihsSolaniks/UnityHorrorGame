using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SphereControll : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject Chupcabra;
    private GameObject Eva;
    private componentManager.shadowEnemyComponent shadowAiControl;
    void Start()
    {
        Chupcabra = this.gameObject;
        Eva = GameObject.Find("Eva");
        shadowAiControl = new componentManager.shadowEnemyComponent(Eva, Chupcabra.GetComponent<NavMeshAgent>(), 50f);
    }

    // Update is called once per frame
    void Update()
    {
        shadowAiControl.moveEnemy();
    }
}