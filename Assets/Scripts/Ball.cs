using UnityEngine;
using UnityEngine.UI;

class Ball : MonoBehaviour
{
    // This variable should be set in the Inspector to a game object which
    // contains a BallSpawner component.
    public BallSpawner spawner = null;
    public Text score_label = null; // The label for the score to print on
    public int score; // Score recorded during gameplay
    public Paddle paddle = null; // Paddle variable to allow for easier collision
    public float size = 1.0f; // size of the ball
    float speed = 0.2f; // speed of the ball
    protected float directionX = 1.0f; // directional speed of the ball
    protected float directionY = 0.5f;
    
    // Records tha value of paddle

      internal Paddle Paddle { get => paddle; set => paddle = value; } 

    // Set the direction of the ball, in degrees.
    public void SetDirection(int angleInDegrees)
    {
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        directionX = Mathf.Cos(angleInRadians);
        directionY = Mathf.Sin(angleInRadians);
    }

    // calculates the speed of the ball
    void Start()
    {
        speed = 0.2f / size;
    }

    // initialises the traits of the ball

    protected virtual void FixedUpdate()
    {
        Vector3 scale = new Vector3();
        scale.x = size;
        scale.y = size;
        transform.localScale = scale;
        Vector3 position = transform.position;
        position.x += speed * directionX;
        position.y += speed * directionY;
        transform.localPosition = position;
    }

   // This method allows for the ball to collide with both the paddle and the walls

    void OnCollisionEnter2D(Collision2D other)
    {

        // Allows for the destruction of the bricks

        if (other.transform.CompareTag("Brick"))
        {
            directionY = -directionY;
            Destroy(other.gameObject);
            score = score + 1;
            score_label.text = score.ToString();
        }

        // This switch statement compares the other game object name to each of the cases
        // within the switch. If the other game object name matches one of the cases then
        // all the statements underneath that case will be run, until the break statement.
        switch (other.gameObject.name)
        {
            case "Left Wall":
            case "Right Wall":
                directionX = -directionX; // Invert the direction horizontally
                break;

            case "Paddle":
            case "Top Wall":
                directionY = -directionY; // Invert the direction vertically
                break; 

            case "Bottom":
                spawner.DespawnBall(this); // despawns the current ball
                break;
        }
    }
}