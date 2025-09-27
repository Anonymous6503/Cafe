using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(StateMachine))]
[RequireComponent(typeof(NavMeshAgent))]
public class BaseCharacter : MonoBehaviour, ICharacter
{
    public StateMachine stateMachine { get; private set; }
    public NavMeshAgent navMeshAgent { get; private set; }
    public Animator animator { get; private set; }

    public GameObject progressBarContainer;
    public Image progressBarFillImage;

    protected virtual void Awake()
    {
        stateMachine = GetComponent<StateMachine>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public void UpdateMovementAnimation(bool isMoving)
    {
        animator.SetBool("isMoving", isMoving);
    }

    public void MoveTo(Vector3 destination)
    {
        navMeshAgent.SetDestination(destination);
    }


    public void StopAgent()
    {
        navMeshAgent.isStopped = true;
    }

    public void ResumeAgent()
    {
        navMeshAgent.isStopped = false;
    }

    public void LookAt(Vector3 position)
    {
        StartCoroutine(SmoothLookAt(position));
    }

    private System.Collections.IEnumerator SmoothLookAt(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        // We only want to rotate on the Y axis
        direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        float elapsedTime = 0f;
        float rotationTime = 0.5f;

        while (elapsedTime < rotationTime)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, elapsedTime / rotationTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetRotation;
    }

    public void UpdateProgressBar(float progress) 
    {
        if (progressBarFillImage != null)
        {
            progressBarFillImage.fillAmount = progress;
        }

        if (progressBarContainer != null)
        {
            progressBarContainer.SetActive(progress > 0 && progress < 1);
        }
    }
}