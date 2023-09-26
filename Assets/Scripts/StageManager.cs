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
        //... 유닛배치 초기화, 다른것들 다 초기화

        // 모두 완료후 리스트 초기화, 또 사용할 수 있게
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