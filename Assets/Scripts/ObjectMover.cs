using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private bool isTextObject;

    private bool isStopped;

    private void Update()
    {
        if (isStopped)
        {
            return;
        }

        if (isTextObject)
        {
            rb.MovePosition(transform.position + new Vector3(0f, 0f, -(SpeedController.Instance.TextObjectSpeed * Time.deltaTime)));
            return;
        }

        rb.MovePosition(transform.position + new Vector3(0f, 0f, -(SpeedController.Instance.LevelSpeed * Time.deltaTime)));
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
        isStopped = false;
    }

    public void Stop()
    {
        isStopped = true;
    }
}
