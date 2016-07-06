using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class csValueManager : MonoBehaviour {

    public Slider revengeGuage;
    public Button btnRevenge;
    public GameObject player;
    public Image lifeImage;

    // fever 상태 유무
    int fever;
    const int TRUE = 2;
    const int FALSE = 1;

    private csPlayer playerMethod;

    Queue<Image> life;

	// Use this for initialization
	void Start () {
   
        life = new Queue<Image>();
        revengeGuage = GameObject.Find("RevengeGuage").GetComponent<Slider>();
        btnRevenge = GameObject.Find("BtnHigh").GetComponent<Button>();
        playerMethod = player.GetComponent<csPlayer>();

//        btnRevenge.enabled = false;
        fever = FALSE;

        for (int i = 0; i < playerMethod.life; ++i)
        {
            Image lifeImg = Instantiate(lifeImage, new Vector3(-590 + (60 * i), 330, 0), Quaternion.identity) as Image;
            life.Enqueue(lifeImg);
        }
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
        int count = life.Count;
        Image lifeImg = Instantiate(lifeImage, new Vector3(-590 + (60 *count), 330, 0), Quaternion.identity) as Image;
        life.Enqueue(lifeImg);
        ++playerMethod.life;
    }

    public void ReduceLife()
    {
        Image lifeImg = life.Dequeue();
        Destroy(lifeImg.gameObject);
        --playerMethod.life;
        SetFeverMode(false);
    }

    public void SetFeverMode(bool isActive)
    {
        fever = isActive ? TRUE : FALSE;
    }
}
