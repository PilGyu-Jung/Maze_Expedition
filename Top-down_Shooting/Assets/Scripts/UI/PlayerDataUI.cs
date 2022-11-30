using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataUI : MonoBehaviour
{
    public Text coinUI;
    public Text killedUI;
    public Text objectUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinUI.text = PlayerPrefs.GetInt("PlayerCoin").ToString("D6");
        killedUI.text = PlayerPrefs.GetInt("EnemyKilled").ToString("D6");


    }
}
