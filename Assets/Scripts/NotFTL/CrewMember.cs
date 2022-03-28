using UnityEngine;

namespace BML.Scripts.NotFTL
{
    public class CrewMember : MonoBehaviour
    {
        [SerializeField] private float repairRate = .25f;
        
        public float RepairRate => repairRate;
    }
}