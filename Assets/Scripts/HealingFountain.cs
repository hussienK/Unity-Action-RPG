/* < 8 - 19 - 2022 >
 * Hussien Kenaan
 *
 * heals the player when colliding
*/
using UnityEngine;

public class HealingFountain : Collidable
{
    [SerializeField] private int healingAmount = 1;

    private float healingCooldown = 1.0f;
    private float lastheal;

    protected override void OnCollide(Collider2D col)
    {
        if (col.name != "Player")
            return;

        if (Time.time - lastheal > healingCooldown)
        {
            lastheal = Time.time;
            GameManager.instance.player.Heal(healingAmount);
        }
    }
}
