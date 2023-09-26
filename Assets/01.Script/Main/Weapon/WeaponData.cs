using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapon")]
public class WeaponData : ScriptableObject
{
    [Header("Info")]
    public string weaponName = "DefaultGun";

    [Header("Value")]
    [Range(0, 100)] public int magAmount = 30;
    [Range(5, 100)] public int damage = 10;
    [Tooltip("10M �տ��� �󸶸�ŭ�� �����ȿ� ź������ ���� �Ǵ���")][Range(0, 10)] public float actually = 1f;
    [Tooltip("�ѹ� �߻翡 ������ �Ѿ˼�")][Range(1, 20)] public int firePerBullet;
    [Tooltip("�Ÿ��� ������ �׷���")] public AnimationCurve distanceDamage;
    [Header("Delay")]
    [Range(0.1f, 1.0f)] public float attackDelay = 0.2f;
    [Range(0.6f, 3f)] public float reloadDelay = 1.0f;
    [Header("Sound")]
    public AudioClip[] attackClip;
    public AudioClip attackCancel;
    public AudioClip reloadClip;
}
