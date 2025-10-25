using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class readPython : MonoBehaviour
{
    UdpClient client;
    public static string CurrentGesture = "NONE";

    void Start()
    {
        client = new UdpClient(5065);
        client.Client.Blocking = false;
    }

    void Update()
    {
        if (client.Available > 0)
        {
            IPEndPoint any = new IPEndPoint(IPAddress.Any, 0);
            byte[] data = client.Receive(ref any);
            string g = Encoding.UTF8.GetString(data).Trim();

            // Validate known gestures
            if (g == "CLOSED" || g == "POINT" || g == "OPEN")
            {
                CurrentGesture = g;
                Debug.Log("Gesture: " + g);
            }
            else
            {
                CurrentGesture = "OPEN"; // Fallback to default
                Debug.Log("Unknown gesture received, defaulting to OPEN");
            }
        }
    }

    void OnApplicationQuit()
    {
        client.Close();
    }
}
