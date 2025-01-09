using System;
using UnityEngine;
using UnityEngine.AI;

public class yacks_mouvs : MonoBehaviour
{    
    public float moveSpeed = 2.0f;
    public NavMeshAgent agent;
    private GameObject plant = null;

    private void OnTriggerEnter(Collider other) // ajt on trigger stay
    {
        if (other.gameObject.tag == "plant" && plant == null)
        {
           plant = other.gameObject;
           agent.destination = plant.transform.position;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "plant")
        {
            Destroy(collision.gameObject);
        }
    }
}
