using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BulletArrayData
{
    public List<BulletData> _bullets = new();
    public float _delayTime;
}
