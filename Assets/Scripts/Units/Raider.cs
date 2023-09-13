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

    private RaycastHit rangeHit, outRangeHit;
    //private List<Vector3> vList;
    //private HashSet<Vector3> vList;
    private HashSet<GameObject> floors;
    private Color grass;

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
        Attack();
        SkillE();
    }

    private void InitVariables()
    {
        name = "Raider";
        maxHp = currentHp = 100f;
        moveRange = 3;

        mv = GetComponent<MovingController>();
        anim = GetComponentInChildren<Animator>();

        //vList = new List<Vector3>();
        //vList = new HashSet<Vector3>();
        floors = new HashSet<GameObject>();
        grass = new Color(96 / 255, 219 / 255, 178 / 255, 1);
    }

    private void MoveTo()
    {

    }

    public override void DrawMoveRange()
    {
        if (isSelected)
        {
            Vector3 curPos = transform.position;
            List<Vector3> vList = new List<Vector3>();

            vList.Add(curPos);
            for (int i = 1; i < moveRange; i++)
            {
                for (int j = 1; j <= -i + 3; j++)
                {
                    int a = i * 2;
                    int b = j * 2;

                    vList.Add(curPos + Vector3.right * a + Vector3.forward * b);
                    vList.Add(curPos + Vector3.left * a + Vector3.forward * b);
                    vList.Add(curPos + Vector3.left * a + Vector3.back * b);
                    vList.Add(curPos + Vector3.right * a + Vector3.back * b);
                }
            }

            for (int i = 1; i <= moveRange; i++)
            {
                int j = i * 2;

                vList.Add(curPos + Vector3.right * j);
                vList.Add(curPos + Vector3.left * j);
                vList.Add(curPos + Vector3.forward * j);
                vList.Add(curPos + Vector3.back * j);
            }

            foreach (Vector3 v in vList)
            {
                Ray ray = new Ray(Camera.main.transform.position, v - Camera.main.transform.position);
                Debug.DrawRay(Camera.main.transform.position, v - Camera.main.transform.position, Color.yellow, 1f);

                if (Physics.Raycast(ray, out rangeHit, Mathf.Infinity, LayerMask.GetMask("Grid")))
                {
                    //originalColor = rangeHit.collider.GetComponent<MeshRenderer>().material.color;
                    rangeHit.collider.GetComponent<MeshRenderer>().material.color = Color.red;

                    floors.Add(rangeHit.collider.gameObject);
                }
            }

            foreach(GameObject o in floors)
            {
                Ray ray = new Ray(Camera.main.transform.position, o.transform.position - Camera.main.transform.position);
                Debug.DrawRay(Camera.main.transform.position, o.transform.position - Camera.main.transform.position, Color.blue, 3f);

                if(Physics.Raycast(ray, out outRangeHit, Mathf.Infinity, LayerMask.GetMask("Grid")))
                {
                    if(rangeHit.collider.GetComponent<MeshRenderer>().material.color == Color.red)
                    {
                        print("ININ");
                        //rangeHit.collider.GetComponent<MeshRenderer>().material.color = grass;
                        //floors.Remove(o);
                    }
                }
            }

            print("Floors : " + floors.Count);
            print(vList.Count);
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetTrigger("Attack");



        }
    }

    public void SkillE()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("Run");



        }
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
