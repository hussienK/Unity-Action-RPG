/* < 8 - 15 - 2022 >
 * Hussien Kenaan
 * 
 * when collided with loads a new level
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    [SerializeField] private string[] sceneNames;

    //override OnCollide to load a new random level from an array
    protected override void OnCollide(Collider2D col)
    {
        if (col.gameObject.CompareTag("Fighter") && col.name == "Player")
        {
            string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
            GameManager.instance.SaveState();
            SceneManager.LoadScene(sceneName);
            
        }
    }
}
