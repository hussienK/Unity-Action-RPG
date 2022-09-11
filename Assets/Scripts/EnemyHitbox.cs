/* < 8 - 18 - 2022 >
 * Hussien Kenaan
 * 
 * let the enemy damage player
 */
using UnityEngine;

public class EnemyHitbox : Collidable
{
    //Damage
    public int damage = 1;
    public float pushForce = 5;

    protected override void OnCollide(Collider2D col)
    {
        if (col.tag == "Fighter" && col.name == "Player")
        {
            //create a new damage object and sent it to player
            Damage dmg = new Damage
            {
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce
            };

            col.SendMessage("RecieveDamage", dmg);
        }
    }
}
