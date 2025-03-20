using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class MinigameStarter : MonoBehaviour, ICursor
{
    [SerializeField] private Texture2D _cursorTexture;
    public Texture2D CursorTexture => _cursorTexture;
    [SerializeField] MinigamesManager.Minigame minigame = MinigamesManager.Minigame.Weapon;
    public void Interact()
    {
        //через event Bus?)
        ServiceLocator.Current.Get<MinigamesManager>().StartMinigame(minigame);
    }
}