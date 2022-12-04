using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OpenAI Behavior", menuName = "ScriptableObjects/OpenAI Behavior", order = 1)]
public class BehaviorSO : ScriptableObject
{
    [SerializeField, TextArea(5, 20)]
    public string Description;

}
