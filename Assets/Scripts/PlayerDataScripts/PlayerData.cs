using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData {

    public string name;
    public int time;

    public PlayerData(string name, int time) {
        this.name = name;
        this.time = time;
    }
}

[Serializable]
public class PlayerDataList
{
    public List<PlayerData> playerData;

    public PlayerDataList() {
        playerData = new List<PlayerData>();
    }
}
