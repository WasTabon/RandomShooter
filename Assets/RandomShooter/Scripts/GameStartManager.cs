using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _text;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        string isFirstStart = PlayerPrefs.GetString("start", "false");

        if (isFirstStart == "false")
        {
            _text.SetActive(false);
            _animator.SetTrigger("Start");
            PlayerPrefs.SetString("start", "true");
            PlayerPrefs.Save();
            Invoke("SetPanelInactive", 23f);
        }
        else
        {
            Invoke("SetTextInactive", 3f);
        }
    }

    private void SetPanelInactive()
    {
        _startPanel.SetActive(false);
    }

    private void SetTextInactive()
    {
        _startPanel.SetActive(false);
    }
}
