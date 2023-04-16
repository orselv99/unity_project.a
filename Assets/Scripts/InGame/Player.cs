using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rigidbody = null;
    private readonly float SPEED = 5f;
    private readonly float JUMP_POWER = 5f;
    private bool isLanded;
    private Animator animator = null;

    private void Start()
    {
        this.rigidbody = GetComponent<Rigidbody>();
        this.animator= GetComponent<Animator>();
    }

    private void Move()
    {
        var axisH = Input.GetAxis("Horizontal");
        var axisV = Input.GetAxis("Vertical");
        var move = new Vector3(axisH, 0, axisV).normalized;
        this.transform.position += move * Time.deltaTime * 3f;
        this.animator.SetBool("isRun", true);
        this.animator.SetBool("isShoot", true);

        // 방향확인 디버그
        /*        Debug.DrawRay(
                    this.cameraArmTransform.position,
                    new Vector3(this.cameraArmTransform.forward.x, 0f, this.cameraArmTransform.forward.z).normalized, Color.red);*/
    }
    private void Jump()
    {
        var isJump = Input.GetButtonDown("Jump");
        if (this.isLanded == true && isJump == true)
        {
            this.rigidbody.AddForce(Vector3.up * JUMP_POWER, ForceMode.Impulse);
        }
    }
    private void LookAround()
    {
        var mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    private void Update()
    {
        LookAround();
        Move();
        Jump();
    }

    private void OnCollisionStay(Collision collision)
    {
        this.isLanded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        this.isLanded = false;
    }
}
