using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;

public class GemsManager : MonoBehaviour
{
    public static GemsManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _gemsCountText;
    [SerializeField] private TextMeshProUGUI _buttonText;

    public int _gemsCount;

    private void Awake()
    {
        Instance = this;
        
        _gemsCount = PlayerPrefs.GetInt("gems", 0);
    }

    public bool Buy(int count)
    {
        if (_gemsCount >= count)
        {
            _gemsCount -= count;
            PlayerPrefs.SetInt("gems", _gemsCount);
            PlayerPrefs.Save();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Add(int count)
    {
        _gemsCount += count;
        PlayerPrefs.SetInt("gems", _gemsCount);
        PlayerPrefs.Save();
    }

    public void OnPurchaseComlete(Product product)
    {
        if (product.definition.id == "com.coidea.forestMatch.full")
        {
            Add(10);
        }
    }

    private void Update()
    {
        _gemsCountText.text = _gemsCount.ToString();
    }
    
    public void OnProductFetched(Product product)
    {
       _buttonText.text = product.metadata.localizedPriceString;
    }
}
