using Calculation;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public bool isSelected { get; set; }
    public int moveRange { get; protected set; }
    
    /* Skills */
    [SerializeField] protected Sprite[] skillImages;
    [SerializeField] protected Image[] skillFrames;
    protected UnitSkills unitSkills;
    protected KeyCode[] keyCodes;
    protected delegate void Skill(int n);
    protected List<Skill> skills;

    [SerializeField] protected SpriteRenderer selector;
    [SerializeField] private RectTransform popupMenu;

    protected Vector3Int currentPos { get; set; }
    protected int maxHp, currentHp;

    protected MovingController mv;
    protected Animator anim;

    protected bool movable, attackable;

    private HealthBar healthBar;

    private RaycastHit rangeHit, outRangeHit;
    private HashSet<GameObject> floors;
    private Color grass;

    protected SoundManager soundManager;

    protected int[] skillDamage;

    protected virtual void Start()
    {
        InitVariables();
    }

    protected virtual void Update()
    {
        Walk();

        PopupMenu();
        DrawMoveRange();
    }

    protected virtual void LateUpdate()
    {
        //SkillCooldown();
    }

    private void InitVariables()
    {
        isSelected = false;

        unitSkills = GetComponent<UnitSkills>();
        keyCodes = new KeyCode[4]{ KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R };

        // 자주 실행하니까 일단 true로 둔다
        movable = true;
        attackable = true;

        mv = GetComponent<MovingController>();
        anim = GetComponentInChildren<Animator>();

        currentHp = maxHp = 100;
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.SetMaxHealth(maxHp);

        floors = new HashSet<GameObject>();
        grass = new Color(96 / 255f, 219 / 255f, 178 / 255f, 1);

        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    public Vector2Int GetCurrentPosition()
    {
        currentPos = GlobalGrid.Instance.Grid.WorldToCell(transform.position);

        return Calc.Vector3to2Int(currentPos);
    }

    public Vector3Int GetCurrentPositionVector3Int()
    {
        currentPos = GlobalGrid.Instance.Grid.WorldToCell(transform.position);

        return currentPos;
    }

    // 개 거지같이 만듦 ㅋㅋ
    public void DrawMoveRange()
    {
        if (isSelected && movable)
        {
            Vector3 curPos = transform.position;
            List<Vector3> vList = new List<Vector3>();
            var toRemove = new HashSet<GameObject>();

            // 1. 이동범위로 만들 좌표들을 일단 구한다(이동범위로 만들 floor, gameobject 아님)
            // vList는 지역변수이기때문에 개수가 항상 고정이다. 주변 이동범위내의 좌표들만 계속 유지
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

            // 2. 그 좌표들로 레이캐스팅해서 만나는 Gameobject들을 floors에 저장한다.
            foreach (Vector3 v in vList)
            {
                Ray ray = new Ray(Camera.main.transform.position, v - Camera.main.transform.position);
                //Debug.DrawRay(Camera.main.transform.position, v - Camera.main.transform.position, Color.yellow, 1f);

                if (Physics.Raycast(ray, out rangeHit, Mathf.Infinity, LayerMask.GetMask("Area")))
                {
                    if(rangeHit.collider.gameObject)
                        floors.Add(rangeHit.collider.gameObject);
                }
            }

            // 3. 만났던 모든 Gameobejct를 floors에 저장했고, 해당 Gameobject를 모두 색깔을 변경한다
            // 그리고 현재 캐릭터와 먼 거리에 있는 gameobject들은 저장해두었다가 삭제한다
            // 결론적으로 내 주변의 gameobject들만 색깔이 변한채로 남는다.
            foreach (GameObject o in floors)
            {
                // //if (o != null) 최적화?하기?
                // //if (o != null)과 위의 if(rangeHit.collider.gameObject) 와 아래의 Dead()에 SetActive() or Destroy는 같이 연관되어있음
                //if (o != null)
                {
                    Ray ray = new Ray(Camera.main.transform.position, o.transform.position - Camera.main.transform.position);
                    //Debug.DrawRay(Camera.main.transform.position, o.transform.position - Camera.main.transform.position, Color.blue, 3f);

                    if (Physics.Raycast(ray, out outRangeHit, Mathf.Infinity, LayerMask.GetMask("Area")))
                    {
                        o.GetComponent<MeshRenderer>().material.color = Color.green;
                    }

                    Vector2Int player = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
                    Vector2Int cell = new Vector2Int((int)o.transform.position.x, (int)o.transform.position.z);

                    if (Calculation.Calc.ManhattenDistance(player, cell) > moveRange * 2)
                    {
                        o.GetComponent<MeshRenderer>().material.color = grass;
                        toRemove.Add(o);
                    }
                }
            }

            foreach (GameObject oo in toRemove)
            {
                floors.Remove(oo);
            }

            toRemove.Clear();
        }
    }

    public void RemoveMoveRange()
    {
        foreach (GameObject o in floors)
        {
            o.GetComponent<MeshRenderer>().material.color = grass;
        }
    }

    private void PopupMenu()
    {
        if (Input.GetMouseButton(2) && isSelected)
        {
            Vector3 popupPos = Camera.main.WorldToScreenPoint(transform.position);
            popupMenu.anchoredPosition = popupPos;
        }



        // 게임형식을 좀 갖추고 세부 진행
        // 파이 맞추기 / 죽으면 뒤에꺼 보이기 / 파이 그림 찾기 / 파이 그림 연속생성 문제 풀어가면서
        // 
    }

    private void PopupMenuDown()
    {
        popupMenu.anchoredPosition = Vector3.down * 2000;
    }

    public void PopupMove()
    {
        movable = true;
    }

    public void PopupAttack()
    {
        attackable = true;
    }

    public void PopupCancel()
    {
        PopupMenuDown();
    }

    public void GetDamage(int damage)
    {
        currentHp -= damage;
        healthBar.SetHealth(currentHp);

        print(name + " : " + currentHp);

        if (currentHp <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        Destroy(gameObject);

        Vector2Int gridPos = GetCurrentPosition();
        Vector2Int squarePos = Squares.Instance.GridToSquareCoordinate(gridPos);

        Vector2Int[,] skillRange = new Vector2Int[3, 3];
        //List<Vector2Int> skillRange = new List<Vector2Int>();
        for(int i=0; i<3; i++)
        {
            for(int j=0; j<3; j++)
            {
                skillRange[i, j] = new Vector2Int(i-1, j-1);
                //print(skillRange[i, j]);
            }
        }

        foreach (var skill in skillRange)
        {
            Area area = Squares.Instance.GetObject(squarePos + skill, Type.GetType("Area")) as Area;


            // 뭐가 더 나을까
            area.gameObject.SetActive(false);
            //Destroy(area.gameObject);
        }
    }

    public void SkillImageChange()
    {
        for (int i = 0; i < skillFrames.Length; i++)
        {
            skillFrames[i].sprite = skillImages[i];
        }
    }

    public void SkillImageChangeToBlank()
    {
        for (int i = 0; i < skillFrames.Length; i++)
        {
            skillFrames[i].sprite = null;

            // 허공에 찍었을 떄 처리 코드 넣기
            unitSkills.OnCancel(i);

        }
    }

    protected virtual void SkillCooldown()
    {
        unitSkills.SkillCooldown();
        if(isSelected)
        {
            unitSkills.ChangeUnit();
        }
    }

    public void OnSelected()
    {
        selector.gameObject.SetActive(true);
    }

    public void OnDeselected()
    {
        selector.gameObject.SetActive(false);
    }

    private void Walk()
    {
        if (mv.moveAllowed)
        {
            anim.SetBool("isWalk", true);
        }
        else
        {
            anim.SetBool("isWalk", false);
        }
    }
}