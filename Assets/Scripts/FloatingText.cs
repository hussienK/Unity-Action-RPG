/* < 8 - 16 - 2022 >
 * Hussien Kenaan
 * 
 * set on a floating text object, manages all its properties + current state
 */
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    //customizable vars
    public bool active;
    public GameObject go;
    public TextMeshProUGUI txt;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    //change text state
    public void Show()
    {
        active = true;
        lastShown = Time.time;
        go.SetActive(true);
    }
    public void Hide()
    {
        active = false;
        go.SetActive(active);
    }

    //manage the current state
    public void UpdateFloatingText()
    {
        //if is hidden don't do anything
        if (!active)
            return;

        //if duration is finished hide
        if (Time.time - lastShown > duration)
            Hide();

        //move in motion if set
        go.transform.position += motion * Time.deltaTime;
    }
}
