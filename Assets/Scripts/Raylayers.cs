using System.Collections;
using UnityEngine;

public static class Raylayers
{

    public static readonly int onlyCollisions;
    public static readonly int upRay;
    public static readonly int downRay;

    static Raylayers()
    {
        onlyCollisions = 1 << LayerMask.NameToLayer("Collisions")
            | 1 << LayerMask.NameToLayer("SoftTop")
            | 1 << LayerMask.NameToLayer("SoftBottom");

        upRay = 1 << LayerMask.NameToLayer("Collisions")
            | 1 << LayerMask.NameToLayer("SoftTop");

        downRay = 1 << LayerMask.NameToLayer("Collisions")
            | 1 << LayerMask.NameToLayer("SoftBottom");
    }
	
}