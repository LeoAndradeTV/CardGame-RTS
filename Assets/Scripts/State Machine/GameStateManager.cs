
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    public GameStateAbstract currentState;
    public GameStateAbstract lastState;
    public AttackState attackState = new AttackState();
    public BuildState buildState = new BuildState();
    public BuyCardsState buyCardsState = new BuyCardsState();
    public EndTurnState endTurnState = new EndTurnState();
    public PlayCardsState playCardsState = new PlayCardsState();
    public StartTurnState startTurnState = new StartTurnState();
    public DrawCardsState drawCardsState = new DrawCardsState();

    public bool hasAttacked = false;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        currentState = startTurnState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }
    
    public void SwitchState(GameStateAbstract state)
    {
        lastState = currentState;
        currentState = state;
        state.EnterState(this);
    }

    public GameStateAbstract GetLastState()
    {
        return lastState;    
    }
}
