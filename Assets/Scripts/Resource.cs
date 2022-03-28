using System;
using BML.Scripts.Utils;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace BML.Scripts
{
    public class Resource : MonoBehaviour
    {
        [SerializeField] private int resourceCount = 10;
        [SerializeField] private float gatherRate = 1;
        [SerializeField] private LayerMask agentMask;

        private float remainingResources;
        private float lastGatherTime;

        private void Start()
        {
            remainingResources = resourceCount;
            lastGatherTime = Mathf.NegativeInfinity;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.gameObject.IsInLayerMask(agentMask)) return;

            Unit unit = other.gameObject.GetComponent<Unit>();
            if (unit == null) return;

            if (lastGatherTime + 1f / gatherRate < Time.time)
            {
                lastGatherTime = Time.time;
                remainingResources--;
                unit.ReceiveResource();
                if (remainingResources == 0)
                    Destroy(this.gameObject);
            }
        }
    }
}