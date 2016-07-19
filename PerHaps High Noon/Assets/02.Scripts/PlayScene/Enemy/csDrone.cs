using System.Collections;
using UnityEngine;

public class csDrone : csEnemyObject
{
    public GameObject drone;

    // Called by CameraPath Manager
    IEnumerator OnMoveEnd()
    {
        yield return new WaitForSeconds(aimCoolTime);

        drone.GetComponent<Animator>().SetInteger("state", 0);

        standard.SetActive(true);

        value.SetActive(true);

        GetComponent<Collider>().enabled = true;
        standard.GetComponent<Renderer>().sharedMaterial.color = Color.white;
        OnChangeNote(false);

        // aim을 player 기준으로 보이도록
        standard.transform.LookAt(GameObject.Find("pPlayer").transform.position + Vector3.up * 1.25f);

        if (itemType != Common.ITEM_TYPE.NONE)
            transform.parent.GetComponent<csEnemyManager>().InitItem(gameObject);
        
    }

    //카메라페스 경로 관리자가 호출
    void OnMoveStart()
    {
        drone.GetComponent<Animator>().SetInteger("state", 1);
    }
}
