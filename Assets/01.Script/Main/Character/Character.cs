using Cinemachine;
using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    // Move
    public CharacterData MovementData { get { return movementData; } }
    [SerializeField] private CharacterData movementData;
    Vector3 moveDir;
    CharacterController controller;

    // Rotate
    [SerializeField] private float mouseSenservity;
    Transform camHolder;
    Vector2 mouseInput;
    Vector2 limitRotX = new Vector2(-75, 75);
    float curRotateX;

    // Cam
    private CinemachineVirtualCamera vcam;

    // Animation
    Animator animator;

    // Jump / Fall
    Transform foot;
    Transform head;
    bool isFalling;
    bool isGround;
    float fallingTime;

    // HeadBobbing
    HeadBobbing headBobbing;

    // Layer
    LayerMask rayLayer;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        camHolder = transform.Find("CamHolder");
        vcam = camHolder.GetComponentInChildren<CinemachineVirtualCamera>(); 
        foot = transform.Find("Foot");
        head = transform.Find("Head");
        animator = transform.Find("Model").GetComponent<Animator>();
        rayLayer = ~(1 << LayerMask.NameToLayer("Character"));
        headBobbing = GetComponent<HeadBobbing>();
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        headBobbing.Init(this, vcam.transform, camHolder);
    }

    private void Update()
    {
        GetInput();
        Gravity();
        Move();
        RotateChatacter();
    }

    private void GetInput()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input.Normalize();

        moveDir.x = input.x;
        moveDir.z = Mathf.Clamp(input.y, -0.5f, 1f);
        Vector2 lerpInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        animator.SetFloat("Horizontal", lerpInput.x);
        animator.SetFloat("Vertical", lerpInput.y);

        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), -Input.GetAxisRaw("Mouse Y"));
    }

    private void Gravity()
    {
        if (CheckGround())
        {
            isGround = true;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDir.y = movementData.jumpForce;
                isFalling = false;
            }
        }
        else
        {
            if (isGround && isFalling)
            {
                moveDir.y = 0f;
                isGround = false;
            }
            moveDir.y -= 9.8f * Time.deltaTime;
        }
        if (moveDir.y > 0.1f && !CheckGround())
        {
            if (CheckHeadBump())
            {
                moveDir.y = 0;
            }
        }

        if (moveDir.y <= 0)
        {
            isFalling = true;
        }

        var perlin = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (perlin)
        {
            if (isFalling && !isGround)
                perlin.m_AmplitudeGain = moveDir.y * 0.04f;
            else
                perlin.m_AmplitudeGain = Mathf.Lerp(perlin.m_AmplitudeGain, 0f, Time.deltaTime * 5f);
        }
    }

    private bool CheckGround()
    {
        if (Physics.Raycast(foot.position, -foot.up, out RaycastHit hit, 0.5f, rayLayer))
        {
            if (hit.transform.CompareTag("Ground") && isFalling)
            {
                return true;
            }
        }
        return false;
    }

    private bool CheckHeadBump()
    {
        if (Physics.Raycast(head.position, head.up, out RaycastHit hit, 0.1f, rayLayer))
        {
            return true;
        }
        return false;
    }

    private void Move()
    {
        Vector3 moveVector = transform.TransformDirection(moveDir);
        moveVector.y = moveDir.y;

        controller.Move(moveVector * movementData.speed * Time.deltaTime);
    }

    private void RotateChatacter()
    {
        transform.Rotate(Vector3.up * mouseInput.x * mouseSenservity * Time.deltaTime);

        curRotateX += mouseInput.y * mouseSenservity * Time.deltaTime;
        curRotateX = Mathf.Clamp(curRotateX, limitRotX.x, limitRotX.y);

        camHolder.transform.localRotation = Quaternion.Euler(curRotateX, 0, 0);
    }
}
