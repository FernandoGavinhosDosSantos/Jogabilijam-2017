using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Completed
{ 
    public abstract class MoveSelection : MonoBehaviour
    {
        private Vector2 startSaci;        
        public LayerMask HighLight;         //Layer on which collision will be checked.
        private Rigidbody2D rb2D;               //The Rigidbody2D component attached to this object.


        //Protected, virtual functions can be overridden by inheriting classes.
        protected virtual void Start()
        {

            startSaci = transform.position;


            //Get a component reference to this object's Rigidbody2D
            rb2D = GetComponent<Rigidbody2D>();
        }


        //Move returns true if it is able to move and false if not. 
        //Move takes parameters for x direction, y direction and a RaycastHit2D to check collision.
        protected bool Move(int xDir, int yDir)
        {
            //Store start position to move from, based on objects current transform position.
            float startx = transform.position.x + (xDir * 2);
            float starty = transform.position.y + (yDir * 2);
            Vector2 start = transform.position;
            Vector2 end;

            if (GameManager.instance.isSaci == true)
            {
                end = startSaci + new Vector2(xDir, yDir);
            }
            else {
                Debug.Log("itsa not me");
                end = start + new Vector2(xDir, yDir);
            }



            //Cast a line from start point to end point checking collision on blockingLayer.
            RaycastHit2D hit = Physics2D.Linecast(new Vector2(startx, starty), end, HighLight);

            //Check if anything was hit
            if (hit.transform != null)
            {
                    //Find a new position proportionally closer to the end, based on the moveTime
                    Vector2 newPostion = new Vector2(end.x, end.y);

                    //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
                    rb2D.MovePosition(newPostion);

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
    }
}