using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BulletArrayData
{
    public List<BulletData> Bullets = new();
    public float DelayTime;
}
