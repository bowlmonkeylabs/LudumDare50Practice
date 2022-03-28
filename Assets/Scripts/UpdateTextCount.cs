using System;
using BML.ScriptableObjectCore.Scripts.Variables;
using TMPro;
using UnityEngine;

namespace BML.Scripts
{
    public class UpdateTextCount : MonoBehaviour
    {
        [SerializeField] private IntVariable resourceCount;
        [SerializeField] private TMP_Text text;

        private void Awake()
        {
            resourceCount.Subscribe(UpdateText);
        }

        private void UpdateText()
        {
            text.text = "Resources: " + resourceCount.Value;
        }
    }
}