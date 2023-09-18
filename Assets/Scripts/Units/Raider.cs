using Calculation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Raider : Unit, ICharacter
{
    private MovingController mv;
    private Animator anim;

    //private RaycastHit rangeHit, outRangeHit;
    //private HashSet<GameObject> floors;
    //private Color grass;

    protected override void Start()
    {
        base.Start();

        InitVariables();
    }

    protected override void Update()
    {
        base.Update();

        Walk();
        Attack();
        SkillE();
    }

    private void InitVariables()
    {
        moveRange = 3;

        mv = GetComponent<MovingController>();
        anim = GetComponentInChildren<Animator>();

        //floors = new HashSet<GameObject>();
        //grass = new Color(96 / 255f, 219 / 255f, 178 / 255f, 1);
    }

    private void MoveTo()
    {

    }

    // �� �������� ���� ����
    // �� �Լ� Unit�� �����ϴµ� moveRange�� Raider(�ڽ�)�� ������ �� �� �ֳ�?
    //public override void DrawMoveRange()
    //{
    //    if (isSelected && movable)
    //    {
    //        Vector3 curPos = transform.position;
    //        List<Vector3> vList = new List<Vector3>();
    //        var toRemove = new HashSet<GameObject>();

    //        // 1. �̵������� ���� ��ǥ���� �ϴ� ���Ѵ�(�̵������� ���� floor, gameobject �ƴ�)
    //        // vList�� ���������̱⶧���� ������ �׻� �����̴�. �ֺ� �̵��������� ��ǥ�鸸 ��� ����
    //        vList.Add(curPos);
    //        for (int i = 1; i < moveRange; i++)
    //        {
    //            for (int j = 1; j <= -i + 3; j++)
    //            {
    //                int a = i * 2;
    //                int b = j * 2;

    //                vList.Add(curPos + Vector3.right * a + Vector3.forward * b);
    //                vList.Add(curPos + Vector3.left * a + Vector3.forward * b);
    //                vList.Add(curPos + Vector3.left * a + Vector3.back * b);
    //                vList.Add(curPos + Vector3.right * a + Vector3.back * b);
    //            }
    //        }

    //        for (int i = 1; i <= moveRange; i++)
    //        {
    //            int j = i * 2;

    //            vList.Add(curPos + Vector3.right * j);
    //            vList.Add(curPos + Vector3.left * j);
    //            vList.Add(curPos + Vector3.forward * j);
    //            vList.Add(curPos + Vector3.back * j);
    //        }

    //        // 2. �� ��ǥ��� ����ĳ�����ؼ� ������ Gameobject���� floors�� �����Ѵ�.
    //        foreach (Vector3 v in vList)
    //        {
    //            Ray ray = new Ray(Camera.main.transform.position, v - Camera.main.transform.position);
    //            //Debug.DrawRay(Camera.main.transform.position, v - Camera.main.transform.position, Color.yellow, 1f);

    //            if (Physics.Raycast(ray, out rangeHit, Mathf.Infinity, LayerMask.GetMask("Grid")))
    //            {
    //                floors.Add(rangeHit.collider.gameObject);
    //            }
    //        }

    //        // 3. ������ ��� Gameobejct�� floors�� �����߰�, �ش� Gameobject�� ��� ������ �����Ѵ�
    //        // �׸��� ���� ĳ���Ϳ� �� �Ÿ��� �ִ� gameobject���� �����صξ��ٰ� �����Ѵ�
    //        // ��������� �� �ֺ��� gameobject�鸸 ������ ����ä�� ���´�.
    //        foreach(GameObject o in floors)
    //        {
    //            Ray ray = new Ray(Camera.main.transform.position, o.transform.position - Camera.main.transform.position);
    //            //Debug.DrawRay(Camera.main.transform.position, o.transform.position - Camera.main.transform.position, Color.blue, 3f);

    //            if(Physics.Raycast(ray, out outRangeHit, Mathf.Infinity, LayerMask.GetMask("Grid")))
    //            {
    //                o.GetComponent<MeshRenderer>().material.color = Color.green;
    //            }

    //            Vector2Int player = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
    //            Vector2Int cell = new Vector2Int((int)o.transform.position.x, (int)o.transform.position.z);

    //            if(Calculation.Calc.ManhattenDistance(player, cell) > moveRange * 2)
    //            {
    //                o.GetComponent<MeshRenderer>().material.color = grass;
    //                toRemove.Add(o);
    //            }
    //        }

    //        foreach (GameObject oo in toRemove)
    //        {
    //            floors.Remove(oo);
    //        }

    //        toRemove.Clear();
    //    }
    //}

    //public override void RemoveMoveRange()
    //{
    //    foreach(GameObject o in floors)
    //    {
    //        o.GetComponent<MeshRenderer>().material.color = grass;
    //    }
    //}

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

            Vector2Int gridPos = GetCurrentPosition();
            Vector2Int squarePos = Squares.Instance.GridToSquareCoordinate(gridPos);

            Vector2Int[] skillRange = new Vector2Int[4];
            Vector2Int v1 = new Vector2Int(Mathf.RoundToInt(transform.forward.x), Mathf.RoundToInt(transform.forward.z));
            Vector2Int v2 = new Vector2Int(Mathf.RoundToInt(transform.right.x), Mathf.RoundToInt(transform.right.z));
            Vector2Int v3 = new Vector2Int(-Mathf.RoundToInt(transform.right.x), -Mathf.RoundToInt(transform.right.z));

            skillRange[0] = v1;
            skillRange[1] = v1 + v1;
            skillRange[2] = skillRange[1] + v2;
            skillRange[3] = skillRange[1] + v3;

            //foreach (var skill in skillRange)
            //    print(skill);

            foreach (var skill in skillRange) {
                Unit unit = Squares.Instance.GetUnit(squarePos + skill);
                //print(squarePos + skill);

                if (unit != null)
                {
                    unit.GetDamage(10);
                }
            }

        }
    }

    public void SkillE()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("Run");

            //transform.rotation = Quaternion.LookRotation(Vector3.left);

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


// bug1 : �̵������� ����� ���̶���Ʈ ���� �ʴ� ��찡 �ִ�. �ణ ����� �̻��ϰ� ���ڸ��̵����ָ� ��������
//          �Ƹ� (int) casting���� �ݿø��ø��������� ����ź���Ͻ����� �ݿø�? �Ǵ� ����ź���Ͻ� ����� �����ϴµ��� ������ �ִµ���