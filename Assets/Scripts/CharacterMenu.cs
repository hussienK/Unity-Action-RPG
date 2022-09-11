/* < 8 - 18 - 2022 >
 * Hussien Kenaan
 * 
 * Manage the Menu that allows for upgrading weapons, character skin selection, and leveling
 */
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterMenu : MonoBehaviour
{
    //Text fields
    [SerializeField] private TextMeshProUGUI levelText, hitpointText, pesosText, upgradeCostText, xpText;

    //Logic
    private int currentCharacterSelection = 0;
    [SerializeField] private Image characterSelectionSprite;
    [SerializeField] private Image weaponSprite;
    [SerializeField] private RectTransform xpBar;   

    //Character selection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;

            //if went too far away
            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0;
        }
        else
        {
            currentCharacterSelection--;

            //if went too far away
            if (currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
        }
  
        OnSelectionChange();
    }
    private void OnSelectionChange()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    //Weapon Upgrade
    public void OnUpgradeWeapon()
    {
        if (GameManager.instance.TryUpgradeWeapon())
        {
            UpdateMenu();
        }
    }

    //Update character information
    public void UpdateMenu()
    {
        //weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
        {
            upgradeCostText.text = "Max";
        }
        else
        {
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        }

        //meta
        hitpointText.text = GameManager.instance.player.hitPoint.ToString() + " / " + GameManager.instance.player.maxHitPoint.ToString();
        pesosText.text = GameManager.instance.pesos.ToString();
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();

        //XP Bar
        int currentLevel = GameManager.instance.GetCurrentLevel();
        if (currentLevel == GameManager.instance.xpTable.Count) // if at max level
        {
            xpText.text = GameManager.instance.experience.ToString() + " total experience points";
        }
        else
        {
            //calculate ratio and apply
            int PrevLevelXp = GameManager.instance.GetXpToLevel(currentLevel - 1);
            int currentLevelXp = GameManager.instance.GetXpToLevel(currentLevel);

            int diff = currentLevelXp - PrevLevelXp;
            int currentXpIntoLevel = GameManager.instance.experience - PrevLevelXp;
            float completionRatio = (float)currentXpIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currentXpIntoLevel.ToString() + " / " + diff;
        }

        //Player Sprite
        currentCharacterSelection = GameManager.instance.player.SelectedSkin;
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
    }
}
