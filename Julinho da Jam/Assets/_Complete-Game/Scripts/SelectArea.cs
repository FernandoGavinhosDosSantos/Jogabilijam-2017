using UnityEngine;
using System.Collections;
using UnityEngine.UI;	//Allows us to use UI.
using UnityEngine.SceneManagement;


namespace Completed
{
    //Player inherits from MovingObject, our base class for objects that can move, Enemy also inherits from this.
    public class SelectArea : MoveSelection
    {
        private Transform playerPosition;
        public static float Delay = 0;
        public int horizontal = 0;     //Used to store the horizontal move direction.
        public int vertical = 0;       //Used to store the vertical move direction.
        public int wallDamage = 1;                  //How much damage a player does to a wall when chopping it.


#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        private Vector2 touchOrigin = -Vector2.one;	//Used to store location of screen touch origin for mobile controls.
#endif

        void Start()
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
            base.Start();
        }
        void Update()
        {

            Delay += Time.deltaTime;
			if(Delay <= 0.2) return;

            if (Input.GetAxis("Confirm") > 0)
            {
                // caso area d seleçao esteja sobre player impede summon
                if (transform.position == playerPosition.position) return;

                GameObject summon = GameManager.instance.boardScript.Summon(GameManager.instance.summonId, transform.position);
                if (summon)
                {
                    Player.selecionando = false;
                    if (GameManager.instance.summonId == GameManager.IARA) GameManager.instance.IarasCharm(5, summon);
                    if (GameManager.instance.summonId != GameManager.SACI) GameManager.instance.playersTurn = false;
                    GameManager.instance.summonId = -1;
                    Destroy(gameObject);
                    GameManager.instance.isSaci = false;
                }
            }
            if (Input.GetAxis("Cancel") > 0)
            {
                GameManager.instance.activeSummons[GameManager.instance.summonId] = false;
                Player.selecionando = false;
                GameManager.instance.summonId = -1;
                Destroy(gameObject);
                GameManager.instance.isSaci = false;
            }

            //Check if we are running either in the Unity editor or in a standalone build.
#if UNITY_STANDALONE || UNITY_WEBPLAYER



            //Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
            horizontal = (int)(Input.GetAxisRaw("Horizontal"));

            //Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
            vertical = (int)(Input.GetAxisRaw("Vertical"));

            //Check if moving horizontally, if so set vertical to zero.
            if (horizontal != 0)
            {
                vertical = 0;
            }
            //Check if we are running on iOS, Android, Windows Phone 8 or Unity iPhone
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
			
			//Check if Input has registered more than zero touches
			if (Input.touchCount > 0)
			{
				//Store the first touch detected.
				Touch myTouch = Input.touches[0];
				
				//Check if the phase of that touch equals Began
				if (myTouch.phase == TouchPhase.Began)
				{
					//If so, set touchOrigin to the position of that touch
					touchOrigin = myTouch.position;
				}
				
				//If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
				else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
				{
					//Set touchEnd to equal the position of this touch
					Vector2 touchEnd = myTouch.position;
					
					//Calculate the difference between the beginning and end of the touch on the x axis.
					float x = touchEnd.x - touchOrigin.x;
					
					//Calculate the difference between the beginning and end of the touch on the y axis.
					float y = touchEnd.y - touchOrigin.y;
					
					//Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
					touchOrigin.x = -1;
					
					//Check if the difference along the x axis is greater than the difference along the y axis.
					if (Mathf.Abs(x) > Mathf.Abs(y))
						//If x is greater than zero, set horizontal to 1, otherwise set it to -1
						horizontal = x > 0 ? 1 : -1;
					else
						//If y is greater than zero, set horizontal to 1, otherwise set it to -1
						vertical = y > 0 ? 1 : -1;
				}
			}
			
#endif //End of mobile platform dependendent compilation section started above with #elif
            //Check if we have a non-zero value for horizontal or vertical
            if (horizontal != 0 || vertical != 0)
            {
                Move(horizontal, vertical);
            }
        }

    }
}