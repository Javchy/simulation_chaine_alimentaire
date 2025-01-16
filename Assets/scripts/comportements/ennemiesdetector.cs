using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ennemiesdetector : MonoBehaviour
{

   
    public float speed = 8f;
    public void OnTriggerEnter(Collider other) 
    {
       

       
        
            Enemies EnemiesComponent = other.GetComponent<Enemies>();  
            if (EnemiesComponent != null)
            {
                Vector3 EnemiesDirection = EnemiesComponent.transform.position - transform.position;
                Vector3 oppositeDirection = -EnemiesDirection.normalized;
                transform.position += oppositeDirection * speed * Time.deltaTime;
            }
        
    }
}
