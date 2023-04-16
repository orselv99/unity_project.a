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
        var isMove = input.magnitude != 0; // magnitude: �Է��� �ǰ��ִ��� Ȯ��, animator state �� ���
        if (isMove == true)
        {
            var forward = new Vector3(this.cameraArmTransform.forward.x, 0f, this.cameraArmTransform.forward.z).normalized;
            var right = new Vector3(this.cameraArmTransform.right.x, 0f, this.cameraArmTransform.right.z).normalized;
            var direction = forward * input.y + right * input.x;

            // direction: �̵������� �ٶ�, forward: ī�޶� ���¹����� �ٶ�
            this.transform.forward = forward;
            this.transform.position += direction * Time.deltaTime * SPEED;

            this.cameraArmTransform.transform.position = this.transform.position;
        }

        // ����Ȯ�� �����
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

/*        // TODO: �ܱ��� �ݴ�ϱ� �ɼ����� ����ó��
        // ī�޶� ȸ���� ����
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
