using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterTitles : MonoBehaviour
{
    public int sceneIndex; // ������ �����, ������� ����� ���������

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //|| Input.GetKeyDown(KeyCode.Space)
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
