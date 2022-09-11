/* < 8 - 15 - 2022 >
 * Hussien Kenaan
 * 
 * Inherits from collidable but adds collection logic
 */
using UnityEngine;

public class Collectable : Collidable
{
    protected bool collected;

    //override on collide to add collection
    protected override void OnCollide(Collider2D col)
    {
        if (col.gameObject.CompareTag("Fighter") && col.gameObject.name == "Player")
        {
            OnCollect();
        }
    }

    //do the collection
    protected virtual void OnCollect()
    {
        collected = true;  
    }
}
