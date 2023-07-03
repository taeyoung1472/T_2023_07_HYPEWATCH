using UnityEngine;

[CreateAssetMenu(menuName = "Data/Character")]
public class CharacterData : ScriptableObject
{
    [Range(1.0f, 10.0f)] public float speed = 5f;
    [Range(1.0f, 5.0f)] public float jumpForce = 2.0f;
}
