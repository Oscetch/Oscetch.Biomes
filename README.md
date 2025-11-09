# Biome generator

## How to

Start by creating a class that inherits the interface `IBiomePlaceableItem`. That class will contain the information of items that are placed in different biomes.

```csharp
public class BiomePlaceableItem(string imagePath) : IBiomePlaceableItem
{
    public Guid Id { get; } = Guid.NewGuid();

    /// How many neighbors of a different id than this, is needed to "kill" this item.
    /// A value of 8 or higher will make it so that this item is never removed during simulation.
    public int DeathLimit { get; init; } = 2;

    /// How many neighbors of the same type is required for this item to spawn during simulation.
    /// A value of 8 or higher will make it so that this item is never created during simulation.
    public int BirthLimit { get; init; } = 3;

    /// The chance that this item is initially spawned, outside of simulation
    public float InitialCreationChance { get; init; } = .25f;

    // This image, or whatever you want this item to be when the generation is finished
    public string ImagePath { get; } = imagePath;
}
```

Now we can start creating a biome. Each biome has different layers, and items placed on the same layer will compete for space in that layer.

For example, say we want a "grass"/"land" biome, and a "water" biome:
```csharp
// these will be our grass background
var limeItem = new BiomePlaceableItem("lime_box.png") { InitialCreationChance = .5f };
var lightGreenItem = new BiomePlaceableItem("light_green_box.png") { InitialCreationChance = .1f };
var dirtItem = new BiomePlaceableItem("light_brown_box.png") { InitialCreationChance = .05f };

// these will sometimes be on top of our grass background
var tree = new BiomePlaceableItem("tree.png") { InitialCreationChance = .1f };
var bush = new BiomePlaceableItem("bush.png") { InitialCreationChance = .1f, DeathLimit = 1 };

// create the biome configuration
var grassBiomeItems = new Dictionary<BiomeLayer, List<BiomePlaceableItem>>
{
  // `BiomeLayer.FillLayer` will tell the generator to fill this biome completely
  { BiomeLayer.FillLayer, [limeItem, lightGreenItem, dirtItem] },
  { new BiomeLayer(0), [tree, bush] }
};

// the same as above but for water
var shallowWater = new BiomePlaceableItem("light_blue_box.png") { InitialCreationChance = .5f };
var deepWater = new BiomePlaceableItem("blue_box.png") { InitialCreationChance = .1f };

var reefRock = new BiomePlaceableItem("rock_in_water.png") { InitialCreationChance = .05f };

var waterBiomeItems = new Dictionary<BiomeLayer, List<BiomePlaceableItem>>
{
  { BiomeLayer.FillLayer, [shallowWater, deepWater] },
  { new BiomeLayer(0), [reefRock] }
};

// Configure the biomes

var grassBiome = new BiomeConfiguration<BiomePlaceableItem>(grassBiomeItems)
{
  InitialCreationChance = .6f,
};
var waterBiome = new BiomeConfiguration<BiomePlaceableItem>(waterBiomeItems)
{
  InitialCreationChance = .1f,
  DeathLimit = 1,
  BirthLimit = 1,
};

// Configure the generation process
var generatorConfiguration = GeneratorConfiguration<BiomePlaceableItem>([grassBiome, waterBiome])
{
  NumberOfSimulations = 10
};

// Generate the map, using a fixed size for the map 100x100
var generator = BiomeGenerator<BiomePlaceableItem>(generatorConfiguration);
var map = generator.RunForSize(100, 100);
```

## Positions

The tile size of the generated maps are always `1`. 
So when you generate a map with a size of 160x160, and your actual tile size is 16x16px, you will want to run `generator.RunForSize(16, 16)` and then scale the resulting postions by a factor of 10.
