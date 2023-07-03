using Cinemachine;
using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterData movementData;
    [SerializeField] private float mouseSenservity;
    CharacterController controller;
    CinemachineVirtualCamera virtualCamera;
    Vector3 moveDir;
    Vector2 mouseInput;
    Transform foot;
    Transform head;
    bool isFalling;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        virtualCamera = transform.Find("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
        foot = transform.Find("Foot");
        head = transform.Find("Head");
        Cursor.lockState = CursorLockMode.Locked;
    }

    private IEnumerator Start()
    {
#if UNITY_EDITOR
        float tempSenservity = mouseSenservity;
        yield return new WaitForSeconds(0.5f);
        mouseSenservity = tempSenservity;
#endif
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
        moveDir.z = input.y;

        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), -Input.GetAxisRaw("Mouse Y"));
    }

    private void Gravity()
    {
        if (CheckGround())
        {
            moveDir.y = 0;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDir.y = movementData.jumpForce;
                isFalling = false;
            }
        }
        else
        {
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
    }

    private bool CheckGround()
    {
        if (Physics.Raycast(foot.position, -foot.up, out RaycastHit hit, 0.5f))
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
        if (Physics.Raycast(head.position, head.up, out RaycastHit hit, 0.1f))
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
        virtualCamera.transform.Rotate(Vector3.right * mouseInput.y * mouseSenservity * Time.deltaTime);
    }
}
