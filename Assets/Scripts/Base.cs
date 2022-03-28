using System;
using BML.ScriptableObjectCore.Scripts.Variables;
using BML.Scripts.Utils;
using UnityEngine;

namespace BML.Scripts
{
    public class Base : MonoBehaviour
    {
        [SerializeField] private LayerMask agentMask;
        [SerializeField] private IntVariable resourceCount;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.IsInLayerMask(agentMask)) return;

            Unit unit = other.gameObject.GetComponent<Unit>();
            if (unit == null) return;

            resourceCount.Value += unit.DepositResources();
        }
    }
}