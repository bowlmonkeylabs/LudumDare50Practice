using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace BML.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private LayerMask selectMask;

        private Vector2 mousePos;

        public void OnSelect()
        {
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, selectMask))
            {
                agent.SetDestination(hit.point);
            }
        }

        public void ReadMousePosition(InputAction.CallbackContext ctx)
        {
            mousePos = ctx.ReadValue<Vector2>();
        }
    }
}