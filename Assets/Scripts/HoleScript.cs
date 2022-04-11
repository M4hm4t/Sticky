using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoleScript : MonoBehaviour
{
    PlayerManager playerManager;
   [SerializeField] Camera cam;

    private void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
    }
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("SphereObj")) {
            other.transform.parent.GetComponent<CollectedObjController>().DropObj();
            Invoke("FinishParticle", 1f);
            Invoke("Restart", 2f);
        }
    }

    void FinishParticle ()
    {

        Transform partcile = Instantiate(playerManager.partcilePrefab1, cam.transform.position, Quaternion.identity);
        partcile.GetComponent<ParticleSystem>().startColor = playerManager.collectedObjMat.color;
    }
   public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
