using System.Collections.Generic;

[System.Serializable]
public class ServerEntry
{
    public string name;
    public string ip_address;
    public int port;
    public int current_players;
    public int max_players;
}

[System.Serializable]
public class ServerListResponse
{
    public List<ServerEntry> servers;
}