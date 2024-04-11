using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsPool : ObjectPool
{
    protected override void Initialize(GameObject prefab)
    {
        base.Initialize(prefab);
    }
}
