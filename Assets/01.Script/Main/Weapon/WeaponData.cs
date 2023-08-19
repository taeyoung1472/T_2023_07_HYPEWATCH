using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapon")]
public class WeaponData : ScriptableObject
{
    [Header("Info")]
    public string weaponName = "DefaultGun";

    [Header("Value")]
    [Range(0, 100)] public int magAmount = 30;
    [Range(5, 100)] public int damage = 10;
    [Tooltip("10M 앞에서 얼마만큼의 범위안에 탄착군이 생성 되는지")][Range(0, 10)] public float actually = 1f;
    [Tooltip("한번 발사에 나오는 총알수")][Range(1, 20)] public int firePerBullet;
    [Tooltip("거리별 데미지 그래프")] public AnimationCurve distanceDamage;
    [Header("Delay")]
    [Range(0.1f, 1.0f)] public float attackDelay = 0.2f;
    [Range(0.6f, 3f)] public float reloadDelay = 1.0f;
    [Header("Sound")]
    public AudioClip[] attackClip;
    public AudioClip attackCancel;
    public AudioClip reloadClip;
}
