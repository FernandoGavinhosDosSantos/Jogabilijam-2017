using UnityEngine;
using System.Collections;

namespace Completed
{
	//Enemy inherits from MovingObject, our base class for objects that can move, Player also inherits from this.
	public class Enemy : MovingObject
	{
        protected bool leftTurned;
		public int playerDamage = 1;						//The amount of food points to subtract from the player when attacking.
		public AudioClip attackSound1;						//First of two audio clips to play when attacking the player.
		public AudioClip attackSound2;						//Second of two audio clips to play when attacking the player.
        public int charmed;
        SpriteRenderer mySpriteRenderer;

        protected int hp;
        public bool trapped;
        protected bool male;                                //bool var that says if this unit is male or not (used in Iara's hability)
        protected Animator animator;                        //Variable of type Animator to store a reference to the enemy's Animator component.
        public Transform target;                            //Transform to attempt to move toward each turn.
        protected bool skipMove;                            //Boolean to determine whether or not enemy should skip a turn or move this turn.
		
		
		//Start overrides the virtual Start function of the base class.
		protected override void Start ()
        {
            charmed = 0;
			//Register this enemy with our instance of GameManager by adding it to a list of Enemy objects. 
			//This allows the GameManager to issue movement commands.
			GameManager.instance.AddEnemyToList (this);
			
			//Get and store a reference to the attached Animator component.
			animator = GetComponent<Animator> ();
            mySpriteRenderer = GetComponent<SpriteRenderer>();

            leftTurned = (GameManager.instance.player.transform.position.x < transform.position.x);
            mySpriteRenderer.flipX = !leftTurned;

			//Find the Player GameObject using it's tag and store a reference to its transform component.
			target = GameObject.FindGameObjectWithTag ("Player").transform;
			
			//Call the start function of our base class MovingObject.
			base.Start ();
		}

        private bool IsPlayerUnderAttackRange(int xDir, int yDir)
        {
            float pX = Mathf.Round(GameManager.instance.player.transform.position.x);
            float pY = Mathf.Round(GameManager.instance.player.transform.position.y);
            float eX = Mathf.Round(transform.position.x + xDir);
            float eY = Mathf.Round(transform.position.y + yDir);

            if (pX == eX && (pY == eY + 1 || pY == eY - 1)) return true;
            if (leftTurned && pY == eY && pX == eX - 1) return true;
            if (!leftTurned && pY == eY && pX == eX + 1) return true;
            return false;
        }

        private void Attack(int damage)
        {
            if (male && charmed == 0)
            {
                //Call the LoseFood function of hitPlayer passing it playerDamage, the amount of foodpoints to be subtracted.
                GameManager.instance.player.LoseFood(damage);

                //Set the attack trigger of animator to trigger Enemy attack animation.
                animator.SetTrigger("enemyAttack");

                //Call the RandomizeSfx function of SoundManager passing in the two audio clips to choose randomly between.
                SoundManager.instance.RandomizeSfx(attackSound1, attackSound2);
            }
        }

        //Override the AttemptMove function of MovingObject to include functionality needed for Enemy to skip turns.
        //See comments in MovingObject for more on how base AttemptMove function works.
        protected override void AttemptMove<T>(int xDir, int yDir)
        {
            if (GameManager.instance.player.transform.position.x > transform.position.x)
            {
                leftTurned = false;
                mySpriteRenderer.flipX = true;
            }
            else
            {
                leftTurned = true;
                mySpriteRenderer.flipX = false;
            }

            //Check if skipMove is true, if so set it to false and skip this turn.
            if (skipMove)
            {
                skipMove = false;
                return;
            }

            //Call the AttemptMove function from MovingObject.
            base.AttemptMove<T>(xDir, yDir);

            int x = (int)Mathf.Round(transform.position.x);
            int y = (int)Mathf.Round(transform.position.y);

            if (!skipMove && !trapped && GameManager.instance.levelSettings[x, y] != 'S' && IsPlayerUnderAttackRange(xDir, yDir))
            {
                Attack(playerDamage);
            }

            //Now that Enemy has moved, set skipMove to true to skip next move.
            skipMove = true;
        }

        public void Charm(int turns, Transform newTarget)
        {
            GameManager.instance.activeSummons[GameManager.IARA] = true;

            if (this.male)
            {
                this.charmed = turns;
                this.target = newTarget;
                //inLoveSprite.activate(); **aguardando sprite**
            }
        }

        private void Uncharm()
        {
            if (male)
            {
                GameManager.instance.activeSummons[GameManager.IARA] = false;

                target = GameObject.FindGameObjectWithTag("Player").transform;
                Destroy(GameManager.instance.Iara);
                //inLoveSprite.inactivate(); **aguardando sprite**
            }
        }

        public virtual void Die()
        {
            int x = (int) Mathf.Round(transform.position.x);
            int y = (int) Mathf.Round(transform.position.y);

            GetComponent<BoxCollider2D>().enabled = false;
            GameManager.instance.levelSettings[x, y] = '_';
            animator.SetTrigger("enemyDeath");
            trapped = true;
        }

        public virtual void Damage(int dmg)
        {
            hp -= dmg;
            if (hp <= 0) Die();

            //Set the playersTurn boolean of GameManager to false now that players turn is over.
            GameManager.instance.playersTurn = false;
        }
		
		//MoveEnemy is called by the GameManger each turn to tell each Enemy to try to move towards the player.
		public void MoveEnemy ()
        {
            if (male)
            {
                if (this.charmed > 0) this.charmed--;
                else Uncharm();
            }
            if (trapped) return;

			//Declare variables for X and Y axis move directions, these range from -1 to 1.
			//These values allow us to choose between the cardinal directions: up, down, left and right.
			int xDir = 0;
			int yDir = 0;
            Vector3 destiny = transform.position;
            /*
			//If the difference in positions is approximately zero (Epsilon) do the following:
			if(Mathf.Abs (target.position.x - transform.position.x) < float.Epsilon)
				
				//If the y coordinate of the target's (player) position is greater than the y coordinate of this enemy's position set y direction 1 (to move up). If not, set it to -1 (to move down).
				yDir = target.position.y > transform.position.y ? 1 : -1;
			
			//If the difference in positions is not approximately zero (Epsilon) do the following:
			else
				//Check if target x position is greater than enemy's x position, if so set x direction to 1 (move right), if not set to -1 (move left).
				xDir = target.position.x > transform.position.x ? 1 : -1;
            */

            /*
             * if (Vector3.Distance(target.position, new Vector3(x - xDir + 1, y - yDir, 0)) < Vector3.Distance(target.position, destiny))
            {
                xDir = 1; yDir = 0; destiny = new Vector3(x - xDir + 1, y - yDir, 0);
            }
            if (Vector3.Distance(target.position, new Vector3(x - xDir, y - yDir + 1, 0)) < Vector3.Distance(target.position, destiny))
            {
                xDir = 0; yDir = 1; destiny = new Vector3(x - xDir, y - yDir + 1, 0);
            }
            if (Vector3.Distance(target.position, new Vector3(x - xDir - 1, y - yDir, 0)) < Vector3.Distance(target.position, destiny))
            {
                xDir = -1; yDir = 0; destiny = new Vector3(x - xDir - 1, y - yDir, 0);
            }
            if (Vector3.Distance(target.position, new Vector3(x - xDir, y - yDir - 1, 0)) < Vector3.Distance(target.position, destiny))
            {
                xDir = 0; yDir = -1; destiny = new Vector3(x - xDir, y - yDir - 1, 0);
            }
            */

            if (target.position.x > transform.position.x)
                xDir = 1;
            else
                xDir = -1;
            if (target.position.y > transform.position.y)
            {
                xDir = 0; yDir = 1;
            }
            else if (target.position.y < transform.position.y)
            {
                xDir = 0; yDir = -1;
            }

            int x = (int)Mathf.Round(transform.position.x + xDir);
            int y = (int)Mathf.Round(transform.position.y + yDir);

            bool newXInRange = (transform.position.x + xDir >= 0 && transform.position.x + xDir < GameManager.instance.levelSettings.GetLength(0));
            bool newYInRange = (transform.position.y + yDir >= 0 && transform.position.y + yDir < GameManager.instance.levelSettings.GetLength(1));

            //Call the AttemptMove function and pass in the generic parameter Player, because Enemy is moving and expecting to potentially encounter a Player
            if (GameManager.instance.levelSettings[x, y] == 'S')
            {
                Die();
                GameManager.instance.levelSettings[x, y] = '_';
            }
            if (!skipMove && !trapped && GameManager.instance.levelSettings[x, y] == 'P')
                OnCantMove(GameManager.instance.player);
            else if (newXInRange && newYInRange && (GameManager.instance.levelSettings[x,y] == '_' || GameManager.instance.levelSettings[x, y] == 'S'))
                AttemptMove <Player> (xDir, yDir);
        }
		
		//OnCantMove is called if Enemy attempts to move into a space occupied by a Player, it overrides the OnCantMove function of MovingObject 
		//and takes a generic parameter T which we use to pass in the component we expect to encounter, in this case Player
		protected override void OnCantMove <T> (T component)
		{
            if (charmed == 0)
            {
                //Declare hitPlayer and set it to equal the encountered component.
                Player hitPlayer = component as Player;

                //Call the LoseFood function of hitPlayer passing it playerDamage, the amount of foodpoints to be subtracted.
                hitPlayer.LoseFood(playerDamage);

                //Set the attack trigger of animator to trigger Enemy attack animation.
                animator.SetTrigger("enemyAttack");

                //Call the RandomizeSfx function of SoundManager passing in the two audio clips to choose randomly between.
                SoundManager.instance.RandomizeSfx(attackSound1, attackSound2);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag.Equals("CorpoSeco"))
            {
                Die();
                Destroy(collision.gameObject);
            }
        }
    }
}
