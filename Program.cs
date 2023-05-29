using System.Diagnostics;

const int AreaScale = 100;
const int AreaBoundsWidth = 1024 * AreaScale;
const int AreaBoundsHeight = 768 * AreaScale;
const int MaxEntityWidth = 10;
const int MaxEntityHeight = 10;

#if DEBUG
var iterations = new[] { 1, 2, 10, 100, 1000, 10_000 };
#else
var iterations = new[] { 1, 2, 10, 100, 1000, 10_000, 100_000, 1_000_000, 10_000_000 };
#endif

var random = new Random();
foreach (var iterationCount in iterations)
{
    List<EntityClass> entityClasses = new();
    EntityStruct[] entityStructs = new EntityStruct[iterationCount];

    foreach (var index in Enumerable.Range(0, iterationCount))
    {

        var x = random.NextSingle() * AreaBoundsWidth;
        var y = random.NextSingle() * AreaBoundsHeight;
        var width = random.NextSingle() * MaxEntityWidth;
        var height = random.NextSingle() * MaxEntityHeight;

        entityStructs[index] = new EntityStruct
        {
            x = x,
            y = y,
            width = width,
            height = height
        };
        entityClasses.Add(new EntityClass
        {
            x = x,
            y = y,
            width = width,
            height = height
        });
    }
    Stopwatch watch = new Stopwatch();
    int collisionCount;
    Console.Write($"{iterationCount,10:N0} Iterations: ");
    if (iterationCount <= 100_000)
    {
        Console.Write("EntityClass: ");
        watch.Restart();
        collisionCount = 0;
        for (int firstEntityIndex = 0; firstEntityIndex < entityClasses.Count; firstEntityIndex++)
        {
            EntityClass entity1 = entityClasses[firstEntityIndex];
            for (int secondEntityIndex = firstEntityIndex + 1; secondEntityIndex < entityClasses.Count; secondEntityIndex++)
            {
                EntityClass entity2 = entityClasses[secondEntityIndex];
                if (entity1.x + entity1.width > entity2.x &&
                entity1.x < entity2.x + entity2.width &&
                entity1.y + entity1.height > entity2.y &&
                entity1.y < entity2.y + entity2.height)
                {
                    // Collision
                    collisionCount++;
                }
            }
        }
        watch.Stop();
        Console.Write($"Duration (ms): {watch.ElapsedMilliseconds}; ");
        Console.Write($"Total Collisions: {collisionCount}; ");
    }

    if (iterationCount <= 100_000)
    {
        Console.Write("  ~~~  EntityStruct: ");
        watch.Restart();
        collisionCount = 0;
        for (int firstEntityIndex = 0; firstEntityIndex < entityStructs.Length; firstEntityIndex++)
        {
            EntityStruct entity1 = entityStructs[firstEntityIndex];
            for (int secondEntityIndex = firstEntityIndex + 1; secondEntityIndex < entityStructs.Length; secondEntityIndex++)
            {
                EntityStruct entity2 = entityStructs[secondEntityIndex];
                if (entity1.x + entity1.width > entity2.x &&
                entity1.x < entity2.x + entity2.width &&
                entity1.y + entity1.height > entity2.y &&
                entity1.y < entity2.y + entity2.height)
                {
                    // Collision
                    collisionCount++;
                }
            }
        }
        watch.Stop();
        Console.Write($"Duration (ms): {watch.ElapsedMilliseconds}; ");
        Console.Write($"Total Collisions: {collisionCount}; ");
    }

    if (true)
    {
        Console.Write("  ~~~  Dumb Spacial Hash: ");
        watch.Restart();
        var rowBucketCount = 100;
        var columnBucketCount = 100;
        var buckets = new List<EntityStruct>[rowBucketCount, columnBucketCount];
        collisionCount = 0;
        for (int firstEntityIndex = 0; firstEntityIndex < entityStructs.Length; firstEntityIndex++)
        {
            EntityStruct entity = entityStructs[firstEntityIndex];
            var bucketRowMin = (int)Math.Floor(entity.y / AreaBoundsHeight * rowBucketCount);
            var bucketRowMax = Math.Min((int)Math.Floor((entity.y + entity.height) / AreaBoundsHeight * rowBucketCount), rowBucketCount - 1);
            var bucketColumnMin = (int)Math.Floor(entity.x / AreaBoundsWidth * columnBucketCount);
            var bucketColumnMax = Math.Min((int)Math.Floor((entity.x + entity.width) / AreaBoundsWidth * columnBucketCount), columnBucketCount - 1);
            Debug.Assert(bucketRowMax >= bucketRowMin);
            Debug.Assert(bucketColumnMax >= bucketColumnMin);
            foreach (var row in Enumerable.Range(bucketRowMin, bucketRowMax - bucketRowMin + 1))
            {
                foreach (var column in Enumerable.Range(bucketColumnMin, bucketColumnMax - bucketColumnMin + 1))
                {
                    var list = buckets[row, column];
                    if (list is null)
                    {
                        buckets[row, column] = list = new List<EntityStruct>();
                    }
                    list.Add(entity);
                }
            }
        }

        foreach (var row in Enumerable.Range(0, rowBucketCount))
        {
            foreach (var column in Enumerable.Range(0, columnBucketCount))
            {
                var list = buckets[row, column];
                if (list != null)
                {
                    for (int firstEntityIndex = 0; firstEntityIndex < list.Count; firstEntityIndex++)
                    {
                        EntityStruct entity1 = list[firstEntityIndex];
                        for (int secondEntityIndex = firstEntityIndex + 1; secondEntityIndex < list.Count; secondEntityIndex++)
                        {
                            EntityStruct entity2 = list[secondEntityIndex];
                            if (entity1.x + entity1.width > entity2.x &&
                            entity1.x < entity2.x + entity2.width &&
                            entity1.y + entity1.height > entity2.y &&
                            entity1.y < entity2.y + entity2.height)
                            {
                                // Collision
                                collisionCount++;
                            }
                        }
                    }
                }
            }
        }
        watch.Stop();
        Console.Write($"Duration (ms): {watch.ElapsedMilliseconds}; ");
        Console.Write($"Total Collisions: {collisionCount}; ");
    }
    Console.WriteLine();
}

class EntityClass
{
    public float x;
    public float y;
    public float width;
    public float height;
}

struct EntityStruct
{
    public float x;
    public float y;
    public float width;
    public float height;
}
