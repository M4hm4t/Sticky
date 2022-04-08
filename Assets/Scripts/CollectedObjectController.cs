using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedObjectController : MonoBehaviour
{
    [SerializeField] Transform sphere;
    PlayerManager playerManager;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
        sphere = transform.GetChild(0);
        if (GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
            Rigidbody rb = GetComponent<Rigidbody>();
           rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            GetComponent<Renderer>().material = playerManager.collectedObjMat;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("CollectableObj"))
        {
            if (!playerManager.collidedList.Contains(other.gameObject))
            {
                other.gameObject.tag = "CollectedObj";
                other.transform.parent = playerManager.collectedPollTransform;
                playerManager.collidedList.Add(other.gameObject);
                other.gameObject.AddComponent<CollectedObjectController>();
            }
        }
        if (other.gameObject.CompareTag("Obstacle"))
        {
            DestroyTheObject();
        }
    }

    void DestroyTheObject()
    {
        playerManager.collidedList.Remove(gameObject);
        Destroy(gameObject);
        Transform particle = Instantiate(playerManager.particlePrefab,transform.position,Quaternion.identity);
        particle.GetComponent<ParticleSystem>().startColor = playerManager.collectedObjMat.color;
        Destroy(particle, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CollectableList"))
        {
            other.transform.GetComponent<BoxCollider>().enabled = false;
            other.transform.parent = playerManager.collectedPollTransform;
            foreach (Transform child in other.transform)
            {
                if (!playerManager.collidedList.Contains(child.gameObject))
                {
                    playerManager.collidedList.Add(child.gameObject);
                    child.gameObject.tag = "CollectedObj";
                    child.gameObject.AddComponent<CollectedObjectController>();
                }
            }
        }
        if (other.gameObject.CompareTag("FinishLine"))
        {
            if (playerManager.levelState != PlayerManager.LevelState.Finised)
            {
                playerManager.levelState = PlayerManager.LevelState.Finised;
                playerManager.CallMakeSphere();

            }
        }
    }

    public void MakeSphere()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        sphere.gameObject.GetComponent<MeshRenderer>().enabled = true;
        sphere.gameObject.GetComponent<SphereCollider>().enabled = true;
        sphere.gameObject.GetComponent<SphereCollider>().isTrigger = true;

        sphere.gameObject.GetComponent<Renderer>().material = playerManager.collectedObjMat;
    }
    public void DropObj()
    {
        sphere.gameObject.layer = 6;
        sphere.gameObject.GetComponent<SphereCollider>().isTrigger = false;
        sphere.gameObject.AddComponent<Rigidbody>();
        sphere.GetComponent<Rigidbody>().useGravity = true;
    }

}
