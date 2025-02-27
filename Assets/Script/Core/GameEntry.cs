using System;
using System.Diagnostics;
using System.Threading;
using cfEngine.Core;
using cfEngine.Core.Layer;
using cfEngine.IO;
using cfEngine.Logging;
using cfEngine.Pooling;
using cfEngine.Serialize;
using cfEngine.Util;
using UnityEngine;

public class GameEntry : MonoBehaviour
{
    [Conditional("UNITY_EDITOR")]
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
    public static void PreprocessInEditor()
    {
        try
        {
            Game.InfoBuildByte();
        }
        catch (Exception e)
        {
            Log.LogException(e);
        }
    }
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Init()
    {
        Log.SetLogger(new UnityLogger());
        Log.SetLogLevel(LogLevel.Debug);

        var cts = new CancellationTokenSource();
        var initParam = new Game.InitParam()
        {
            taskToken = cts.Token,
            info = new InfoLayer(new StreamingAssetStorage("Info"), JsonSerializer.Instance),
            asset = new ResourceAssetManager(),
            pool = new PoolManager(),
            gsm = new GameStateMachine(),
            userData = new UserDataManager(new FileStorage(Application.persistentDataPath), JsonSerializer.Instance),
            auth = new LocalLoginHandler()
        };
        
        Game.MakeInstance(initParam);
        
        Game.Auth.RegisterPlatform(new LocalPlatform());
        
        Game.Gsm.onAfterStateChange += OnStateChanged;
        Application.quitting += OnApplicationQuit;
        
        void OnApplicationQuit()
        {
            Game.Gsm.onAfterStateChange -= OnStateChanged;
            Application.quitting -= OnApplicationQuit;
            
            cts.Cancel();
            Game.Dispose();
        }
        
        Game.Gsm.GoToState(GameStateId.InfoLoad);
    }

    private static void OnStateChanged(StateMachine<GameStateId>.StateChangeRecord record)
    {
        Log.LogInfo($"Game state changed, {record.LastState?.Id.ToString()} -> {record.NewState?.Id.ToString()}");
    }
}
