using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillItem : MonoBehaviour {

    public float coldTime = 8f;
    public GameObject playerHPgameobject;
    private float timer = 0f;
    private Image filledImage;
    private bool isStartTimer = false;
    private playerHP playerHP;
    // Use this for initialization
    void Start () {
        filledImage = transform.Find("FilledSkill").GetComponent<Image>();
        playerHP = playerHPgameobject.GetComponent<playerHP>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!isStartTimer)
                playerHP.addHP();
            isStartTimer = true;
            
        }
        if(isStartTimer)
        {
            timer += Time.deltaTime;
            filledImage.fillAmount = (coldTime - timer) / coldTime ;
        }
        if (timer >= coldTime) 
        {
            timer = 0f;
            filledImage.fillAmount = 0f;
            isStartTimer = false;
        }
	}
}
