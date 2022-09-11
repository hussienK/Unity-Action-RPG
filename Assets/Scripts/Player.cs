/* < 8 - 14 - 2022 >
 * Hussien Kenaan
 * 
 * handles the movement and collisions of player, doesn't use rigidbody as a challenge
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Mover
{
    private SpriteRenderer spriteRenderer;
    public int SelectedSkin;


    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    protected override void RecieveDamage(Damage dmg)
    {
        base.RecieveDamage(dmg);
        //change health bar
        GameManager.instance.OnHitPointChange();
    }

    private void FixedUpdate()
    {
        //get Input
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(x, y, 0));

    }

    public void SwapSprite(int skiniD)
    {
        //change the current skin
        SelectedSkin = skiniD;
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = GameManager.instance.playerSprites[SelectedSkin];
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = GameManager.instance.playerSprites[SelectedSkin];
        }
    }

    public void OnLevelUp()
    {
        //increase health and heal
        maxHitPoint++;
        hitPoint = maxHitPoint;
    }

    public void SetLevel(int level)
    {
        //for when loading to set level
        for (int i = 0; i < level; i++)
            OnLevelUp();
    }

    public void Heal(int healingAmount)
    {
        if (hitPoint == maxHitPoint)
            return;

        hitPoint += healingAmount;

        if (hitPoint > maxHitPoint)
            hitPoint = maxHitPoint;

        GameManager.instance.ShowText("+" + healingAmount.ToString() + " hp", 25, Color.green, transform.position, Vector3.up * 30f, 1.0f);
        GameManager.instance.OnHitPointChange();
    }

    protected override void Death()
    {
        base.Death();
        SceneManager.LoadScene(0);
    }
}
