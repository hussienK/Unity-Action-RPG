/* < 8 - 17 - 2022 >
 * Hussien Kenaan
 * 
 * manages the combat stats
 */
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //changable vars
    public int hitPoint = 10;
    public int maxHitPoint = 10;
    [SerializeField] protected float pushRecoverySpeed = 0.2f;

    //Imunity
    protected float immuneTime = 1.0f;
    protected float lastImune;

    //Push
    protected Vector3 pushDirection;

    //make sure all fighter can reciev damage / die
    protected virtual void RecieveDamage(Damage dmg)
    {
        if (Time.time - lastImune > immuneTime)
        {
            lastImune = Time.time;
            hitPoint -= dmg.damageAmount;
            //calculate direction from position
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.zero, 0.5f);

            if (hitPoint <= 0)
            {
                Death();
            }
        }
    }

    protected virtual void Death()
    {

    }
}
