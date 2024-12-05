
using UnityEngine;

public class CameraFollowRotation : MonoBehaviour
{
    public Transform target; // objet qui est target
    public float distance = 5f; //range entre le target et la cam ( je crois )
    public float rotationSpeed = 100f; // vitesse de la rotation

    private float angleX = 0f; // Angle horizontal SmeH
    private float angleY = 0f; // Angle Vertical bah oe logique
    public float verticalClamp = 80f; // Angle vertical max pcq azy sinon il part trop loin

    void Update()
    {
        if (target != null)
        {
            //Verif si clique droit est maitenu
            if (Input.GetMouseButton(1))
            {
                // j'crois on récupère les mouvement de la souris
                float horizontalInput = Input.GetAxis("Mouse X");
                float verticalInput = Input.GetAxis("Mouse Y");

                // MAJ Angle
                angleX += horizontalInput * rotationSpeed * Time.deltaTime;
                angleY -= verticalInput * rotationSpeed * Time.deltaTime;

                // Limiter l'angle Y ( vertical pour éviter 360 )
                angleY = Mathf.Clamp(angleY, -verticalClamp, verticalClamp);
            }

            // Calcul la position de la cam en fonction des angles X,Y
            Quaternion rotation = Quaternion.Euler(angleY, angleX, 0);
            Vector3 position = target.position - (rotation * Vector3.forward * distance);

            //Appliquer la position et la rotation bro
            transform.position = position;
            transform.LookAt(target);

        }
    }
}
