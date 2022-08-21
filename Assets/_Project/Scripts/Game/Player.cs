using UnityEngine;

public class Player : MonoBehaviour
{
    public CarController CarController;
    public CameraController CameraController;
    public ConsumerScanner Scanner;
    public Car Car;
    public CargoContainer CargoContainer;

    void Update()
    {

        if (Scanner.CurrentConsumableInSight != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Scanner.CurrentConsumableInSight.Consume())
                {
                    CargoContainer.AddLog();
                    CargoContainer.AddLog();
                }
            }
        }
    }
}