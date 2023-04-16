using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    [SerializeField]
    private Transform prop = null;

    [SerializeField]
    private GameObject[] items = null;

    private GameObject giftbox = null;

    // ������ġ�� ���� plane ���� (z -350 �̻�Ǹ� ����)
    private void Start()
    {
        var index = Random.Range(0, items.Length);
        this.giftbox = items[index];
        this.giftbox.SetActive(true);
    }

    void Update()
    {
        // �� 30�ʸ��� �����Ͽ� ������� ������ġ�� �ٴٸ��� �������� �������� �����Ͽ� ���
        if (this.gameObject.transform.position.z > -30f)
        {
            this.giftbox.GetComponent<Rigidbody>().useGravity = true;
        }

        this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * 5f);
        this.prop.Rotate(Vector3.forward * Time.deltaTime * 500f);
    }
}
