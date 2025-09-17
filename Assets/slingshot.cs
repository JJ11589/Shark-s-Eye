using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [Header("Line Renderers")]
    [SerializeField] private LineRenderer[] lineRenderers;

    [Header("Strip Positions")]
    [SerializeField] private Transform[] stripPosition;
    [SerializeField] private Transform centre;
    [SerializeField] private Transform idlePosition;

    [Header("Slingshot Settings")]
    [SerializeField] private float maxLength = 3f;
    [SerializeField] private float bottomBoundary = -3f;
    [SerializeField] private float force = 20f;
    [SerializeField] private float birdPositionOffset = 0.5f;

    [Header("Bird Settings")]
    [SerializeField] private GameObject birdPrefab;

    [Header("Clone Settings")]
    //[SerializeField] private GameObject clonePrefab;

    private Vector3 currentPosition;
    private Rigidbody2D bird;
    private Collider2D birdCollider;

    private bool isMouseDown;
    private Destroy destroy;
    public AudioManager audioManager;   


    private void Start()
    {
        InitializeLineRenderers();
        CreateBird();
    }

    private void Update()
    {
        if (isMouseDown)
        {
            DraggingSlingshot();
            
        }
        else
        {
            ResetStripsToIdle();
        }
    }

    private void OnMouseDown()
    {
        isMouseDown = true;
        audioManager.PlayStrech();
    }

    private void OnMouseUp()
    {
        isMouseDown = false;
        ShootBird();
    }

    #region Slingshot Logic

    private void InitializeLineRenderers()
    {
        foreach (LineRenderer line in lineRenderers)
        {
            if (line != null)
            {
                line.positionCount = 2;
            }
        }

        lineRenderers[0].SetPosition(0, stripPosition[0].position);
        lineRenderers[1].SetPosition(0, stripPosition[1].position);
    }

    private void DraggingSlingshot()
    {
        if (Camera.main == null) return;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10; // Depth to screen world
        currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        

        // Clamp position within limits
        currentPosition = centre.position + Vector3.ClampMagnitude(currentPosition - centre.position, maxLength);
        currentPosition = ClampBoundary(currentPosition);

        UpdateSlingshotVisual(currentPosition);
    }

    private Vector3 ClampBoundary(Vector3 position)
    {
        position.y = Mathf.Clamp(position.y, bottomBoundary, centre.position.y + maxLength);
        return position;
    }

    private void ResetStripsToIdle()
    {
        currentPosition = idlePosition.position;
        UpdateSlingshotVisual(currentPosition);
    }

    private void UpdateSlingshotVisual(Vector3 position)
    {
        if (lineRenderers[0] != null && lineRenderers[1] != null)
        {
            lineRenderers[0].SetPosition(1, position);
            lineRenderers[1].SetPosition(1, position);
        }

        if (bird != null)
        {
            Vector3 direction = position - centre.position;
            bird.transform.position = position + direction.normalized * birdPositionOffset;
            bird.transform.right = -direction.normalized;
        }
    }

    private void ShootBird()
    {
        if (bird == null) return;


        bird.bodyType = RigidbodyType2D.Dynamic;
        Vector3 launchForce = (currentPosition - centre.position) * force * -1;
        bird.linearVelocity = launchForce;
        audioManager.PlayRelease();

        birdCollider.enabled = true;

        // Automatically destroy bird after 3 seconds of release
        Destroy(bird.gameObject, 3f);
        destroy.Canpop = true;

        bird = null;
        birdCollider = null;
        Invoke(nameof(CreateBird), 1f); // Delay next bird creation
    }

    #endregion

    #region Bird Management

    private void CreateBird()
    {
        GameObject birdInstance = Instantiate(birdPrefab);
        bird = birdInstance.GetComponent<Rigidbody2D>();
        destroy= birdInstance.GetComponent <Destroy>();
        birdCollider = birdInstance.GetComponent<Collider2D>();

        if (bird != null)
        {
            bird.bodyType = RigidbodyType2D.Kinematic;
        }

        if (birdCollider != null)
        {
            birdCollider.enabled = false;
        }

        ResetStripsToIdle();
    }


    #endregion
}