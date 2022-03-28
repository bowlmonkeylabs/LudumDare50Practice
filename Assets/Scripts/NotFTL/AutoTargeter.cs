using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BML.Scripts.NotFTL
{
    public class AutoTargeter : MonoBehaviour
    {
        [SerializeField] private int switchDelayMin = 5;
        [SerializeField] private int switchDelayMax = 20;
        [SerializeField] private Transform aimTargeter;
        [SerializeField] private List<Transform> targetList;

        private float lastSwitchTime;
        private float currentDelay;

        private void Awake()
        {
            lastSwitchTime = Mathf.NegativeInfinity;
            currentDelay = Random.Range(switchDelayMax, switchDelayMax);
        }

        private void Update()
        {
            if (lastSwitchTime + currentDelay < Time.time)
            {
                aimTargeter.position = RandomFromList().position;
                lastSwitchTime = Time.time;
                currentDelay = Random.Range(switchDelayMax, switchDelayMax);
                aimTargeter.gameObject.SetActive(true);
            }
        }

        private Transform RandomFromList()
        {
            int index = Random.Range(0, targetList.Count);
            return targetList[index];
        }
    }
}