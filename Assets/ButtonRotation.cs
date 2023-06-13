using UnityEngine;
using UnityEngine.UI;

public class ButtonRotation : MonoBehaviour
{
    public Button[] buttons;
    public Transform objectToMove;
    public float moveSpeed = 5f;
    private int[] buttonDirections;

    private void Start()
    {
        buttonDirections = new int[buttons.Length];

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Create a local copy of the index variable for the listener

            buttons[index].onClick.AddListener(() => RotateButton(index));
        }
    }

    private void RotateButton(int buttonIndex)
    {
        buttonDirections[buttonIndex] = (buttonDirections[buttonIndex] + 1) % GetButtonDirections(buttonIndex).Length;
        int direction = buttonDirections[buttonIndex];

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, GetButtonDirections(buttonIndex)[direction]);

        buttons[buttonIndex].transform.rotation = targetRotation;
    }

    private float[] GetButtonDirections(int buttonIndex)
    {
        // Customize this method to define the directions for each button
        switch (buttonIndex)
        {
            case 0:
                return new float[] { 0f, 90f, 270f };
            case 1: // Button 1 - 2 directions: right, up
                return new float[] { 180f, 270f };
            case 2:
                return new float[] { 90f, 180f };
            case 3:
                return new float[] { 0f, 270f };
            case 4:
                return new float[] { 90f, 0f, 270f };
            case 5:
                return new float[] { 0f, 90f };
        }

        return null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is the object to move
        if (collision.gameObject == objectToMove.gameObject)
        {
            // Get the direction index based on the current button rotation
            int directionIndex = buttonDirections[GetButtonIndexFromCollision(collision)];

            // Get the direction in radians
            float directionRadians = GetButtonDirections(GetButtonIndexFromCollision(collision))[directionIndex] * Mathf.Deg2Rad;

            // Calculate the movement vector
            Vector3 movement = new Vector3(Mathf.Cos(directionRadians), Mathf.Sin(directionRadians), 0f);

            // Apply the movement to the object
            objectToMove.position += movement * moveSpeed * Time.deltaTime;
        }
    }

    private int GetButtonIndexFromCollision(Collision collision)
    {
        // Find the button index based on the colliding GameObject's tag or name
        for (int i = 0; i < buttons.Length; i++)
        {
            if (collision.gameObject.CompareTag(buttons[i].tag) || collision.gameObject.name == buttons[i].name)
            {
                return i;
            }
        }

        return -1; // Button not found
    }
}
