using System.Collections;
using System.Collections.Generic;
using BML.ScriptableObjectCore.Scripts.SceneReferences;
using BML.ScriptableObjectCore.Scripts.Variables;
using Sirenix.OdinInspector;
using UnityEngine;
namespace BML.VisualStateMachine.Scripts.Nodes.TransitionConditions
{
    [System.Serializable]
    [HideReferenceObjectPicker]
    public class TransformCondition : ITransitionCondition
    {
        [SerializeField] [HideLabel] [HideReferenceObjectPicker] 
        private TransformSceneReference targetParameter;
    
        [LabelWidth(40f)] [SerializeField] [HideReferenceObjectPicker]
        private bool isNull;

        private string parentTransitionName = "";

        public void Init(string transitionName)
        {
            parentTransitionName = transitionName;
        }

        public bool Evaluate(List<TriggerVariable> receivedTriggers)
        {
            return (targetParameter.Value == null) == isNull;
        }
    
        public override string ToString()
        {
            if (targetParameter != null)
                return isNull ? $"{targetParameter.name} == null" : $"{targetParameter.name} != null";
            else
                return "<Missing TransformSceneReference>";
        }
    }

}
