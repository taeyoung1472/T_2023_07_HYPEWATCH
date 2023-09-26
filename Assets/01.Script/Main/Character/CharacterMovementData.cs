using UnityEngine;

[CreateAssetMenu(menuName = "Data/Character")]
public class CharacterData : ScriptableObject
{
    [Header("Move")]
    [Range(1.0f, 10.0f)] public float speed = 5f;
    [Range(1.0f, 5.0f)] public float jumpForce = 2.0f;

    [Header("HeadBobbing")]
    [Range(0, 0.1f)] public float amplitude;
    [Range(0, 30)] public float frequency;

    [Header("Sound")]
    public AudioClip footStepClip;
}
