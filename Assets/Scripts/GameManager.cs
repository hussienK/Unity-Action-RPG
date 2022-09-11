/* < 8 - 16 - 2022 >
 * Hussien Kenaan
 * 
 * general game manager of the game, contains info about the player and references for easy acess 
 */
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (GameManager.instance != null)
        {
            //create a singleton
            Destroy(player.gameObject);

            //make sure no copy exists when entering new room
            Destroy(floatingTextManager.gameObject);
            Destroy(gameObject);
            Destroy(HUD);
            Destroy(Menu);
            return;
        }
        instance = this;
        //subscribe to activate loading when new scene is entered
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    //Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player player;
    public PlayersWeapon weapon;
    public FloatingManager floatingTextManager;
    public RectTransform hitpointBar;
    public GameObject HUD;
    public GameObject Menu;

    //Logic
    public int pesos;
    public int experience;


    //Upgrade Weapon
    public bool TryUpgradeWeapon()
    {
        //is the weapon max level
        if (weaponPrices.Count <= weapon.weaponLevel)
            return false;

        //has enough money
        if (pesos >= weaponPrices[weapon.weaponLevel])
        {
            pesos -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    //Save State oOrder
    /*
     * INT preferedSkin
     * INT pesos
     * INT experience
     * INT weaponLevel
     */


    #region changes
    //modify pesos from outside
    public void AddPesos(int amount)
    {
        pesos += amount;
    }
    public void RemovePesos(int amount)
    {
        pesos -= amount;
    }
    #endregion

    #region Leveling
    //Experience System
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;
        while (experience >= add)
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count) //if at max level
                return r;
        }

        return r;
    }

    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }

        return xp;
    }

    public void GrantXP(int xp)
    {
        //check if level changed before and after adding xp
        int currLevel = GetCurrentLevel();
        experience += xp;
        if (currLevel < GetCurrentLevel())
        {
            LevelUp();
        }
    }
    public void LevelUp()
    {
        player.OnLevelUp();
    }
    #endregion

    #region SaveSystem
    public void SaveState()
    {
        //create the save data
        string s = "";

        //set the save data using variables, data is split by |
        s += player.SelectedSkin + "|";
        s += pesos.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();

        //save the info using playerPrefs
        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode load)
    {
        //don't load if first time playing
        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        SceneManager.sceneLoaded -= LoadState;

        //seperate the saved string into data
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        //change player skin
        player.SwapSprite(int.Parse(data[0]));

        //assign the pesos
        pesos = int.Parse(data[1]);

        //assign the experience
        experience = int.Parse(data[2]);
        if (GetCurrentLevel() != 1)
        {
            player.SetLevel(GetCurrentLevel());
        }
        //change weapon level
        weapon.weaponLevel = int.Parse(data[3]);
        weapon.SetWeaponLevel(int.Parse(data[3]));
    }

    //on scene loaded
    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        //repositon player
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }
    #endregion


    //call the floating manager to show a text, set here for easy acess
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    //Hitpoint bar
    public void OnHitPointChange()
    {
        float ratio = (float)player.hitPoint / (float)player.maxHitPoint;
        hitpointBar.localScale = new Vector3(1, ratio, 1);
    }
}
