using UnityEngine;

public class Room : MonoBehaviour
{
   [SerializeField] private GameObject[] enemies;
    private Vector3[] initialPosition;

    private void Awake()
    {
        //salvam pozitia initiala a inamicilor
        initialPosition = new Vector3[enemies.Length];
        for(int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null )
                 initialPosition[i] = enemies[i].transform.position;
        }

    }

    public void ActivateRoom(bool _status)
    {
        //activarea sau deactivarea inamicilor
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].SetActive(_status);
                enemies[i].transform.position = initialPosition[i];
            }
               
        }

    }

}
