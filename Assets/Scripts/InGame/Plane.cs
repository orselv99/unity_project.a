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

    // 종료위치에 가면 plane 삭제 (z -350 이상되면 삭제)
    private void Start()
    {
        var index = Random.Range(0, items.Length);
        this.giftbox = items[index];
        this.giftbox.SetActive(true);
    }

    void Update()
    {
        // 매 30초마다 출현하여 상공에서 전장위치에 다다르면 아이템을 랜덤으로 선택하여 드랍
        if (this.gameObject.transform.position.z > -30f)
        {
            this.giftbox.GetComponent<Rigidbody>().useGravity = true;
        }

        this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * 5f);
        this.prop.Rotate(Vector3.forward * Time.deltaTime * 500f);
    }
}
