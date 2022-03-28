using System;
using BML.ScriptableObjectCore.Scripts.Variables;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace BML.Scripts.ResourceGather
{
    public class UpdateTextCount : MonoBehaviour
    {
        [SerializeField] private bool useFloat;
        [SerializeField] [HideIf("useFloat")] private IntVariable resourceCount;
        [SerializeField] [ShowIf("useFloat")] private FloatVariable resourceCountFloat;
        [SerializeField] private string prefix = "Resources: ";
        [SerializeField] private TMP_Text text;

        private void Awake()
        {
            if (!useFloat) resourceCount.Subscribe(UpdateText);
            else resourceCountFloat.Subscribe(UpdateText);
        }

        private void UpdateText()
        {
            float count = useFloat ? resourceCountFloat.Value : resourceCount.Value;
            text.text = prefix + count.ToString("F1");
        }
    }
}