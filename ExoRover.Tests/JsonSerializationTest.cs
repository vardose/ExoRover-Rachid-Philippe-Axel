using Map;
using Xunit;
using System.Text.Json;

namespace ExoRover.Tests;

public class JsonSerializationTest
{
    [Fact]
    public void Map_JsonSerialization_ShouldSerializeAndDeserialize()
    {
        var originalMap = new Map.Map();
        originalMap.addObstacle(new Obstacle(2, 3));
        originalMap.addObstacle(new Obstacle(5, 7));

        var json = JsonSerializer.Serialize(originalMap);
        var deserializedMap = JsonSerializer.Deserialize<Map.Map>(json);

        Assert.NotNull(deserializedMap);
        Assert.True(deserializedMap.hasObstacle(3, 2));
        Assert.True(deserializedMap.hasObstacle(7, 5));
        Assert.False(deserializedMap.hasObstacle(0, 0));
    }

    [Fact]
    public void Map_EmptyMap_JsonSerialization_ShouldWork()
    {
        var emptyMap = new Map.Map();

        var json = JsonSerializer.Serialize(emptyMap);
        var deserializedMap = JsonSerializer.Deserialize<Map.Map>(json);

        Assert.NotNull(deserializedMap);
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                Assert.False(deserializedMap.hasObstacle(x, y));
            }
        }
    }

    [Fact]
    public void Map_FullMap_JsonSerialization_ShouldWork()
    {
        var fullMap = new Map.Map();
        RandomObstacleGenerator.GenerateObstacles(fullMap, 100);

        var json = JsonSerializer.Serialize(fullMap);
        var deserializedMap = JsonSerializer.Deserialize<Map.Map>(json);

        Assert.NotNull(deserializedMap);
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                Assert.True(deserializedMap.hasObstacle(x, y));
            }
        }
    }

    [Fact]
    public void Map_JsonSerialization_ShouldProduceValidJson()
    {
        var map = new Map.Map();
        map.addObstacle(new Obstacle(1, 2));

        var json = JsonSerializer.Serialize(map);

        Assert.NotNull(json);
        Assert.True(json.Length > 0);
        Assert.Contains("Obstacles", json);
        
        var jsonDoc = JsonDocument.Parse(json);
        Assert.NotNull(jsonDoc);
    }

    [Fact]
    public void Map_JsonDeserialization_WithInvalidJson_ShouldThrow()
    {
        var invalidJson = "{invalid json}";

        Assert.ThrowsAny<JsonException>(() => JsonSerializer.Deserialize<Map.Map>(invalidJson));
    }

    [Fact]
    public void Map_JsonSerialization_RoundTrip_ShouldPreserveData()
    {
        var originalMap = new Map.Map();
        var random = new Random(42);
        
        var testPositions = new List<(int lat, int lon)>
        {
            (0, 0), (1, 2), (3, 4), (5, 6), (7, 8), (9, 9)
        };

        foreach (var (lat, lon) in testPositions)
        {
            originalMap.addObstacle(new Obstacle(lat, lon));
        }

        var json = JsonSerializer.Serialize(originalMap);
        var deserializedMap = JsonSerializer.Deserialize<Map.Map>(json);

        Assert.NotNull(deserializedMap);
        
        foreach (var (lat, lon) in testPositions)
        {
            Assert.True(deserializedMap.hasObstacle(lon, lat), 
                $"Obstacle at position ({lon}, {lat}) should be preserved after serialization");
        }

        int originalCount = 0;
        int deserializedCount = 0;
        
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                if (originalMap.hasObstacle(x, y)) originalCount++;
                if (deserializedMap.hasObstacle(x, y)) deserializedCount++;
            }
        }
        
        Assert.Equal(originalCount, deserializedCount);
    }
}
