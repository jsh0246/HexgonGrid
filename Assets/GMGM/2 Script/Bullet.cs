using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;

    private void Awake()
    {
        InitVariables();
    }

    private void InitVariables()
    {
        damage = 10;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Unit")
        {
            Destroy(gameObject);
            // 유닛과 충돌하면서 애니메이션? 이펙트? 추가

            other.gameObject.GetComponent<Unit>().GetDamage(damage);
        } else if(other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}