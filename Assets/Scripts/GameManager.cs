using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);

    }

    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    // References
    public PlayerMovement player;
    // Public weapon weapon

    // Logic
    public int gold;
    public int experience;

    /*
     * Fields to save between scenes
     * 
     INT preferedSkin
     INT pesos
     INT experience
     INT weaponLevel
    */


    // Save state
    public void SaveState()
    {
        string s = "";

        s += "0" + "|";
        s += gold + "|";
        s += experience.ToString() + "|";
        s += "0";

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');


        //Change Player Skin
        gold = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        // Change Weapon level


        Debug.Log("Load State");
    }
}
