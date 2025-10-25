using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;

    private Transform currentCheckpoint;
    private Health playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
    }

    public void Respawn()
    {
        transform.position = currentCheckpoint.position;
        playerHealth.Respawn(); //respawn player to checkpoint position

        //Move camera to checkpoint room
        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);

     }

    //Activate Checkpoints

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint") 
        {
            currentCheckpoint = collision.transform;//Store the checkpoint that we activated as the current one
            collision.GetComponent<Collider2D>().enabled = false;//Deactivate checkpoint
            collision.GetComponent<Animator>().SetTrigger("appear");//trigger checkpoint animation

        }
    }
}
