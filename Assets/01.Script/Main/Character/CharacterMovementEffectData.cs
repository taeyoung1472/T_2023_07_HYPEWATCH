using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/CharacterMovementEffect")]
public class CharacterMovementEffectData : ScriptableObject
{
    [Header("Main Value")]
    public string effectName = "EffectName";
    public Sprite effectIcon;
    [Range(0.1f, 10f)] public float effectDuration = 0.5f;

    [Header("Effect Value")]
    [Range(0.1f, 1f)] public float speedMultiflyEffect = 1f;
    [Range(0.1f, 1f)] public float jumpMultiflyEffect = 1f;
    [Range(0.1f, 1f)] public float senservityMultiflyEffect = 1f;
}