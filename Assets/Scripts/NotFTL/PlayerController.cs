using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace BML.Scripts.NotFTL
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private LayerMask selectMask;
        [SerializeField] private Transform weaponTargeter;

        private Vector2 mousePos;
        private bool weaponMode;

        public void OnSelect()
        {
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, selectMask))
            {
                if (!weaponMode)
                    agent.SetDestination(hit.point);

                else
                {
                    weaponTargeter.gameObject.SetActive(true);
                    weaponTargeter.position = hit.point;
                    weaponMode = false;
                }
                    
            }
        }

        public void ReadMousePosition(InputAction.CallbackContext ctx)
        {
            mousePos = ctx.ReadValue<Vector2>();
        }

        public void SwitchToWeapon()
        {
            weaponMode = true;
        }
    }
}