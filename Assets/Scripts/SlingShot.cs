using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class SlingShot : MonoBehaviour
{
    [Header("LineRenderer Objects", order = 1)]
    [SerializeField] private LineRenderer stripRight;
    [SerializeField] private LineRenderer stripLeft;

    [Header("Transform SlingShot", order = 2)]
    [SerializeField] private Transform stripRightPos;
    [SerializeField] private Transform stripLeftPos;
    [SerializeField] private Transform centerPosition;
    [SerializeField] private Transform idlePosition;

    [Header("Scripts", order = 3)]
    [SerializeField] private SlingShotArea slingShotArea;

    [Header("Sling Shot Stats", order = 4)]
    private bool clickedWithinArea;
    [SerializeField] private float maxLength = 3.5f;
    private Vector3 touchedPosition;


    [Header("Birds", order = 5)]
    [SerializeField] private GameObject[] birdsPrefab;
    [SerializeField] private float force = 3;
    [SerializeField] private float birdPositionOffset = 0.15f;

    private Vector2 direction;
    private Vector2 directionNormalized;

    private GameObject spawnedBird;

    private Bird currentBird;



    void Awake()
    {
        SetLinesPosition(idlePosition.position);
        SpawnBird();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SlingShotControl();
    }


    #region Sling Shot System
    private void SlingShotControl() 
    {
        // Check if the player has clicked within the sling shot area
        if (Mouse.current.leftButton.wasPressedThisFrame && slingShotArea.IsWithinSlingShotArea()) 
        {
            clickedWithinArea = true;
        }
        // Check if the player is pressing within the sling shot area
        if (Mouse.current.leftButton.isPressed && clickedWithinArea)
        {
            DrawSlingShot();
            PositionandRotateBirds();
        }
        // Check if the player release the button
        // release : thả, giải phóng
        if (Mouse.current.leftButton.wasReleasedThisFrame) 
        {
            clickedWithinArea = false;
            currentBird.LaunchBird(direction, force);
            ResetLines();
            SpawnBird();
        }
    }

    private void DrawSlingShot() 
    {
        // Get Value of Mouse Position while pressed
        // Nhận giá trị của vị trí chuột khi nhấn
        touchedPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        
        // Make touchedPosition not move outside maxLenght and plus centerPosition to get the valid location of the drag point
        // Làm cho touchPosition không di chuyển ra ngoài maxLenght và plus centerPosition để có được vị trí hợp lệ của điểm kéo
        touchedPosition = centerPosition.position + Vector3.ClampMagnitude(touchedPosition - centerPosition.position, maxLength);
        
        SetLinesPosition(touchedPosition);

        // Calculate the direction from the slingshot center to the touched position
        direction = centerPosition.position - touchedPosition;
        directionNormalized = direction.normalized;
    }

    private void SetLinesPosition(Vector2 position) 
    {
        // Set Position of Line at StartPosition while pressed by index
        // Đặt Vị trí của Dòng tại StartPosition khi được nhấn theo index
        stripRight.SetPosition(0, stripRightPos.position);
        stripLeft.SetPosition(0, stripLeftPos.position);
        stripRight.SetPosition(1, position);
        stripLeft.SetPosition(1, position);
    }

    private void ResetLines()
    {
        stripRight.SetPosition(1, idlePosition.position);
        stripLeft.SetPosition(1, idlePosition.position);
    }
    #endregion


    #region Bird Method

    private void SpawnBird() 
    {
        SetLinesPosition(idlePosition.position);

        // Calculate the position to spawn the bird with an offset
        Vector2 dir = (centerPosition.position - idlePosition.position).normalized;
        Vector2 spawnPosition = (Vector2)idlePosition.position + dir * birdPositionOffset;


        //GameObject nextBird = birdsPrefab.
        // Create a new Bird
        // Set spawnedBird = Instantiate(redBirdPrefab) to change the transform values
        spawnedBird = Instantiate(birdsPrefab[1], spawnPosition, Quaternion.identity);
        currentBird = spawnedBird.GetComponent<Bird>();
        spawnedBird.transform.right = dir;
    }

    private void PositionandRotateBirds() 
    {
        // Update the bird's position based on the touched position and direction
        spawnedBird.transform.position = (Vector2)touchedPosition + directionNormalized * birdPositionOffset;
        spawnedBird.transform.right = directionNormalized;
    }

    #endregion
}
