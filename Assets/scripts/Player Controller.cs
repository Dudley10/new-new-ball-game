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
public AudioSource audioSource;
public AudioClip[] sounds;
//public Transform player;

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
        if (count >=16)
        {
            winTextObject.SetActive(true);
             Destroy(GameObject.FindGameObjectWithTag("Enemy"));
             playSound(2);
        }
    }

     void FixedUpdate(){
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
}
void OnTriggerEnter(Collider other){
    audioSource = GetComponent<AudioSource>();
    if (other.gameObject.CompareTag("PickUp")){
    other.gameObject.SetActive(false);
    audioSource.Play();
}
count = count + 1;
SetCountText();
}
private void OnCollisionEnter(Collision collision){
    if (collision.gameObject.CompareTag("Enemy")){
        playSound(1);
        //Destroy game object
        Destroy(gameObject);
        //Update text to display "you lose"
        winTextObject.gameObject.SetActive(true);
        winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
    }
    //sources = GetComponents()<audioSource>();
    //sources [1].play;
    }
    
        public void playSound(int index){
        if (index >= 0 && index < sounds.Length){
            audioSource.clip = sounds[index];
            audioSource.Play();
        }
        else{Debug.LogWarning("invalid #");}
    }
    //public void gameOver(){
    //    if(player = null){
    //        playSound(1);
    //    }
    //}
}