using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPatient : Action
{
    public override bool PrePerform()
    {
        target = World.Instance.RemovePatient();
        if (target == null)
        {
            return false;
        }
        return true;
    }

    public override bool PostPerform()
    {
        return true;
    }
}
