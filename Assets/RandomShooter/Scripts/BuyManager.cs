using UnityEngine;
using UnityEngine.UI;

public class BuyManager : MonoBehaviour
{
    [SerializeField] private int _name;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _text;

    public bool _isFirstLevel;

    private void Awake()
    {
        _button = transform.parent.GetComponent<Button>();
        
        Transform parent = transform.parent;
        foreach (Transform child in parent)
        {
            if (child != transform)
            {
                _text = child.gameObject;
                break;
            }
        }
        
        LoadPlayerPrefs();
    }

    public void Buy()
    {
        if (GemsManager.Instance.Buy(2))
        {
            _button.interactable = true;
            _text.SetActive(true);
            gameObject.SetActive(false);
            PlayerPrefs.SetString($"buy_{_name}", "true");
            PlayerPrefs.Save();
        }
    }

    private void LoadPlayerPrefs()
    {
        string saved = PlayerPrefs.GetString($"buy_{_name}", "false");
        if (saved == "true")
        {
            _button.interactable = true;
            _text.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
