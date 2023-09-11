using Calculation;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Raider : Unit, ICharacter
{
    private MovingController mv;
    private Animator anim;

    private RaycastHit hit;

    protected override void Start()
    {
        base.Start();

        InitVariables();
    }

    protected override void Update()
    {
        base.Update();
        Walk();
        DrawMoveRange();
    }

    private void InitVariables()
    {
        name = "Raider";
        maxHp = currentHp = 100f;
        moveRange = 3;

        mv = GetComponent<MovingController>();
        anim = GetComponentInChildren<Animator>();
    }

    private void MoveTo()
    {

    }

    public override void DrawMoveRange()
    {
        if (isSelected)
        {
            Vector3 curPos = transform.position;
            List<Vector3> list = new List<Vector3>();

            list.Add(curPos);
            for (int i = 1; i < moveRange; i++)
            {
                for (int j = 1; j <= -i + 3; j++)
                {
                    int a = i * 2;
                    int b = j * 2;

                    list.Add(curPos + Vector3.right * a + Vector3.forward * b);
                    list.Add(curPos + Vector3.left * a + Vector3.forward * b);
                    list.Add(curPos + Vector3.left * a + Vector3.back * b);
                    list.Add(curPos + Vector3.right * a + Vector3.back * b);
                }
            }

            for (int i = 1; i <= moveRange; i++)
            {
                int j = i * 2;

                list.Add(curPos + Vector3.right * j);
                list.Add(curPos + Vector3.left * j);
                list.Add(curPos + Vector3.forward * j);
                list.Add(curPos + Vector3.back * j);
            }

            foreach (Vector3 v in list)
            {
                Ray ray = new Ray(Camera.main.transform.position, v - Camera.main.transform.position);
                //Debug.DrawRay(Camera.main.transform.position, v - Camera.main.transform.position, Color.red, 5f);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Grid")))
                {
                    //originalColor = hit.collider.GetComponent<MeshRenderer>().material.color;
                    hit.collider.GetComponent<MeshRenderer>().material.color = Color.red;
                }
            }
        }
    }

    private void Walk()
    {
        if(mv.moveAllowed)
        {
            anim.SetBool("isWalk", true);
        }
    }

    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    public void SkillE()
    {
        throw new System.NotImplementedException();
    }

    public void SkillQ()
    {
        throw new System.NotImplementedException();
    }

    public void SkillUltR()
    {
        throw new System.NotImplementedException();
    }

    public void SkillW()
    {
        throw new System.NotImplementedException();
    }
}
