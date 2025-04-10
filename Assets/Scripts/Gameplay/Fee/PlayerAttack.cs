using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;

    public GameObject RangeTrigger;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            animator.SetTrigger("attack");
        }
    }
    public void Attack()
    {
        RangeTrigger.SetActive(true);
    }
    public void AttackFinsiihed()
    {
        RangeTrigger.SetActive(false);
    }
}
