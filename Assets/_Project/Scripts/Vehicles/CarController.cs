using UnityEngine;

public class CarController : MonoBehaviour
{
    public Car Car;

    void Update()
    {
        var state = new CarInputState()
        {
            Gas = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) ? 1f : 0,
            Brake = (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) ? 1f : 0,
        };

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            state.Steering -= 1f;
        }
        
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            state.Steering += 1f;
        }

        Car.Input = state;
    }
}
