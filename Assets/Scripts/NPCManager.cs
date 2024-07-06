using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    //THESE PROPERTIES SHOULD NOT BE DIRECTLY ACCESSED!
    [Header("Speech Properties")]
    [SerializeField] private TMPro.TMP_Text SpeechBubbleText;
    [SerializeField] private Animator SpeechBubbleAnim;
    [Header("Other")]
    [SerializeField] private Transform Model;
    [SerializeField] private SphereCollider triggerCollider;
    [SerializeField] private string[] possibleOrders;
    public string order;

    private Transform _target;
    private int _showHash;
    
    public bool PlayerNear { get; private set; } = false;

    public StateMachine FSM { get; private set; }
    public static IdleState IdleState { get; private set; } = new();
    public static ConverseState ConverseState { get; private set; } = new();

    /// <summary>
    /// Opens this npc's speech bubble, and dispays a message.
    /// </summary>
    /// <param name="message">What text to place in the speech bubble</param>
    public void ShowMessage(string message)
    {
        SpeechBubbleAnim.SetBool(_showHash, true);
        SpeechBubbleText.text = message;
    }
    /// <summary>
    /// Closes this npc's speech bubble.
    /// </summary>
    public void HideMessage() => SpeechBubbleAnim.SetBool(_showHash, false);
    /// <summary>
    /// Rotates to face the target
    /// </summary>
    /// 
    const float FaceLerpRate = 8;
    public void FaceTarget()
    {
        Model.transform.rotation = Quaternion.Lerp(Model.transform.rotation, Quaternion.LookRotation(_target.position - Model.transform.position), FaceLerpRate * Time.deltaTime);
    }
    /// <summary>
    /// Sets the target to the player by searching for gameObjects that are tagged "Player".
    /// </summary>
    public void TargetPlayer()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Awake()
    {
        if (SpeechBubbleAnim) _showHash = Animator.StringToHash("Show");

        NewOrder();

        FSM = new StateMachine(IdleState);
    }

    void Update()
    {
        FSM.CurrentState.Update(this);
        Countdown();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Test");
        if (other.tag == "Player")
        {
            PlayerNear = true;
        }
        else if (other.tag == "Food")
        {
            Debug.Log("Food Entered" + other.name);
            FoodType food = other.GetComponent<FoodType>();
            if (food != null && food.foodType == order)
            {
                // Correct food delivered
                Destroy(other.gameObject);
                timeRemaining = complaintTimer;
                ScoreManager.instance.AddScore();
                ShowMessage("Om Nom Nom");
                NewOrder();
            }
        }
    }
    // Countdown timer that resets when correct food is delivered
    [SerializeField] public int complaintTimer;
    public float timeRemaining = 0; 
    

    private void Countdown()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            timeRemaining = complaintTimer;
            ScoreManager.instance.RemoveScore();
            ShowMessage("Too Slow");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") PlayerNear = false;
    }

    void NewOrder()
    {
        int index = Random.Range(0, possibleOrders.Length);
        order = possibleOrders[index];
    }
}
