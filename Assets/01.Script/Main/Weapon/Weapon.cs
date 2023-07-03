using Shapes;
using System;
using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponData weaponData;
    protected WeaponState curState;
    public WeaponState CurState { get { return curState; } }

    protected void Awake()
    {
        Init();
    }
    public abstract void Init();
    public void Attack(Action callBack, int attackFlag)
    {
        StartCoroutine(AttackCor(callBack, attackFlag));
    }
    public abstract IEnumerator AttackCor(Action callBack, int attackFlag);
    public void Reload(Action callBack)
    {
        StartCoroutine(ReloadCor(callBack));
    }
    public abstract IEnumerator ReloadCor(Action callBack);
}

public enum WeaponState
{
    Idle,
    Attack,
    Reload,
}