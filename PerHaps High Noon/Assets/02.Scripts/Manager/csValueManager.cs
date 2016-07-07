using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class csValueManager : MonoBehaviour {

    public Slider revengeGuage;
    public Button btnRevenge;
    public GameObject player;
    public GameObject life;

    // fever 상태 유무
    int fever;
    const int TRUE = 2;
    const int FALSE = 1;

    private csPlayer playerMethod;

    GameObject[] lifeImages;

	// Use this for initialization
	void Start () {
   
        revengeGuage = GameObject.Find("RevengeGuage").GetComponent<Slider>();
        btnRevenge = GameObject.Find("BtnHigh").GetComponent<Button>();
        playerMethod = player.GetComponent<csPlayer>();

        fever = FALSE;

        life.transform.GetChild(0).gameObject.SetActive(true);
        life.transform.GetChild(1).gameObject.SetActive(true);
        life.transform.GetChild(2).gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    public void ActiveRevenge()
    {
        if ( (GetRevengeGuage() >= 30.0f ) || 
             ((GetRevengeGuage() < 30.0f && playerMethod.isHighNoon)) )
            playerMethod.OnHighNoon();
        else
            return;
    }

    /// <summary>
    /// Revenge Guage의 값을 변경한다
    /// </summary>
    /// <param name="amount">guage가 증가, 감소하는 값</param>
    public void SetRevengeGuage(float amount)
    {
        if (amount >= 0)
            revengeGuage.value += (fever * amount);
        else 
            revengeGuage.value = revengeGuage.value < amount ? 0 : revengeGuage.value + amount;
    }

    /// <summary>
    ///  Revenge Guage를 반환한다
    /// </summary>
    public float GetRevengeGuage()
    {
        return revengeGuage.value;
    }

    public void GainLife()
    {
        //      Image lifeImg = Instantiate(lifeImage, new Vector3(-590 + (60 *count), 330, 0), Quaternion.identity) as Image;

        if (playerMethod.life >= 5)
            return;

        life.transform.GetChild(playerMethod.life).gameObject.SetActive(true);
        ++playerMethod.life;
    }

    public void ReduceLife()
    {
        if (playerMethod.life == 1)
            SceneManager.LoadScene("StageScene");
        else
        {
            --playerMethod.life;
            life.transform.GetChild(playerMethod.life).gameObject.SetActive(false);
            SetFeverMode(false);
        }
    }

    public void SetFeverMode(bool isActive)
    {
        fever = isActive ? TRUE : FALSE;
    }
}
