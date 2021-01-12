using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class defines a component which can spawn new game objects by creating copies
// of a template game object which already exists in the scene.
class BallSpawner : MonoBehaviour
{
    // This variable should be set in the Inspector to an inactive Text object containing
    // the text to display when the game is over.
    public Text gameOver = null;

    // This variable should be set in the Inspector to an existing ball object within
    // the scene. The template object can, and probably should be an inactive object.
    public Ball ballTemplate = null;

    // The total number of balls to spawn when the game is started. This value can be
    // overridden in the Inspector.
    public int totalBalls = 3;

    //The variable responsible for a ball coming back after falling out of bounds
    public bool respawn;

    //The variable responsible for the amount of times you can let a ball fall down
    public int lives = 3;

    //Ensures that none of the balls spawn in at the same time
    public int count = 0;

    //Allows for the position of the spawner to change according to the paddle
    public Transform position;

    // List to keep track of all balls spawned by this script.
    List<Ball> ballList = new List<Ball>();

    // This method is usually run by Unity when the object is created (when the game
    // is first run), but also whenever the object is activated after being inactive.

    private void Start()
    {
        Respawn();
        transform.position = position.position;
    }
    
    // the section which causes the balls to come back within certain parameters.
    void Respawn()
    {
        respawn = false;
        // if the respawn variable is false, it will check to see that the count variable
        // is still within the boundaries before calling on the SpawnBall section and setting the respawn
        // variable to true
        if (!respawn)
        {
            if (count < 3)
            {
                respawn = true;
                SpawnBall();                
            }
        }
        
    }

    // This method spawns a new ball with a random size and initial direction.
    void SpawnBall()
    {
        // Clone the template game object
        Ball ballClone = Instantiate(ballTemplate);

        // Move the new ball clone to the ball spawner's position
        ballClone.transform.position = transform.position;

        // Set the size of the ball clone to a random number
        ballClone.size = 1.0f;

        // Generate a random direction for the ball clone
        int angle = Random.Range(0, 180);
        if (Random.Range(0, 180) < 50)
        {
            angle += 90;
        }
        ballClone.SetDirection(angle);

        // Activate the new ball clone
        ballClone.gameObject.SetActive(true);

        // Add the new ball clone to the list of balls
        
            ballList.Add(ballClone);
    }

    public void DespawnBall(Ball ballToDespawn)
    {
        // Destroy the ball game object
        Destroy(ballToDespawn.gameObject);
        // Set respawn to false to allow another ball to spawn if allowed
        respawn = false;
        // Lives go down by one unit
        lives = lives - 1;
        // Count goes up by one unit
        count = count + 1;
        // Calls on the respawn section again
        Respawn();

        // Show the game over text if there are no balls left in the list
        if (lives == 0)
        {
            // calls oin the game over object to signify that the game is done
            gameOver.gameObject.SetActive(true);
        }
    }
}
