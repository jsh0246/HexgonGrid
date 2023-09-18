using Calculation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    
    public bool isSelected { get; set; }
    public int moveRange { get; protected set; }

    protected Vector3Int currentPos { get; set; }
    protected int maxHp, currentHp;
    [SerializeField] private RectTransform popupMenu;
    [SerializeField] protected Sprite[] skillImages;
    [SerializeField] protected Image[] skillFrames;
    private HealthBar healthBar;

    protected bool movable, attackable;
    [SerializeField] protected SpriteRenderer selector;
    //protected Vector3 headDir;

    private RaycastHit rangeHit, outRangeHit;
    private HashSet<GameObject> floors;
    private Color grass;

    protected virtual void Start()
    {
        InitVariables();
    }

    protected virtual void Update()
    {
        PopupMenu();
        DrawMoveRange();
    }

    private void InitVariables()
    {
        isSelected = false;

        // 자주 실행하니까 일단 true로 둔다
        movable = true;
        attackable = true;

        currentHp = maxHp = 100;
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.SetMaxHealth(maxHp);

        floors = new HashSet<GameObject>();
        grass = new Color(96 / 255f, 219 / 255f, 178 / 255f, 1);
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

                if (Physics.Raycast(ray, out rangeHit, Mathf.Infinity, LayerMask.GetMask("Grid")))
                {
                    floors.Add(rangeHit.collider.gameObject);
                }
            }

            // 3. 만났던 모든 Gameobejct를 floors에 저장했고, 해당 Gameobject를 모두 색깔을 변경한다
            // 그리고 현재 캐릭터와 먼 거리에 있는 gameobject들은 저장해두었다가 삭제한다
            // 결론적으로 내 주변의 gameobject들만 색깔이 변한채로 남는다.
            foreach (GameObject o in floors)
            {
                Ray ray = new Ray(Camera.main.transform.position, o.transform.position - Camera.main.transform.position);
                //Debug.DrawRay(Camera.main.transform.position, o.transform.position - Camera.main.transform.position, Color.blue, 3f);

                if (Physics.Raycast(ray, out outRangeHit, Mathf.Infinity, LayerMask.GetMask("Grid")))
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

        print(currentHp);
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
}