using UnityEngine;
using System;
using System.Collections.Generic; 		//Allows us to use Lists.
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.

namespace Completed
	
{
	
	public class BoardManager : MonoBehaviour
	{
		// Using Serializable allows us to embed a class with sub properties in the inspector.
		[Serializable]
		public class Count
		{
			public int minimum; 			//Minimum value for our Count class.
			public int maximum; 			//Maximum value for our Count class.
			
			
			//Assignment constructor.
			public Count (int min, int max)
			{
				minimum = min;
				maximum = max;
			}
		}

        public GameObject Highlight;
        //public int columns = 3; 										//Number of columns in our game board.
		//public int rows = 3;											//Number of rows in our game board.
		public Count wallCount = new Count (5, 9);						//Lower and upper limit for our random number of walls per level.
		public Count foodCount = new Count (1, 5);						//Lower and upper limit for our random number of food items per level.
		public GameObject[] exit;										//Prefab to spawn for exit.
		public GameObject[] floorTiles;									//Array of floor prefabs.
		public GameObject[] wallTiles;									//Array of wall prefabs.
		public GameObject[] foodTiles;									//Array of food prefabs.
		public GameObject[] enemyTiles;									//Array of enemy prefabs.
		public GameObject[] outerWallTiles;								//Array of outer tile prefabs.
        public GameObject[] SummonTiles;
        public GameObject[] SpecialTiles;

        private int exitType;
		private Transform boardHolder;									//A variable to store a reference to the transform of our Board object.
		private List <Vector3> gridPositions = new List <Vector3> ();	//A list of possible locations to place tiles.
		
		
		//Clears our list gridPositions and prepares it to generate a new board.
		void InitialiseList ()
		{
			//Clear our list gridPositions.
			gridPositions.Clear ();
			
			//Loop through x axis (columns).
			for(int x = 0; x < GameManager.instance.columns; x++)
			{
				//Within each column, loop through y axis (rows).
				for(int y = 0; y < GameManager.instance.rows; y++)
				{
					//At each index add a new Vector3 to our list with the x and y coordinates of that position.
					gridPositions.Add (new Vector3(x, y, 0f));
				}
			}
		}
		
		//Sets up the outer walls and floor (background) of the game board.
		void BoardSetup ()
		{
			//Instantiate Board and set boardHolder to its transform.
			boardHolder = new GameObject ("Board").transform;

			//Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
			for(int x = -1; x < GameManager.instance.columns + 1; x++)
			{
				//Loop along y axis, starting from -1 to place floor or outerwall tiles.
				for(int y = -1; y < GameManager.instance.rows + 1; y++)
				{
					//Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
					GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
					
					//Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
					if(x == -1 || x == GameManager.instance.columns || y == -1 || y == GameManager.instance.rows)
						toInstantiate = outerWallTiles [Random.Range (0, outerWallTiles.Length)];
					
					//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
					GameObject instance = Instantiate (toInstantiate, new Vector3 (x, y, 0f), Quaternion.identity) as GameObject;
					
					//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
					instance.transform.SetParent (boardHolder);
				}
			}
		}

        public GameObject Summon(int summonId, Vector3 position)
        {
            int x = (int) Mathf.Round(position.x);
            int y = (int) Mathf.Round(position.y);
            char[,] levelSetup = GameManager.instance.levelSettings;

            bool xInRange = (x >= 0 && x < levelSetup.GetLength(0));
            bool yInRange = (y >= 0 && y < levelSetup.GetLength(1));

            if (GameManager.instance.summonId == GameManager.BOITATA)
            {
                if (xInRange && yInRange && (levelSetup[x, y] == 'L' || levelSetup[x, y] == 'A' || levelSetup[x, y] == 'T'))
                    return Instantiate(SummonTiles[summonId], position, Quaternion.identity);

                return null;
            }

            if (xInRange && (yInRange && levelSetup[x,y] == '_' || summonId == GameManager.BOITATA))
                    return Instantiate(SummonTiles[summonId], position, Quaternion.identity);

            return null;
        }
		
		
		//RandomPosition returns a random position from our list gridPositions.
		Vector3 RandomPosition (bool enemy)
		{
			//Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
			int randomIndex = Random.Range (0, gridPositions.Count);
			
			//Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
			Vector3 randomPosition = gridPositions[randomIndex];
			
			//Remove the entry at randomIndex from the list so that it can't be re-used.
			if (!enemy) gridPositions.RemoveAt (randomIndex);
			
			//Return the randomly selected Vector3 position.
			return randomPosition;
		}
		
		
		//LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
		void LayoutObjectAtRandom (GameObject[] tileArray, int minimum, int maximum, bool enemy)
		{
			//Choose a random number of objects to instantiate within the minimum and maximum limits
			int objectCount = Random.Range (minimum, maximum+1);
			
			//Instantiate objects until the randomly chosen limit objectCount is reached
			for(int i = 0; i < objectCount; i++)
			{
				//Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
				Vector3 randomPosition = RandomPosition(enemy);
				
				//Choose a random tile from tileArray and assign it to tileChoice
				GameObject tileChoice = tileArray[Random.Range (0, tileArray.Length)];
				
				//Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
				Instantiate(tileChoice, randomPosition, Quaternion.identity);
			}
        }

        public void SetCharacter(int[] character)
        {
            int qtdMana = 4;

            GameManager.instance.player.mana = character[qtdMana];
            GameManager.instance.player.DrawMana();

            GameManager.instance.player.unlockedSummons[GameManager.SACI] = (character[GameManager.SACI] == 1);
            GameManager.instance.player.unlockedSummons[GameManager.CORPO_SECO] = (character[GameManager.CORPO_SECO] == 1);
            GameManager.instance.player.unlockedSummons[GameManager.BOITATA] = (character[GameManager.BOITATA] == 1);
            GameManager.instance.player.unlockedSummons[GameManager.IARA] = (character[GameManager.IARA] == 1);

            exitType = character[GameManager.SACI] + character[GameManager.CORPO_SECO] + character[GameManager.BOITATA] + character[GameManager.IARA];
        }

        public void SetCamera(float[] camera)
        {
            Vector3 pos = new Vector3(camera[0], camera[1], camera[2]);
            GameManager.instance.mainCamera.transform.position = pos;
            GameManager.instance.mainCamera.orthographicSize = camera[3];
        }

        public void BuildLevel(char[,] board)
        {
            GameManager.instance.levelSettings = new char[board.GetLength(0), board.GetLength(1)];

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    GameManager.instance.levelSettings[i, j] = board[i, j];
                }
            }

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    switch (board[i, j])
                    {
                        case 'P':
                            GameManager.instance.player.transform.position = new Vector3(i, j, 0);
                            break;
                        case '_':
                            break;
                        case 'T':
                            Instantiate(enemyTiles[0], new Vector3(i, j, 0), Quaternion.identity);
                            break;
                        case 'L':
                            Instantiate(enemyTiles[1], new Vector3(i, j, 0), Quaternion.identity);
                            break;
                        case 'A':
                            Instantiate(enemyTiles[2], new Vector3(i, j, 0), Quaternion.identity);
                            break;
                        case 'W':
                            Instantiate(wallTiles[Random.Range(0, wallTiles.Length)], new Vector3(i, j, 0), Quaternion.identity);
                            break;
                        case 'F':
                            Instantiate(exit[exitType], new Vector3(i, j, 0), Quaternion.identity);
                            break;
                        case 's':
                            Instantiate(SpecialTiles[0], new Vector3(i, j, 0), Quaternion.identity);
                            break;
                        case 'r':
                            Instantiate(SpecialTiles[1], new Vector3(i, j, 0), Quaternion.identity);
                            break;
                        case 'c':
                            Instantiate(SpecialTiles[2], new Vector3(i, j, 0), Quaternion.identity);
                            break;
                        case 't':
                            Instantiate(SpecialTiles[3], new Vector3(i, j, 0), Quaternion.identity);
                            break;
                        case 'b':
                            Instantiate(SpecialTiles[4], new Vector3(i, j, 0), Quaternion.identity);
                            break;
                        case 'f':
                            Instantiate(SpecialTiles[5], new Vector3(i, j, 0), Quaternion.identity);
                            break;
                        case 'i':
                            Instantiate(SpecialTiles[6], new Vector3(i, j, 0), Quaternion.identity);
                            break;
                        case 'g':
                            Instantiate(SpecialTiles[7], new Vector3(i, j, 0), Quaternion.identity);
                            break;
                    }
                }
            }
        }

        //SetupScene initializes our level and calls the previous functions to lay out the game board
        public void SetupScene (int level)
		{
            int[] boardSize = Maps.BoardSettings(GameManager.instance.level);

            GameManager.instance.columns = boardSize[0];
            GameManager.instance.rows = boardSize[1];

			//Creates the outer walls and floor.
			BoardSetup();

            /*
			//Creates the outer walls and floor.
			BoardSetup ();
			
			//Reset our list of gridpositions.
			InitialiseList ();
			
			//Instantiate a random number of wall tiles based on minimum and maximum, at randomized positions.
			LayoutObjectAtRandom (wallTiles, wallCount.minimum, wallCount.maximum, false);
			
			//Instantiate a random number of food tiles based on minimum and maximum, at randomized positions.
			//LayoutObjectAtRandom (foodTiles, foodCount.minimum, foodCount.maximum);
			
			//Determine number of enemies based on current level number, based on a logarithmic progression
			int enemyCount = (int)Mathf.Log(level, 2f);
			
			//Instantiate a random number of enemies based on minimum and maximum, at randomized positions.
			LayoutObjectAtRandom (enemyTiles, enemyCount, enemyCount, true);
			
			//Instantiate the exit tile in the upper right hand corner of our game board
			Instantiate (exit, new Vector3 (GameManager.instance.columns - 1, GameManager.instance.rows - 1, 0f), Quaternion.identity);
            */
        }
	}
}
