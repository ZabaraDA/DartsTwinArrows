using Newtonsoft.Json;

public class LevelData
{
    [JsonProperty("number")]
    public int Number;
    [JsonProperty("weaponId")]
    public int WeaponId;
    [JsonProperty("enemyId")]
    public int EnemyId;
    [JsonProperty("enemyCount")]
    public int EnemyCount;
    [JsonProperty("spawnAtCenter")]
    public bool SpawnAtCenter;
    [JsonProperty("spawnCount")]
    public int SpawnCount;
}
