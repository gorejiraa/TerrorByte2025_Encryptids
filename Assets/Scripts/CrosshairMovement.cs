using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings;

public class CrosshairMovement : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    private float timer = 0f;
    private Vector3 centerPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        centerPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        //CrosshairIsOverOpponent();
        FigureEight(80f);

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            if (CrosshairIsOverOpponent()) 
            {
                Debug.Log("YOU SHOT HIM!");
            }
        }
    }

    void FigureEight(float intensity) 
    {
        float speed = 2.0f;
        float radiusX = 2.0f * intensity; // Radius of the loop along X-axis
        float radiusY = 2.0f * intensity; // Radius of the loop along Y-axis (for the '8' shape)

        timer += Time.deltaTime * speed;

        // Calculate X and Y positions using sine and cosine
        float x = Mathf.Cos(timer) * radiusX;
        float y = Mathf.Sin(timer * 2) * radiusY; // Double the frequency for Y to create the '8'

        Vector3 targetPosition = new Vector3(x, y);

        // Set the object's position
        transform.position = Vector3.MoveTowards(transform.position, centerPosition + targetPosition, 10f);
    }

    public bool CrosshairIsOverOpponent() 
    {
        Ray ray = Camera.main.ScreenPointToRay(transform.position);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        //RaycastHit hit;
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        if (hit.collider != null)
        {
            Debug.Log("Hit:" + hit.collider.name);
            string hitTag = hit.collider.gameObject.tag;

            if (hitTag == "Opponent")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else 
        {
            return false;
        }
    }

    void PlayerInput() 
    {
        int xMove = 0;
        int yMove = 0;

        if (Input.GetKey(KeyCode.RightArrow)) xMove += 1;
        if (Input.GetKey(KeyCode.LeftArrow)) xMove -= 1;
        if (Input.GetKey(KeyCode.DownArrow)) yMove -= 1;
        if (Input.GetKey(KeyCode.UpArrow)) yMove += 1;

        transform.Translate(new Vector3(xMove * moveSpeed, yMove * moveSpeed));
    }
}
