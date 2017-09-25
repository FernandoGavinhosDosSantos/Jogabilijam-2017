using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Completed
{ 
    public abstract class MoveSelection : MonoBehaviour
    {
        public float moveTime = 0.1f;           //Time it will take object to move, in seconds.
        public LayerMask HighLight;         //Layer on which collision will be checked.

        private BoxCollider2D boxCollider;      //The BoxCollider2D component attached to this object.
        private Rigidbody2D rb2D;               //The Rigidbody2D component attached to this object.
        private float inverseMoveTime;          //Used to make movement more efficient.


        //Protected, virtual functions can be overridden by inheriting classes.
        protected virtual void Start()
        {
            //Get a component reference to this object's BoxCollider2D
            boxCollider = GetComponent<BoxCollider2D>();

            //Get a component reference to this object's Rigidbody2D
            rb2D = GetComponent<Rigidbody2D>();

            //By storing the reciprocal of the move time we can use it by multiplying instead of dividing, this is more efficient.
            inverseMoveTime = 1f / moveTime;
        }


        //Move returns true if it is able to move and false if not. 
        //Move takes parameters for x direction, y direction and a RaycastHit2D to check collision.
        protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
        {
            //Store start position to move from, based on objects current transform position.
            float startx = transform.position.x + (xDir * 4);
            float starty = transform.position.y + (yDir * 4);
            Vector2 start = transform.position;

            // Calculate end position based on the direction parameters passed in when calling Move.
            Vector2 end = start + new Vector2(xDir, yDir);

            //Cast a line from start point to end point checking collision on blockingLayer.
            hit = Physics2D.Linecast(new Vector2(startx, starty), end, HighLight);

            //Check if anything was hit
            if (hit.transform != null)
            {
                //If nothing was hit, start SmoothMovement co-routine passing in the Vector2 end as destination
                StartCoroutine(SmoothMovement(end));
                //Set the playersTurn boolean of GameManager to false now that players turn is over.
                SelectArea.Delay = 0;
                return true;
            }
            else
            {
                //Return true to say that Move was successful
                return false;
            }
        }

        //Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
        protected IEnumerator SmoothMovement(Vector3 end)
        {

                //Find a new position proportionally closer to the end, based on the moveTime
                Vector2 newPostion = new Vector2(end.x,end.y);

                //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
                rb2D.MovePosition(newPostion);

                //Return and loop until sqrRemainingDistance is close enough to zero to end the function
                yield return null;
            
        }


        //The virtual keyword means AttemptMove can be overridden by inheriting classes using the override keyword.
        //AttemptMove takes a generic parameter T to specify the type of component we expect our unit to interact with if blocked (Player for Enemies, Wall for Player).
        protected virtual void AttemptMove(int xDir, int yDir)

        {
            //Hit will store whatever our linecast hits when Move is called.
            RaycastHit2D hit;

            //Set canMove to true if Move was successful, false if failed.
            bool canMove = Move(xDir, yDir, out hit);

            //Check if nothing was hit by linecast
            if (hit.transform == null)
                //If nothing was hit, return and don't execute further code.
                return;

        }

    }
}