using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(OAICharacter))]
public abstract class OAIBehavior : MonoBehaviour
{
    public abstract string GetAsText();
}
