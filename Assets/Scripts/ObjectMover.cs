using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    private bool isStopped;
    private float speedMultiplier = 1f;

    private void Update()
    {
        if (isStopped)
        {
            return;
        }
        rb.MovePosition(transform.position + new Vector3(0f, 0f, -(SpeedController.Instance.Speed * speedMultiplier * Time.deltaTime)));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "KillPlane")
        {
            Destroy(gameObject);
        }
    }

    public void Resume()
    {
        speedMultiplier = 1f;
        isStopped = false;
    }

    public void Stop()
    {
        isStopped = true;
        speedMultiplier = 0f;
    }
}
