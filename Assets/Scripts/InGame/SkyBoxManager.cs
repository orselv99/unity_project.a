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
        // 지정된 구름 오브젝트를 랜덤으로 생성
        var random = Random.Range(0, this.clouds.Length);

        var temp = new CloudType();
        temp.obj = Instantiate(this.clouds[random]);
        temp.speed = Random.Range(3f, 10f);

        // 생성된 구름을 spawn position 에 위치
        temp.obj.transform.position = new Vector3(
            this.cloudSpawnPositions[index].transform.position.x + Random.Range(-10f, 10f), // 정해진 위치에서 조금씩 변경
            Random.Range(50f, 100f),                                                    // 높낮이 변경
            this.cloudSpawnPositions[index].transform.position.z);                          // 고정

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
        // 구름 이동
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
            // z 가 200 이면 destroy
            if (this.currents[i].obj.transform.position.z >= 200)
            {
                Destroy(this.currents[i].obj);
                this.currents.Remove(this.currents[i]);

                // 삭제된만큼 하나 추가
                GenerateCloud(i);
            }
            else
            {
                i++;
            }
        }
    }
}
