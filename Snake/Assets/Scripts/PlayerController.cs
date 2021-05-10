using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A Class for player's movement
public class PlayerController : MonoBehaviour
{
    // Prefab for snake's body parts
    public GameObject bodyPrefab;
    public GameObject GameOverPanel;

    // How often should player's movement be updated
    [Range(0.01f, 0.99f)]
    public float movementSpeed = 0.7f;

    // How much player moves on one update
    public float movementAmount = 0.3f;

    // List of body parts
    LinkedList<GameObject> bodyParts = new LinkedList<GameObject>();
    GameObject head;
    GameObject tail;
    AudioSource audioSource;

    int bodyPartCount;
    float movementUpdate;
    float defaultSpeed;
    Vector3 currentDirection;
    Vector3 newTailPosition;
    Vector3 up;
    Vector3 down;
    Vector3 left;
    Vector3 right;

    // Awake is called before Start
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        up = new Vector3(0, movementAmount, 0);
        down = new Vector3(0, -movementAmount, 0);
        left = new Vector3(-movementAmount, 0, 0);
        right = new Vector3(movementAmount, 0, 0);

        movementUpdate = 1f;
        defaultSpeed = movementSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate the first body part (snake's head)
        InstantiateBodyPart();

        // Start moving to some direction e.g. right
        currentDirection = right;
        head = bodyParts.First.Value;
        tail = bodyParts.First.Value;

        // Set new tail parts to spawn out of the screen
        newTailPosition = new Vector3(20, 20, 0);
        GameManager.onScoreChanged += Grow;
    }

    // Update is called once per frame
    void Update()
    {
        // Move every time movementUpdate time has been reached
        movementUpdate -= Time.deltaTime;
        while (movementUpdate < 0)
        {
            Move();
            movementUpdate += 1f - movementSpeed;
        }

        CheckInput();
    }

    // Instantiate new body part from bodyPrefab
    void InstantiateBodyPart()
    {
        GameObject bodyPart = Instantiate(bodyPrefab);
        bodyPart.transform.SetParent(gameObject.transform);
        bodyPart.transform.position = newTailPosition;
        bodyParts.AddFirst(bodyPart);
    }

    // Grow is subscribed to GameManagers onScoreChanged event:
    // when an apple gets eaten snake grows by one body part
    void Grow()
    {
        InstantiateBodyPart();
        bodyPartCount++;
        tail = bodyParts.First.Value;
        tail.transform.position = tail.transform.position;
    }

    // Swap snakes tail to the head to create an illusion of a 
    // forward moving snake
    void Move()
    {
        if (bodyPartCount > 0)
        {
            GameObject temp = tail;
            tail.transform.position = head.transform.position + currentDirection;
            tail = bodyParts.First.Next.Value;
            bodyParts.RemoveFirst();
            bodyParts.AddLast(temp);
            head = bodyParts.Last.Value;
        }
        else
        {
            tail.transform.position = head.transform.position + currentDirection;
        }
        audioSource.Play();
    }

    // Check player's input to change movement direction
    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentDirection == up) return;
            currentDirection = down;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentDirection == down) return;
            currentDirection = up;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentDirection == right) return;
            currentDirection = left;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentDirection == left) return;
            currentDirection = right;
        }
    }

    // Return player's normal speed
    void ReturnSpeed()
    {
        movementSpeed = defaultSpeed;
    }

    // On death just pause the game and say "GAME OVER"
    public void Die()
    {
        GameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
