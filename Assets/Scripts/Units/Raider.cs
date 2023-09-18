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

    // 개 거지같이 만듦 ㅋㅋ
    // 이 함수 Unit에 구현하는데 moveRange는 Raider(자식)의 변수를 쓸 수 있나?
    //public override void DrawMoveRange()
    //{
    //    if (isSelected && movable)
    //    {
    //        Vector3 curPos = transform.position;
    //        List<Vector3> vList = new List<Vector3>();
    //        var toRemove = new HashSet<GameObject>();

    //        // 1. 이동범위로 만들 좌표들을 일단 구한다(이동범위로 만들 floor, gameobject 아님)
    //        // vList는 지역변수이기때문에 개수가 항상 고정이다. 주변 이동범위내의 좌표들만 계속 유지
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

    //        // 2. 그 좌표들로 레이캐스팅해서 만나는 Gameobject들을 floors에 저장한다.
    //        foreach (Vector3 v in vList)
    //        {
    //            Ray ray = new Ray(Camera.main.transform.position, v - Camera.main.transform.position);
    //            //Debug.DrawRay(Camera.main.transform.position, v - Camera.main.transform.position, Color.yellow, 1f);

    //            if (Physics.Raycast(ray, out rangeHit, Mathf.Infinity, LayerMask.GetMask("Grid")))
    //            {
    //                floors.Add(rangeHit.collider.gameObject);
    //            }
    //        }

    //        // 3. 만났던 모든 Gameobejct를 floors에 저장했고, 해당 Gameobject를 모두 색깔을 변경한다
    //        // 그리고 현재 캐릭터와 먼 거리에 있는 gameobject들은 저장해두었다가 삭제한다
    //        // 결론적으로 내 주변의 gameobject들만 색깔이 변한채로 남는다.
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


// bug1 : 이동범위가 제대로 하이라이트 되지 않는 경우가 있다. 약간 모양이 이상하게 제자리이동해주면 고쳐진다
//          아마 (int) casting에서 반올림올림문제인지 맨하탄디스턴스쪽의 반올림? 또는 맨하탄디스턴스 결과로 삭제하는데서 오류가 있는듯함