/* < 8 - 16 - 2022 >
 * Hussien Kenaan
 * 
 * give the ability to attack enemie
 */
using UnityEngine;

public class PlayersWeapon : Collidable
{
    //damage struct
    public int[] damagePoint;
    public float[] pushForce;

    //updgrade
    private Animator anim;
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    //swing
    private float cooldown = 0.5f;
    private float lastSwing;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        //get input to see if should swing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D col)
    {
        //ignore npcs
        if (col.CompareTag("Fighter"))
        {
            //ignore player
            if (col.gameObject.name == "Player")
                return;

            //createa a new damage object and send it to fighter we've hit
            Damage dmg = new Damage
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };

            col.SendMessage("RecieveDamage", dmg);
        }
    }

    //do the swingin
    private void Swing()
    {
        anim.SetTrigger("Swing");
    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

    public void SetWeaponLevel(int level)
    {
        spriteRenderer.sprite = GameManager.instance.weaponSprites[level];
    }
}
