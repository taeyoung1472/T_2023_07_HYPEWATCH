using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageAble
{
    void Damage(float dmg, Vector3 dmgDir = default(Vector3), float force = 0);
}
