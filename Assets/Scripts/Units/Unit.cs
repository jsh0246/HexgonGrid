using Calculation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool isSelected { get; set; }
    public int moveRange { get; protected set; }

    protected Vector3Int currentPos { get; set; }
    protected int maxHp, currentHp;
    [SerializeField]
    private RectTransform popupMenu;
    private HealthBar healthBar;

    protected bool movable, attackable;
    //protected Vector3 headDir;
    
    protected virtual void Start()
    {
        InitVariables();
    }

    protected virtual void Update()
    {
        PopupMenu();
    }

    private void InitVariables()
    {
        isSelected = false;

        // ���� �����ϴϱ� �ϴ� true�� �д�
        movable = true;
        attackable = true;

        currentHp = maxHp = 100;
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.SetMaxHealth(maxHp);
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

    public virtual void DrawMoveRange()
    {

    }

    private void PopupMenu()
    {
        if (Input.GetMouseButton(2) && isSelected)
        {
            Vector3 popupPos = Camera.main.WorldToScreenPoint(transform.position);
            popupMenu.anchoredPosition = popupPos;
        }



        // ���������� �� ���߰� ���� ����
        // ���� ���߱� / ������ �ڿ��� ���̱� / ���� �׸� ã�� / ���� �׸� ���ӻ��� ���� Ǯ��鼭
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
}