using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleOAIBehavior : OAIBehavior
{
    [TextArea(5, 20)]
    public string Description;

    public override string GetAsText()
    {
        return Description;
    }
}
