using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviour
{
private Rigidbody rb;
private int count;
private float movementX;
private float movementY;
public float speed = 0;
public TextMeshProUGUI countText;
public GameObject winTextObject;
public AudioSource[] sounds;
public GameObject explosionFX;
public GameObject pickupFX;
public GameObject victoryFX;
//public Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent <Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        sounds = GetComponents<AudioSource>();
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
             Instantiate (victoryFX, transform.position, Quaternion.identity);
        }
    }

     void FixedUpdate(){
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
}
void OnTriggerEnter(Collider other){
    //audioSource = GetComponent<AudioSource>();
    if (other.gameObject.CompareTag("PickUp")){
    other.gameObject.SetActive(false);
    playSound(0);
    var currentPickupFX = Instantiate(pickupFX, other.transform.position, Quaternion.identity);
    Destroy(currentPickupFX, 3);
}
count = count + 1;
SetCountText();
}
private void OnCollisionEnter(Collision collision){
    if (collision.gameObject.CompareTag("Enemy")){
        StartCoroutine(WaitForSoundAndDestroy(1));
         Instantiate(explosionFX, transform.position, Quaternion.identity);
        //Destroy game object
        //Destroy(gameObject);
        //Update text to display "you lose"
        winTextObject.gameObject.SetActive(true);
        winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        //Set the speed of the enemy's animation to 0
        collision.gameObject.GetComponentInChildren<Animator>().SetFloat("speed_f", 0);
    }
    //sources = GetComponents()<audioSource>();
    //sources [1].play;
    }
    
        public void playSound(int index){
        if (index >= 0 && index < sounds.Length){
            AudioSource audioSource = sounds[index];
            audioSource.Play();
        }
        else{Debug.LogWarning("invalid #");}
    }

    private IEnumerator WaitForSoundAndDestroy(int sound){
        //AudioSource audioSource = GetComponentInChildren<AudioSource>();
        if (sound >= 0 && sound < sounds.Length){
        AudioSource audioSource = sounds[sound];
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(gameObject);
    }
    }

    //public void gameOver(){
    //    if(player = null){
    //        playSound(1);
    //    }
    //}
}