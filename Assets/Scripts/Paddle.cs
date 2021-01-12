using UnityEngine;
using UnityEngine.UI;

class Paddle : MonoBehaviour
{
    public float speed = 0.2f; // speed of the apddle
    float direction = 0.0f; // variable for changing directions of the paddle
    

    public KeyCode moveLeftKey = KeyCode.LeftArrow; // controls for the paddle
    public KeyCode moveRightKey = KeyCode.RightArrow;
    bool canMoveLeft = true; // collision detection
    bool canMoveRight = true;

   

    void FixedUpdate()
    {
        // initial values for the paddle 

        Vector3 position = transform.localPosition; 
        position.x += speed * direction;
        transform.localPosition = position;
    }
    void Update()

    {   // input detection
        bool isLeftPressed = Input.GetKey(moveLeftKey); 
        bool isRightPressed = Input.GetKey(moveRightKey);

        // if statements check for whether or not you are colliding with a wall
        // and allows the paddle to move if not
        if (isLeftPressed && canMoveLeft)
        {
            direction = -1.0f;
        }
        else if (isRightPressed && canMoveRight)
        {
            direction = 1.0f;
        }
        else
        {
            direction = 0.0f;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        // switch statements stop movemtnet if colliding with walls
    
        switch (other.gameObject.name)
        {
            case "Left Wall":
                canMoveLeft = false;
                break;

            case "Right Wall":
                canMoveRight = false;
                break;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {

        // switch statements allow movement if not colliding with walls
        switch (other.gameObject.name)
        {
            case "Left Wall":
                canMoveLeft = true;
                break;

            case "Right Wall":
                canMoveRight = true;
                break;
        }
    }
}
