using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Completed
{
    public class Sacizera : MonoBehaviour
    {
        private Rigidbody2D rb2D;
        Vector3 target;
        private float Delay;

        void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
            target = new Vector3(transform.position.x + 1, transform.position.y);
        }

        void FixedUpdate()
        {
            for (int i = 0; i < GameManager.instance.enemies.Count; i++)
            {
                //Call the MoveEnemy function of Enemy at index i in the enemies List.
                GameManager.instance.enemies[i].Saci();
            }
            Delay += Time.deltaTime;
            if (Delay <= 0.2f) return;
            StartCoroutine(SmoothMovement(target));
        }

        protected IEnumerator SmoothMovement(Vector3 end)
        {
            //Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
            //Square magnitude is used instead of magnitude because it's computationally cheaper.
            float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            //While that distance is greater than a very small amount (Epsilon, almost zero):
            while (sqrRemainingDistance > float.Epsilon)
            {
                //Find a new position proportionally closer to the end, based on the moveTime
                Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, 15 * Time.deltaTime);

                //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
                rb2D.MovePosition(newPostion);

                //Recalculate the remaining distance after moving.
                sqrRemainingDistance = (transform.position - end).sqrMagnitude;

                //Return and loop until sqrRemainingDistance is close enough to zero to end the function
                yield return null;
            }
            for (int i = 0; i < GameManager.instance.enemies.Count; i++)
            {
                //Call the MoveEnemy function of Enemy at index i in the enemies List.
                GameManager.instance.enemies[i].Saci();
            }
            Destroy(gameObject);
        }
    }
}
