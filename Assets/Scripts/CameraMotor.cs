/* < 8 - 14 - 2022 >
 * Hussien Kenaan
 * 
 * moves camera to follow player
 */
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    [SerializeField] private Transform lookAt;
    [SerializeField] private float boundX = 0.15f;
    [SerializeField] private float boundY = 0.05f;

    private void Start()
    {
        lookAt = GameObject.Find("Player").transform;

    }

    private void LateUpdate()
    {
        //createa a delta vector for calculations
        Vector3 delta = Vector3.zero;

        //let player move only when outside of small bounds

        //check if not inside bounds on X-axis
        float deltaX = lookAt.position.x - transform.position.x;
        if (deltaX > boundX || deltaX < -boundX)
        {
            //move negatively 
            if (transform.position.x < lookAt.position.x)
            {
                delta.x = deltaX - boundX;
            }
            //move positively
            else
            {
                delta.x = deltaX + boundX;
            }
        }

        //check if not inside bounds on Y-axis
        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            //move negatively 
            if (transform.position.y < lookAt.position.y)
            {
                delta.y = deltaY - boundY;
            }
            //move positively
            else
            {
                delta.y = deltaY + boundY;
            }
        }

        //move the camera
        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
