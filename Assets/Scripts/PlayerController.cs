using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 50.0f;
    //istance variable to store the CharacterController
    private CharacterController characterController;
    //reference to the connected head Rigidbody
    public Rigidbody head;
    //lets us indicate what layers the ray should hit. 
    public LayerMask layerMask;
    //where we want the marine to stare.
    //we set value to zero at the start of the game.
    private Vector3 currentLookTarget = Vector3.zero;
    //
    public Animator bodyAnimator;
    //This provides an array of force values for the camera.
    public float[] hitForce;
    //timeBetweenHits is the grace period after the hero sustains damage.
    public float timeBetweenHits = 2.5f;
    //isHit is a flag that indicates the hero took a hit.
    private bool isHit = false;
    //timeSinceHit tracks of the amount of time in the grace period.
    private float timeSinceHit = 0;
    // references the number of times the hero took a hit. 
    // It’s also used to get the shake intensity for the camera shake.
    private int hitNumber = -1;
    //marineBody is the marine’s body. 
    public Rigidbody marineBody;
    // isDead keeps track of the player’s current death state.
    private bool isDead = false;

    //
    public void Die()
    {
        //You set IsMoving to false since the marine is dead
        bodyAnimator.SetBool("IsMoving", false);
        //We set the parent to null to remove the current GameObject from its parent.
        marineBody.transform.parent = null;
        //by enabling Use Gravity and disabling IsKinematic, the body will drop and roll, and we enable a collider to make this all work.
        marineBody.isKinematic = false;
        marineBody.useGravity = true;
        marineBody.gameObject.GetComponent<CapsuleCollider>().enabled = true;
        // Disabling the gun prevents the player from firing after death.
        marineBody.gameObject.GetComponent<Gun>().enabled = false;

        //First, this destroys the joint to release the head from the body. 
        //Then, like the body, we remove the parent and enable gravity.
        //Finally, we destroy the current GameObject while playing the death sound.
        Destroy(head.gameObject.GetComponent<HingeJoint>());
        head.transform.parent = null;
        head.useGravity = true;
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.marineDeath);
        Destroy(gameObject);
    }


    void OnTriggerEnter(Collider other)
    {
        Alien alien = other.gameObject.GetComponent<Alien>();
        if (alien != null)
        { // 1 First, you check if the colliding object has an Alien script attached to it
            // 1  If it’s an alien and the player hasn’t been hit, then the player is officially considered hit.
            if (!isHit)
            {
                hitNumber += 1; // 2 The hitNumber increases by one, after which you get a reference to CameraShake().
                CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();
                if (hitNumber < hitForce.Length) // 3 If the current hitNumber is less than the number of force values for the camera shake, then the hero is still alive.
                {
                    cameraShake.intensity = hitForce[hitNumber];
                    cameraShake.Shake();
                }
                else
                {
                    Die();
                }
                isHit = true; // 4 This sets isHit to true, plays the grunt sound and kills the alien.
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.hurt);
            }
            alien.Die();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        //GetComponent() gets a reference to current component passed into the script.
        characterController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {

        //creates a new Vector3 to store the movement direction 
        /*SimpleMove() is a built-in method that automatically moves the character in the given direction, but
          not allowing the character to move through obstacles.*/
         Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"),
          0, Input.GetAxis("Vertical"));
        characterController.SimpleMove(moveDirection * moveSpeed);

        //Get current position of the GameObject and stores it in variable pos.
        //Vector3 = x, y and z of a GameObject.
        //Vector3 pos = transform.position;

        //a or left key pressed = -1(left) and d or right key pressed = 1(right)
        //pos.x += moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        //s or down key pressed = -1(back) and w or up key pressed = 1(forward)
        // pos.z += moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;

        //updates SpaceMarine GameObject's position with the new position. 
        //transform.position = pos;

        //This tabulates time since the last hit to the hero.
        // If that time exceeds timeBetweenHits, the player can take more hits.
        if (isHit)
        {
            timeSinceHit += Time.deltaTime;
            if (timeSinceHit > timeBetweenHits)
            {
                isHit = false;
                timeSinceHit = 0;
            }
        }

    }

    //FixedUpdate() handles physics, it’s called at consistent intervals and not subject to frame rate.
    //Anything that affects a Rigidbody should be updated in FixedUpdate().
    void FixedUpdate()
    {
        //This code moves the head when the marine moves
        // First, it calculates the movement direction.If the value equals Vector3.zero, then the marine is standing still.
        //AddForce() provides a direction then multiply it by the force amount.
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"),
        0, Input.GetAxis("Vertical"));
        if (moveDirection == Vector3.zero)
        {
            bodyAnimator.SetBool("IsMoving", false);
        }
        else
        {
            bodyAnimator.SetBool("IsMoving", true);

            head.AddForce(transform.right * 150, ForceMode.Acceleration);
        }

        // creates an empty RaycastHit. If we get a hit, it’ll be populated with an object.
        RaycastHit hit;
        //cast the ray from the main camera to the mouse position.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //draws a ray in the Scene view while playing the game
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.white);

        if (Physics.Raycast(ray, out hit, 1000, layerMask,
            QueryTriggerInteraction.Ignore))
        {
            if (hit.point != currentLookTarget)
            {
                currentLookTarget = hit.point;
            }

            // 1 We get the target position. 
            Vector3 targetPosition = new Vector3(hit.point.x,
             transform.position.y, hit.point.z);
            // 2 We calculate the current quaternion, which is used to determine rotation.
            Quaternion rotation = Quaternion.LookRotation(targetPosition -
             transform.position);
            // 3 We do the actual turn by using Lerp().
            transform.rotation = Quaternion.Lerp(transform.rotation,
             rotation, Time.deltaTime * 10.0f);
        }

    }
}
