using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitSelector : MonoBehaviour
{
    public Unit unit { get; private set; }

    private Highlighter hl;
    private RaycastHit hit;

    private void Start()
    {
        InitVariables();
    }

    private void Update()
    {
        MouseClickToSelectUnit();
    }

    private void InitVariables()
    {
        hl = GetComponent<Highlighter>();
    }

    private void MouseClickToSelectUnit()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Unit")))
            {

                // 이전 선택된 것 변수 제거
                if (unit != null)
                {
                    unit.isSelected = false;
                    //print(unit + "<color=#00FF22> is unselected</color>");

                    unit.OnDeselected();
                    unit.RemoveMoveRange();
                    unit.SkillImageChangeToBlank();
                }

                unit = hit.collider.GetComponent<Unit>();
                unit.isSelected = true;
                //print(unit + "<color=cyan> is selected</color>");

                unit.OnSelected();
                unit.DrawMoveRange();
                unit.SkillImageChange();

            } else
            {
                if (unit != null)
                {
                    unit.isSelected = false;
                    //print(unit + "<color=#00FF22> is unselected</color>");

                    unit.OnDeselected();
                    unit.RemoveMoveRange();
                    unit.SkillImageChangeToBlank();
                }
            }
        }
    }
}
