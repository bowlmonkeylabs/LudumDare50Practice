using UnityEngine;

namespace BML.Scripts
{
    public class Unit : MonoBehaviour
    {
        private int resourcesHeld;

        public void ReceiveResource()
        {
            resourcesHeld++;
        }

        public int DepositResources()
        {
            int resources = resourcesHeld;
            resourcesHeld = 0;
            return resources;
        }
    }
}