using Calculation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public bool isSelected { get; set; }
    public int moveRange { get; protected set; }

    protected Vector3Int currentPos { get; set; }
    protected float maxHp, currentHp;
    [SerializeField]
    private RectTransform popupMenu;
    
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

        if(Input.GetMouseButtonDown(0) && isSelected)
        {
            popupMenu.anchoredPosition = Vector3.down * 2000;
        }

        // 게임형식을 좀 갖추고 세부 진행
        // 파이 맞추기 / 죽으면 뒤에꺼 보이기 / 파이 그림 찾기 / 파이 그림 연속생성 문제 풀어가면서
        // 
    }
}