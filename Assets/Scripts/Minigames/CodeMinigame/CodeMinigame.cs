using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Minigames.CodeMinigame
{
    public class CodeMinigame : MonoBehaviour, IMinigame
    {
        [SerializeField] Button Prefab;
        [SerializeField] Transform Parent;
        [SerializeField] private string RightCode = "";
        [SerializeField] private TextMeshProUGUI CodeText; 
        private int buttonCount = 9;
        private string Code = "";

        public event Action OnGameEnded;

        public void StartGame()
        {
            SpawnButtons();
        }

        [ContextMenu("SpawnButtons")]
        private void SpawnButtons()
        {
            for (int i = 0; i < buttonCount; i++)
            {
                SpawnButton(i+1);
            }
        }

        [ContextMenu("SpawnButton")]
        private void SpawnButton(int i)
        {
            Button button =  Instantiate(Prefab, transform.position, Quaternion.identity , Parent);
            button.onClick.AddListener(new UnityAction(() => { AddCodeSymbol(i); CheckSequence(); })) ;
            button.GetComponentInChildren<TextMeshProUGUI>().text = i.ToString();
            button.gameObject.SetActive(true);
        }

        private void AddCodeSymbol(int code)
        {
            Debug.Log(code);
            Code += code;
            CodeText.text += code;
           // TurnOnLight(Code.Length);
        }

        private void CheckSequence()
        {
            if (Code.Length == RightCode.Length)
            {
                if (IsRightCode())
                    OnRightCode();
                else
                    OnWrongCode();
            }
        }

        private void OnWrongCode()
        {
            Code = "";
            CodeText.text = "";
            //playwrongSound
            //TurnOffLights();
        }

        private void OnRightCode()
        {
            CodeText.text = "RightCode!";
            DestroyChildren();
            OnGameEnded?.Invoke();
        }

        private bool IsRightCode()
        {
            return Code == RightCode;
        }


        public void StopGame()
        {
            DestroyChildren();
            gameObject.SetActive(false);
        }

        private void DestroyChildren()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        public Vector3 GetGamePositon()
        {
            return transform.position;
        }
    }
}
