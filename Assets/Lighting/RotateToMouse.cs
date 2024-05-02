using UnityEngine;

public class RotateToMouse : MonoBehaviour
{
    // Reference to the character's transform
    public Transform characterTransform;

    // Offset of the pivot point from the center of the flashlight
    public Vector3 pivotOffset;

    // Update is called once per frame
    void Update()
    {
        // Get the position of the mouse in screen coordinates
        Vector3 mousePos = Input.mousePosition;

        // Convert the screen coordinates to world coordinates
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.z - Camera.main.transform.position.z));

        // Adjust mouse position based on pivot offset
        mousePos += pivotOffset;

        // Get the direction from the object to the mouse
        Vector2 direction = (mousePos - transform.position).normalized;

        // Calculate the angle in radians
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the character's rotation to the flashlight rotation
        float characterAngle = characterTransform.eulerAngles.z;
        if (characterTransform.localScale.x < 0) // Check if character is flipped
        {
            // If character is flipped, adjust the angle accordingly
            characterAngle += 180f;
        }

        transform.rotation = Quaternion.Euler(0, 0, angle + characterAngle);
    }
}