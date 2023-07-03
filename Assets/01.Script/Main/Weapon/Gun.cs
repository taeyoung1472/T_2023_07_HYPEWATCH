using System;
using System.Collections;
using UnityEngine;

public class Gun : Weapon
{
    protected int curMagAmount;

    public override void Init()
    {
        curMagAmount = weaponData.magAmount;
    }

    public override IEnumerator AttackCor(Action callBack, int attackFlag)
    {
        if (curMagAmount <= 0)
        {
            yield return new WaitForSeconds(weaponData.attackDelay * 0.25f);
            callBack?.Invoke();
            yield break;
        }
        else
        {
            curMagAmount--;
        }

        Debug.Log($"Attack Type:{attackFlag}");
        yield return new WaitForSeconds(weaponData.attackDelay);
        callBack?.Invoke();
    }

    public override IEnumerator ReloadCor(Action callBack)
    {
        Debug.Log($"Reload");
        yield return new WaitForSeconds(weaponData.reloadDelay);
        curMagAmount = weaponData.magAmount;
        callBack?.Invoke();
    }
}
