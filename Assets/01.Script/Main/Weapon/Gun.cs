using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gun : Weapon
{
    protected int curMagAmount;
    protected int CurMagAmount
    {
        get
        {
            return curMagAmount;
        }
        set
        {
            curMagAmount = value;
            UIManager.Instance.SetBulletUI(curMagAmount, weaponData.magAmount);
        }
    }

    public override void Init()
    {
        CurMagAmount = weaponData.magAmount;
    }

    public override IEnumerator AttackCor(Action callBack, int attackFlag)
    {
        if (CurMagAmount <= 0)
        {
            yield return new WaitForSeconds(weaponData.attackDelay * 0.25f);
            callBack?.Invoke();
            yield break;
        }
        else
        {
            ShootRay();
            AudioManager.PlayAudio(weaponData.attackClip[attackFlag]);
            CurMagAmount--;
        }

        Debug.Log($"Attack Type:{attackFlag}");
        yield return new WaitForSeconds(weaponData.attackDelay);
        callBack?.Invoke();
    }

    protected virtual void ShootRay()
    {
        for (int i = 0; i < weaponData.firePerBullet; i++)
        {
            Vector3 rayDir = cam.transform.forward * 10 + cam.transform.TransformDirection(Random.insideUnitCircle * weaponData.actually * 0.5f);

            // �汸 ��ġ���� Ź���������� ���� �������� Raycast �߻�
            if (Physics.Raycast(cam.transform.position, rayDir, out RaycastHit hit, float.MaxValue, rayLayer))
            {
                // �ѱ� ��ġ�� ���� ���� ���̿� Line �׸���
                Debug.DrawLine(cam.transform.position, hit.point, Color.red, 1f);

                GameObject bulletImpact = PoolManager.Pop(PoolType.BulletImpact);
                bulletImpact.transform.SetPositionAndRotation(hit.point, Quaternion.LookRotation(hit.normal));
            }
            // ���� Raycast�� �����Ѱ� ���ٸ�
            else
            {
                // �׳� ���� �߻�
                Debug.DrawRay(cam.transform.position, rayDir, Color.black, 1f);
            }
        }
    }

    public override IEnumerator ReloadCor(Action callBack)
    {
        Debug.Log($"Reload");
        yield return new WaitForSeconds(weaponData.reloadDelay);
        CurMagAmount = weaponData.magAmount;
        callBack?.Invoke();
    }
}
