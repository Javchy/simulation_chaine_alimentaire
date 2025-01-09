using UnityEngine;
using UnityEngine.AI;


[AddComponentMenu("NavMesh - Player")]
public class NavMesh_Player : MonoBehaviour
{
    [Tooltip("The agent used to move this entity.")]
    public NavMeshAgent agent;

    [Tooltip("The physics layer used for the floor, so we can move the entity at a valid position when clicking on it.")]
    // The default value ~0 is like selecting "Everything" in the list.
    public LayerMask floorLayer = ~1;
    private void Awake()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }
    private void OnValidate()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }
    private void Update()
    {
        // If a mouse button is pressed
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            // Calculate a ray from the mouse position toward the forward vector of the camera
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // If we hit something when the ray is cast (which should be the floor)
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, floorLayer))
            {
                // Move this agent to the clicked position
                agent.SetDestination(hitInfo.point);
            }
        }
    }

}
