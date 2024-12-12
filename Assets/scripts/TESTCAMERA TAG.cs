using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTCAMERATAG : MonoBehaviour

{
    public Transform target; // L'objet � suivre
    public float distance = 5f; // Distance entre la cam�ra et le target
    public float rotationSpeed = 100f; // Vitesse de la rotation

    private float angleX = 0f; // Angle horizontal
    private float angleY = 0f; // Angle vertical
    public float verticalClamp = 80f; // Angle vertical max pour limiter la rotation

    void Update()
    {
        // V�rifier si un objet est s�lectionn�
        if (target != null)
        {
            // V�rifier si le clic droit est maintenu
            if (Input.GetMouseButton(2))
            {
                // R�cup�rer les mouvements de la souris
                float horizontalInput = Input.GetAxis("Mouse X");
                float verticalInput = Input.GetAxis("Mouse Y");

                // Mise � jour des angles
                angleX += horizontalInput * rotationSpeed * Time.deltaTime;
                angleY -= verticalInput * rotationSpeed * Time.deltaTime;

                // Limiter l'angle vertical pour �viter les rotations excessives
                angleY = Mathf.Clamp(angleY, -verticalClamp, verticalClamp);
            }

            // Calculer la position de la cam�ra en fonction des angles X, Y
            Quaternion rotation = Quaternion.Euler(angleY, angleX, 0);
            Vector3 position = target.position - (rotation * Vector3.forward * distance);

            // Appliquer la position et la rotation de la cam�ra
            transform.position = position;
            transform.LookAt(target);
        }

        // Si le clic droit est effectu�, s�lectionner un nouvel objet
        if (Input.GetMouseButtonDown(1))  // Quand le bouton droit de la souris est cliqu�
        {
            SelectTargetFromRaycast();
        }
    }

    // Fonction pour effectuer un raycast et s�lectionner l'objet cliqu�
    void SelectTargetFromRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Cr�er un raycast depuis la souris
        RaycastHit hit;

        // Si le raycast touche quelque chose
        if (Physics.Raycast(ray, out hit))
        {
            // V�rifier si l'objet a le tag sp�cifi�
            if (hit.transform.CompareTag("Animals"))
            {
                // Assigner l'objet touch� par le raycast comme target
                target = hit.transform;
            }
        }
    }
}

