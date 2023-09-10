using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Calculation;

public class Player : Object, ICharacter
{
    [SerializeField]
    private Grid grid;
    public Vector3Int currentPos { get; private set; }

    private void Start()
    {
        InitVariables();
    }

    private void Update()
    {

    }

    private void InitVariables()
    {

    }

    public Vector2Int GetCurrentPosition()
    {
        currentPos = grid.WorldToCell(transform.position);

        //print(Calc.Vector3to2Int(currentPos));

        return Calc.Vector3to2Int(currentPos);
    }

    /* SKills */

    private void SummonCommonoMinion1()
    {

    }

    private void SummonCommonoMinion2()
    {

    }

    private void SummonUncommonMinion1()
    {

    }

    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    public void SkillQ()
    {
        throw new System.NotImplementedException();
    }

    public void SkillW()
    {
        throw new System.NotImplementedException();
    }

    public void SkillE()
    {
        throw new System.NotImplementedException();
    }

    public void SkillUltR()
    {
        throw new System.NotImplementedException();
    }
}