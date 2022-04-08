using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayer : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    private Rigidbody rb;
   [SerializeField] bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<Renderer>().material = playerManager.collectedObjMat;
        playerManager.collidedList.Add(gameObject);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Grounded();
        }
    }

    void Grounded()
    {
        isGrounded = true;
        playerManager.playerState = PlayerManager.PlayerState.Move;
        GetComponent<BoxCollider>().isTrigger = false;
        rb.useGravity = false;
        Destroy(this,1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
