using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] cloudSpawnPositions = null;
    [SerializeField]
    private GameObject[] clouds = null;
    
    private struct CloudType
    {
        public GameObject obj;
        public float speed;
    }
    private List<CloudType> currents = null;

    private void GenerateCloud(int index)
    {
        // ������ ���� ������Ʈ�� �������� ����
        var random = Random.Range(0, this.clouds.Length);

        var temp = new CloudType();
        temp.obj = Instantiate(this.clouds[random]);
        temp.speed = Random.Range(3f, 10f);

        // ������ ������ spawn position �� ��ġ
        temp.obj.transform.position = new Vector3(
            this.cloudSpawnPositions[index].transform.position.x + Random.Range(-10f, 10f), // ������ ��ġ���� ���ݾ� ����
            Random.Range(50f, 100f),                                                    // ������ ����
            this.cloudSpawnPositions[index].transform.position.z);                          // ����

        this.currents.Add(temp);
    }

    private void Start()
    {
        this.currents = new List<CloudType>();

        for (int i = 0; i < this.cloudSpawnPositions.Length; i++)
        {
            GenerateCloud(i);
        }
    }

    private void Update()
    {
        // ���� �̵�
        foreach (var data in this.currents)
        {
            data.obj.transform.Translate(Vector3.forward * data.speed * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        int i = 0;
        while (i < this.currents.Count)
        {
            // z �� 200 �̸� destroy
            if (this.currents[i].obj.transform.position.z >= 200)
            {
                Destroy(this.currents[i].obj);
                this.currents.Remove(this.currents[i]);

                // �����ȸ�ŭ �ϳ� �߰�
                GenerateCloud(i);
            }
            else
            {
                i++;
            }
        }
    }
}
