using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Completed
{
    public class Sacizera : MonoBehaviour
    {
        Vector3 target;
        private Rigidbody2D rb2D;
        Transform playerReference;
        private float Delay;
        private bool chooseDirection = true;
        private int xDir;
        private int yDir;

        void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
            playerReference = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void FixedUpdate()
        {
            if(chooseDirection == true)
            {
                chosenDirection();
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
                Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, 6 * Time.deltaTime);

                //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
                rb2D.MovePosition(newPostion);

                //Recalculate the remaining distance after moving.
                sqrRemainingDistance = (transform.position - end).sqrMagnitude;

                //Return and loop until sqrRemainingDistance is close enough to zero to end the function
                yield return null;
            }
            Destroy(gameObject);
        }

        private void chosenDirection()
        {
            if (playerReference.position.y == transform.position.y)
            {
                yDir = 0;
            }
            else
            {
                //If the y coordinate of the target's (player) position is greater than the y coordinate of this enemy's position set y direction 1 (to move up). If not, set it to -1 (to move down).
                yDir = playerReference.position.y > transform.position.y ? -1 : 1;
            }

            if (playerReference.position.x == transform.position.x)
            {
                xDir = 0;
            }
            else
            {
                //Check if target x position is greater than enemy's x position, if so set x direction to 1 (move right), if not set to -1 (move left).
                xDir = playerReference.position.x > transform.position.x ? -1 : 1;
            }
            Vector3 reference = new Vector3(playerReference.position.x, playerReference.position.y);
            for (int i = 0; i < GameManager.instance.enemies.Count; i++)
            {
                GameManager.instance.enemies[i].referencial = reference;
            }
            target = new Vector3(playerReference.position.x + (4 * xDir), playerReference.position.y + (4 * yDir));
            chooseDirection = false;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag.Equals("Wall"))
            {
                Destroy(gameObject);
            }
        }
    }
}
