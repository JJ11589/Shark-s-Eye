using UnityEngine;
using UnityEngine.InputSystem;

public class Destroy : MonoBehaviour
{
    public GameObject Bubble;
    public bool Canpop =false;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Debug.Log(Canpop);
        
        if (Input.GetMouseButtonDown(0))
        {
            Pop();    
        }
    }
    private void Pop()
    {
        
        
        if (Canpop==true)
        {
            // Double the velocity of the parent Rigidbody
            rb.linearVelocity *= 2;
            Debug.Log("Mouse clicked");
            Bubble.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject.FindGameObjectWithTag("Score").GetComponent<Score>().IncreaseScore();
        }
    }
}
