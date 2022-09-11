/* < 8 - 19 - 2022 >
 * Hussien Kenaan
 *
 * Destroyable crate
 */
using UnityEngine;

public class Crate : Fighter
{
    protected override void Death()
    {
        Destroy(gameObject);
    }
}
