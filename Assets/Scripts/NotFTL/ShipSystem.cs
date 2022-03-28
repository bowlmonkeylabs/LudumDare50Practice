using System;
using BML.Scripts.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace BML.Scripts.NotFTL
{
    public class ShipSystem : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 3;
        [SerializeField] private ShipController shipController;
        [SerializeField] private LayerMask agentMask;
        [SerializeField] private Color healthyColor = Color.green;
        [SerializeField] private Color damagedColor = Color.yellow;
        [SerializeField] private Color destroyedColor = Color.red;
        public float CurrentHealth => currentHealth;
        public float MaxHealth => maxHealth;

        private float currentHealth;
        private MeshRenderer renderer;

        private void Awake()
        {
            renderer = GetComponentInChildren<MeshRenderer>();
        }

        private void Start()
        {
            currentHealth = maxHealth;
            UpdateColor();
        }

        public void TakeDamage()
        {
            if (shipController.TryTakeDamage())
            {
                currentHealth = Mathf.Max(0f, currentHealth - 1);
                UpdateColor();
            }
        }

        private void UpdateColor()
        {
            float percentMaxHealth = Mathf.InverseLerp(0f, maxHealth, currentHealth);
            Color newColor;
            
            if (percentMaxHealth >= .66f) 
                newColor = healthyColor;
            else if (percentMaxHealth >= .33f)
                newColor = damagedColor;
            else 
                newColor = destroyedColor;

            renderer.material.color = newColor;
        }
        
        private void OnTriggerStay(Collider other)
        {
            if (!other.gameObject.IsInLayerMask(agentMask)) return;

            CrewMember crewMember = other.gameObject.GetComponent<CrewMember>();
            if (crewMember == null) return;

            currentHealth = Mathf.Min(maxHealth, currentHealth + crewMember.RepairRate * Time.deltaTime);
            UpdateColor();
        }
    }
}