using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform cameraArmTransform = null;
    private Rigidbody rigidbody = null;
    [SerializeField]
    private Canvas canvas = null;

    private readonly float SPEED = 5f;
    private readonly float JUMP_POWER = 5f;
    private bool isLanded;

    private void Start()
    {
        this.rigidbody = GetComponent<Rigidbody>();
    }

    private void Move()
    {
        var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        var isMove = input.magnitude != 0; // magnitude: 입력이 되고있는지 확인, animator state 에 사용
        if (isMove == true)
        {
            var forward = new Vector3(this.cameraArmTransform.forward.x, 0f, this.cameraArmTransform.forward.z).normalized;
            var right = new Vector3(this.cameraArmTransform.right.x, 0f, this.cameraArmTransform.right.z).normalized;
            var direction = forward * input.y + right * input.x;

            // direction: 이동방향을 바라봄, forward: 카메라가 보는방향을 바라봄
            this.transform.forward = forward;
            this.transform.position += direction * Time.deltaTime * SPEED;

            this.cameraArmTransform.transform.position = this.transform.position;
        }

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
        var angle = this.cameraArmTransform.rotation.eulerAngles;

        var x = angle.x - mouseDelta.y;

/*        // TODO: 외국은 반대니까 옵션으로 반전처리
        // 카메라 회전각 제한
        if (x < 180f)
        {
            x = Mathf.Clamp(x, 10f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 360f);
        }*/

        this.cameraArmTransform.rotation = Quaternion.Euler(x, angle.y + mouseDelta.x, angle.z);
    }

    private void Update()
    {
        LookAround();
        Move();
        Jump();

        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            this.canvas.gameObject.SetActive(true);
        }
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
