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

            // 충구 위치에서 탁착지점으로 가는 방향으로 Raycast 발사
            if (Physics.Raycast(cam.transform.position, rayDir, out RaycastHit hit, float.MaxValue, rayLayer))
            {
                // 총구 위치와 맞은 지점 사이에 Line 그리기
                Debug.DrawLine(cam.transform.position, hit.point, Color.red, 1f);

                GameObject bulletImpact = PoolManager.Pop(PoolType.BulletImpact);
                bulletImpact.transform.SetPositionAndRotation(hit.point, Quaternion.LookRotation(hit.normal));
            }
            // 만약 Raycast로 검출한게 없다면
            else
            {
                // 그냥 광선 발사
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
