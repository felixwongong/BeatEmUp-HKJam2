using cfEngine.Util;

public enum GameStateId
{
    InfoLoad,
    Login,
    UserDataLoad,
    Initialization,
    Home,
    Battle,
}

public abstract class GameState : State<GameStateId>
{
}

public class GameStateMachine: StateMachine<GameStateId>
{
    public GameStateMachine(): base()
    {
        RegisterState(new InfoLoadState());
        RegisterState(new LoginState());
        RegisterState(new UserDataLoadState());
        RegisterState(new InitializationState());
        RegisterState(new HomeState());
        RegisterState(new BattleState());
    }
}