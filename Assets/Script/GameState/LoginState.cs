using System;
using System.Threading.Tasks;
using cfEngine.Logging;
using cfEngine.Util;

public class LoginState: GameState
{
    public class Param : cfEngine.Util.StateParam 
    {
        public LoginPlatform Platform;
        public LoginToken Token;
    }
    
    public override GameStateId Id => GameStateId.Login;
    protected internal override void StartContext(StateMachine<GameStateId> sm, cfEngine.Util.StateParam stateParam)
    {
        if (stateParam is not Param p)
        {
            var ex = new ArgumentNullException(nameof(stateParam), "Invalid param for Login State");
            Log.LogException(ex);
            throw ex;
        }

        LoginAsync(p).ContinueWith(task =>
        {
            if (!task.IsFaulted)
            {
                if (task.Result)
                {
                    sm.GoToState(GameStateId.UserDataLoad);
                }
                else
                {
                    sm.GoToState(GameStateId.Login, new Param()
                    {
                        Platform = LoginPlatform.Local,
                    });
                }
            }
            else
            {
                Log.LogException(task.Exception);
            }
        }, Game.TaskToken);
    }

    private async Task<bool> LoginAsync(Param param)
    {
        var token = Game.TaskToken;

        await Game.Auth.InitAsync(token);

        if (param.Platform == LoginPlatform.FromCached)
        {
            var loggedInCached = await Game.Auth.TryLoginCachedUserAsync(token);

            return loggedInCached;
        }

        if (!Game.Auth.IsSessionUserExist())
        {
            await Game.Auth.SignUpAsync(param.Platform, param.Token);
            return true;
        }
        else
        {
            //need to handle link account later
            return false;
        }
    }
}
