using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {

    public GameObject playerHPgameobject;
    private playerHP playerHP;
    public GameObject deathpanel;
	// Use this for initialization
	void Start () {
       playerHP = playerHPgameobject.GetComponent<playerHP>();
       deathpanel.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if(playerHP.IsDeath())
        {
            deathpanel.SetActive(true);
        }
	}
    void OnTriggerEnter(Collider hitInfo)
    {
        if(hitInfo.gameObject.tag == "Trap")
        {
            playerHP.lessHP();
        }
        else if (hitInfo.gameObject.tag == "addHP")
        {
            playerHP.addHP();
            Destroy(hitInfo.gameObject);
        }
        else if(hitInfo.gameObject.tag =="finish")
        {
            Application.LoadLevel("finish");
        }
    }
}
