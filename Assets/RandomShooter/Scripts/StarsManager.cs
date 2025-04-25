using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarsManager : MonoBehaviour
{
    public static StarsManager Instance {get; private set;}

    [SerializeField] private TextMeshProUGUI _starsCountText;

    private int starsCount;

    void Awake()
    {
        Instance = this;
        starsCount = PlayerPrefs.GetInt("stars", 0);

        if (_starsCountText != null)
        {
            _starsCountText.text = starsCount.ToString();
        }
    }

    public void AddStarsCount(int count)
    {
        starsCount += count;
        PlayerPrefs.SetInt("stars", starsCount);
        PlayerPrefs.Save();
    }

}
