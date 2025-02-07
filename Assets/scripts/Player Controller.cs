using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
private Rigidbody rb;
private int count;
private float movementX;
private float movementY;
public float speed = 0;
public TextMeshProUGUI countText;
public GameObject winTextObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent <Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }
    void OnMove(InputValue movementValue) {
        //Function body
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText(){
        countText.text = "Count: " + count.ToString();
        if (count >=27)
        {
            winTextObject.SetActive(true);
             Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

     void FixedUpdate(){
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
}
void OnTriggerEnter(Collider other){
    if (other.gameObject.CompareTag("PickUp")){
    other.gameObject.SetActive(false);
}
count = count + 1;
SetCountText();
}
private void OnCollisionEnter(Collision collision){
    if (collision.gameObject.CompareTag("Enemy")){
        //Destroy game object
        Destroy(gameObject);
        //Update text to display "you lose"
        winTextObject.gameObject.SetActive(true);
        winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
    }
}
}