using UnityEngine;

public class WallCollideCheck : MonoBehaviour
{
    public BoxCollider2D gridArea;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 position = this.transform.position;
        Vector3 playerPos = other.transform.position;
        Bounds bounds = this.gridArea.bounds;
        if (other.tag.Equals("Player"))
        {
            // set player position to the far end of another wall
            if (position.x > 0)
            {
                other.transform.position = new Vector3(Mathf.Round(bounds.min.x), playerPos.y, 0.0f);
            }
            else if (position.x < 0)
            {
                other.transform.position = new Vector3(Mathf.Round(bounds.max.x), playerPos.y, 0.0f);
            }
            else if (position.y > 0)
            {
                other.transform.position = new Vector3(playerPos.x, Mathf.Round(bounds.min.y), 0.0f);
            }
            else if (position.y < 0)
            {
                other.transform.position = new Vector3(playerPos.x, Mathf.Round(bounds.max.y), 0.0f);
            }
        }
    }
}
