using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ServerScriptableObject : ScriptableObject
{
    public enum ServerName { None, Dev, Alpha, Prod, };
    public ServerName name = ServerName.None;

    public struct ServerAddress
    {
        public string ip;
        public int port;
    }
    public ServerAddress address
    {
        get
        {
            var result = new ServerAddress();

            switch (this.name)
            {
                case ServerName.Dev:
                    result.ip = "127.0.0.1";
                    result.port = 8081;
                    break;
                case ServerName.Alpha:
                    result.ip = "127.0.0.2";
                    result.port = 8082;
                    break;
                case ServerName.Prod:
                    result.ip = "127.0.0.3";
                    result.port = 8083;
                    break;
            }

            return result;
        }
    }
    

}
