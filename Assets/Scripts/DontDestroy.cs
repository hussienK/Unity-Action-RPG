/* < 8 - 22 - 2022 >
 * Hussien Kenaan
 * 
 * set on objects that should persist between sceenes
 */
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
