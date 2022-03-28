using System;
using BML.ScriptableObjectCore.Scripts.Variables;
using UnityEngine;

namespace BML.Scripts.NotFTL
{
    public class ShipController : MonoBehaviour
    {
        [SerializeField] private IntVariable currentHullHealth;
        [SerializeField] private FloatVariable currentShieldHealth;
        [SerializeField] private int maxHullHealth = 30;
        [SerializeField] private float shieldRechargeRate = .15f;
        [SerializeField] private float weaponDelay = 4f;
        [SerializeField] private Transform weaponTargeter;
        [SerializeField] private GameObject shieldObj;
        [SerializeField] private GameObject hitParticles;
        [SerializeField] private ShipSystem pilotSystem;
        [SerializeField] private ShipSystem weaponsSystem;
        [SerializeField] private ShipSystem shieldSystem;
        [SerializeField] private ShipSystem medicalSystem;
        [SerializeField] private ShipSystem engineSystem;
        [SerializeField] private ShipSystem oxygenSystem;
        [SerializeField] private LayerMask systemMask;

        private int maxShieldHealth;
        private float lastFireTime;

        private void Start()
        {
            lastFireTime = Mathf.NegativeInfinity;
            currentHullHealth.Value = maxHullHealth;
            currentShieldHealth.Value = shieldSystem.MaxHealth;
        }

        private void Update()
        {
            maxShieldHealth = Mathf.FloorToInt(shieldSystem.CurrentHealth);
            currentShieldHealth.Value = Mathf.Min(currentShieldHealth.Value, maxShieldHealth);

            currentShieldHealth.Value += shieldRechargeRate * Time.deltaTime;
            if (currentShieldHealth.Value >= 1f) 
                shieldObj.SetActive(true);
            else
                shieldObj.SetActive(false);

            if (lastFireTime + weaponDelay < Time.time)
                TryFireWeapon();
        }

        private void TryFireWeapon()
        {
            if (!weaponTargeter.gameObject.activeSelf) return;
            if (weaponsSystem.CurrentHealth <= 0f) return;

            GameObject tempParticles = GameObject.Instantiate(hitParticles, weaponTargeter.position, Quaternion.identity);
            LeanTween.value(0f, 1f, 5f).setOnComplete(_ => Destroy(tempParticles));

            Collider[] hitColliders = new Collider[10];
            int numColliders = Physics.OverlapSphereNonAlloc(weaponTargeter.position, 5f, hitColliders, systemMask);

            for (int i = 0; i < numColliders; i++)
            {
                ShipSystem system = hitColliders[i].GetComponent<ShipSystem>();
                if (system != null)
                {
                    system.TakeDamage();
                    Debug.Log($"Hit {system.gameObject.name}!");
                }
                    
            }
            
            lastFireTime = Time.time;
        }

        public bool TryTakeDamage()
        {
            if (currentShieldHealth.Value >= 1)
            {
                currentShieldHealth.Value--;
                return false;
            }
            
            currentHullHealth.Value--;
            return true;
        }
    }
}