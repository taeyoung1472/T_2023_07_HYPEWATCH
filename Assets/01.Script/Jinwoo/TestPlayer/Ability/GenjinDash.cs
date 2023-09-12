using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GenjinDash : Ability
{
    [SerializeField] private float dashForce;
    [SerializeField] private float dashDuration;

    private Rigidbody rb;

    [SerializeField] private CinemachineVirtualCamera cam;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Cast());
        }
    }

    public override IEnumerator Cast()
    {
        //rb.velocity = (Camera.main.transform.forward * dashForce);

        rb.AddForce(cam.transform.forward * dashForce, ForceMode.VelocityChange);

        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector3.zero;
    }

    
}
