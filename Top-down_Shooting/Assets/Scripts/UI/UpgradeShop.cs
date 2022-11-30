using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeShop : MonoBehaviour
{
    public Player player;
    public GunController gunController;
    public GameUI gameUI;
    public Button upgradeHpButton;
    public Button upgradeAmmoButton;

    public static int levelHP;
    public static int levelAmmo;
    public int costHP;
    public int costAmmo;
    int coin;

    public float ammoQuantity;

    // Start is called before the first frame update
    void Start()
    {
        levelHP = 1;
        levelAmmo = 1;
        player = FindObjectOfType<Player>();
        gunController = FindObjectOfType<GunController>();
        gameUI = FindObjectOfType<GameUI>();
        
        //ammoQuantity 
        //ScoreKeeper.
    }

    // Update is called once per frame
    void Update()
    {
        upgradeHpButton.GetComponentsInChildren<Text>()[1].text = levelHP.ToString();
        upgradeHpButton.GetComponentsInChildren<Text>()[2].text = costHP.ToString();
        upgradeAmmoButton.GetComponentsInChildren<Text>()[1].text = levelAmmo.ToString();
        upgradeAmmoButton.GetComponentsInChildren<Text>()[2].text = costAmmo.ToString();
        
        coin = PlayerPrefs.GetInt("PlayerCoin");

    }

    public void OnUpgradeMaxHP()
    {
        if (coin >= costHP)
        {
            coin -= costHP;
            player.startingHealth++;
            levelHP++;
            costHP += 2;
            PlayerPrefs.SetInt("PlayerCoin", coin);

        }
        else
            return;
        //ScoreKeeper.haveCoin
    }

    public void OnUpgradeMaxAmmo()
    {
        if (coin >= costAmmo)
        {
            coin -= costAmmo;
            gunController.maxAmmo += ammoQuantity;
            levelAmmo++;
            costAmmo += 3;
            PlayerPrefs.SetInt("PlayerCoin", coin);
        }
        else 
            return;
    }
}
