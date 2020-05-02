using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public GameObject player;
    public GameObject playerHP;
    private Animator anim;
    public float DropTime;
    public float velocity;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("unitychan");
        playerHP = GameObject.Find("nowHP");
        DropTime = 2.4f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //print(transform.position);
        Vector3 playerPos = player.transform.position;
        Vector3 enemyToplayer = playerPos - this.transform.position;
        RaycastHit hit = new RaycastHit();
        if (enemyToplayer.magnitude < 10)
        {
            if (Physics.Raycast(this.transform.position, enemyToplayer.normalized, out hit))
            {
                if (hit.collider.gameObject.name == "unitychan")
                {
                    enemyToplayer.y = 0;
                    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(enemyToplayer), Time.fixedDeltaTime * 2);
                    if (enemyToplayer.magnitude > 2)
                    {
                        this.transform.position += enemyToplayer.normalized * velocity * Time.fixedDeltaTime;
                        anim.SetBool("isIdle", false);
                        anim.SetBool("isWalking", true);
                        anim.SetBool("isAttacking", false);
                        DropTime = 2.4f;
                    }
                    else
                    {
                        anim.SetBool("isIdle", false);
                        anim.SetBool("isWalking", false);
                        anim.SetBool("isAttacking", true);
                        DropTime -= Time.deltaTime;
                        if (DropTime <= 0)
                        {
                            playerHP.GetComponent<playerHP>().lessHP();
                            DropTime = 2.4f;
                        }
                    }
                }
                else
                {
                    anim.SetBool("isIdle", true);
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isAttacking", false);
                }
            }
        }
        else
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);
        }
    }
}
