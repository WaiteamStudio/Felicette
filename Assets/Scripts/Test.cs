using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour //������ ��� ������������ �������� ��� ������� ����
{
    public GameObject[] game; 
    private void Start()
    {
        for (int i = 0; i < game.Length; i++)
        {
            game[i].SetActive(false);
            game[i].SetActive(true);
        }
    }
}
