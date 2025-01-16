using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerReproduction : MonoBehaviour
{
    public class SimplePartnerReproduction : MonoBehaviour
    {
        public float searchRadius = 5f; // Rayon de recherche pour trouver le partanaire pour ken
        public GameObject prefab; // Préfab qui ken
        public float reproductionCooldown = 5f; // Temp apres ken
        public float desireIncreaseRate = 8; // Vitesse de la jauge d'envie (par seconde)
        public float maxDesire = 100f; // Niveau maximum de la jauge
        public float minReproductionDesire = 40f; // Niveau minimum requis pour ken un max

        private float currentDesire = 0f; // Niveau actuel de la jauge 
        private bool canReproduce = true; // Indique si l'objet est prêt à chercher un pelo pour ken

        void Update()
        {
            IncreaseDesire(); // Augmente la jauge d'envie petit a petit

            if (canReproduce && currentDesire >= minReproductionDesire)
            {
                SearchAndReproduce();
            }
        }

        void IncreaseDesire()
        {
            // Augmente la jauge d'envie jusque son max
            if (currentDesire < maxDesire)
            {
                currentDesire += desireIncreaseRate * Time.deltaTime;
            }
        }

        void SearchAndReproduce()
        {
            // Rechercher les objets proches
            Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);

            foreach (Collider collider in colliders)
            {
                // Ignorer soi meme et vérifier si l'objet est valide
                if (collider.gameObject != gameObject && collider.CompareTag(gameObject.tag))
                {
                    // Vérifier si l'autre objet aussi peut ken
                    SimplePartnerReproduction partner = collider.GetComponent<SimplePartnerReproduction>();
                    if (partner != null && partner.IsReadyToReproduce())
                    {
                        // lancer le ken un max
                        Reproduce(partner);
                        break;
                    }
                }
            }
        }

        public bool IsReadyToReproduce()
        {
            // Vérifie si la jauge d'envie est entre 40 et 100
            return canReproduce && currentDesire >= minReproductionDesire && currentDesire <= maxDesire;
        }

        void Reproduce(SimplePartnerReproduction partner)
        {
            // Desactiver la reproduction pour les deux pour pas qu'ils fassent de plan a 15
            canReproduce = false;
            partner.canReproduce = false;

            // jauge d'envie a zéro pour les deux
            currentDesire = 0f;
            partner.currentDesire = 0f;

            // Creer un nouvel objet entre les deux partenaires qui a le meme prefab ( j'suis pas sur en vrais)
            Vector3 spawnPosition = (transform.position + partner.transform.position) / 2;
            Instantiate(prefab, spawnPosition, Quaternion.identity);

            // ils peuvent re ken apres un certain temp
            Invoke(nameof(ResetReproduction), reproductionCooldown);
            partner.Invoke(nameof(ResetReproduction), reproductionCooldown);
        }

        void ResetReproduction()
        {
            canReproduce = true;
        }

        private void OnDrawGizmosSelected()
        {
            // déssiner le radius en editeur ( sinon on voit pas )
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, searchRadius);
        }

        private void OnGUI()
        {
            // Afficher une barre de progression pour la jauge d'envie au-dessus de l'objet
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            screenPosition.y = Screen.height - screenPosition.y; // Inverser l'axe Y pour correspondre à l'écran
            float barWidth = 50f;
            float barHeight = 5f;
            float filledWidth = (currentDesire / maxDesire) * barWidth;

            // Dessiner une barre de fond (en gris)
            GUI.color = Color.gray;
            GUI.DrawTexture(new Rect(screenPosition.x - barWidth / 2, screenPosition.y - 20, barWidth, barHeight), Texture2D.whiteTexture);

            // Dessiner la barre remplie (en vert)
            GUI.color = Color.red;
            GUI.DrawTexture(new Rect(screenPosition.x - barWidth / 2, screenPosition.y - 20, filledWidth, barHeight), Texture2D.whiteTexture);
        }
    }
}
