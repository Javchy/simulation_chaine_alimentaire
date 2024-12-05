using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTCAMERATAG : MonoBehaviour

{
    public Transform target; // L'objet à suivre
    public float distance = 5f; // Distance entre la caméra et le target
    public float rotationSpeed = 100f; // Vitesse de la rotation

    private float angleX = 0f; // Angle horizontal
    private float angleY = 0f; // Angle vertical
    public float verticalClamp = 80f; // Angle vertical max pour limiter la rotation

    void Update()
    {
        // Vérifier si un objet est sélectionné
        if (target != null)
        {
            // Vérifier si le clic droit est maintenu
            if (Input.GetMouseButton(2))
            {
                // Récupérer les mouvements de la souris
                float horizontalInput = Input.GetAxis("Mouse X");
                float verticalInput = Input.GetAxis("Mouse Y");

                // Mise à jour des angles
                angleX += horizontalInput * rotationSpeed * Time.deltaTime;
                angleY -= verticalInput * rotationSpeed * Time.deltaTime;

                // Limiter l'angle vertical pour éviter les rotations excessives
                angleY = Mathf.Clamp(angleY, -verticalClamp, verticalClamp);
            }

            // Calculer la position de la caméra en fonction des angles X, Y
            Quaternion rotation = Quaternion.Euler(angleY, angleX, 0);
            Vector3 position = target.position - (rotation * Vector3.forward * distance);

            // Appliquer la position et la rotation de la caméra
            transform.position = position;
            transform.LookAt(target);
        }

        // Si le clic droit est effectué, sélectionner un nouvel objet
        if (Input.GetMouseButtonDown(1))  // Quand le bouton droit de la souris est cliqué
        {
            SelectTargetFromRaycast();
        }
    }

    // Fonction pour effectuer un raycast et sélectionner l'objet cliqué
    void SelectTargetFromRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Créer un raycast depuis la souris
        RaycastHit hit;

        // Si le raycast touche quelque chose
        if (Physics.Raycast(ray, out hit))
        {
            // Vérifier si l'objet a le tag spécifié
            if (hit.transform.CompareTag("Animals"))
            {
                // Assigner l'objet touché par le raycast comme target
                target = hit.transform;
            }
        }
    }
}

