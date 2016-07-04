using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class csValueManager : MonoBehaviour {

    public Slider revengeGuage;
    public Button btnRevenge;
    public GameObject player;
    public Image lifeImage;
    int fever;

    private csPlayer playerMethod;

    Queue<Image> life;

	// Use this for initialization
	void Start () {
        fever = 1;
        revengeGuage = GetComponent<Slider>();
        btnRevenge = GetComponent<Button>();
        lifeImage = GetComponent<Image>();
        playerMethod = player.GetComponent<csPlayer>();

        for (int i = 0; i < playerMethod.life; ++i)
            GainLife();
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    public void GainRevengeGuage(float amount)
    {
        revengeGuage.value += (fever * amount);

        if (revengeGuage.value >= 70.0f)
            SetRevengeButton(true);
        else
            SetRevengeButton(false);
    }

    public void SetRevengeButton(bool isActive)
    {
        btnRevenge.enabled = isActive;
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
        fever = isActive ? 2 : 1;
    }
}
