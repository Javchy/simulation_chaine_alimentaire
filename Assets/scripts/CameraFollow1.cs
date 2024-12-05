using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target; // objet que la cam va suivre, que tu select
    public Vector3 offset;
    public float smoothSpeed = 0.125f; // tu peux modif c'est la speed de transition (logiquement)

    void LateUpdate()
    {
        if (target != null)
        {
            // Calcul de la position désirée
            Vector3 desiredPosition = target.position + offset;
            //interpolation pour mouvement plus fluide
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(target);

        }
    }
}
