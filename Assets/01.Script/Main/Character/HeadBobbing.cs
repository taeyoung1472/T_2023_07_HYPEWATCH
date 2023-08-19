using System;
using UnityEngine;

public class HeadBobbing : MonoBehaviour
{
    public bool Enable { get { return enable; } set { enable = value; } }
    private bool enable;

    private Transform cam;
    private Transform camHolder;
    private float toggleSpeed = 3.0f;
    private Vector3 startPos;
    bool isHeadRight;

    private Character myCharacter;
    private CharacterController controller;
    private CharacterData movementData;

    public void Init(Character character, Transform cam, Transform camHolder)
    {
        myCharacter = character;
        movementData = character.MovementData;
        controller = character.GetComponent<CharacterController>();
        this.cam = cam;
        this.camHolder = camHolder;
        startPos = cam.localPosition;
    }

    private void Update()
    {
        if (!enable) return;

        CheckMotion();
        ResetPosition();
        cam.LookAt(FocusTarget());
    }

    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * movementData.frequency) * movementData.amplitude;
        pos.x += Mathf.Cos(Time.time * movementData.frequency / 2) * movementData.amplitude * 2;

        if (isHeadRight && Mathf.Sin(Time.time * movementData.frequency / 2) > 0.9f)
        {
            isHeadRight = false;
            AudioManager.PlayAudioRandPitch(movementData.footStepClip);
        }
        if (!isHeadRight && Mathf.Sin(Time.time * movementData.frequency / 2) < -0.9f)
        {
            isHeadRight = true;
            AudioManager.PlayAudioRandPitch(movementData.footStepClip);
        }

        return pos;
    }

    private void CheckMotion()
    {
        float speed = new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude;

        if (speed < toggleSpeed) return;
        if (!controller.isGrounded) return;

        PlayMotion(FootStepMotion());
    }

    private void PlayMotion(Vector3 motion)
    {
        cam.localPosition += motion;
    }

    private void ResetPosition()
    {
        if (cam.localPosition == startPos) return;
        cam.localPosition = Vector3.Lerp(cam.localPosition, startPos, Time.deltaTime);
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + camHolder.localPosition.y, transform.position.z);
        pos += camHolder.forward * 15f;
        return pos;
    }
}
