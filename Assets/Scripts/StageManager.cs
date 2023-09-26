using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    private HashSet<GameObject> inactiveArea;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;

            InitVariables();

            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void InitVariables()
    {
        inactiveArea = new HashSet<GameObject>();
    }

    public void NextStage()
    {
        InactiveAreaToActiveArea();
        //pi.Next();
        //... ���ֹ�ġ �ʱ�ȭ, �ٸ��͵� �� �ʱ�ȭ

        // ��� �Ϸ��� ����Ʈ �ʱ�ȭ, �� ����� �� �ְ�
        inactiveArea.Clear();
    }

    public void AddInactiveArea(GameObject o)
    {
        this.inactiveArea.Add(o);
    }

    public void InactiveAreaToActiveArea()
    {
        foreach(GameObject o in this.inactiveArea)
        {
            o.SetActive(true);
        }
    }
}