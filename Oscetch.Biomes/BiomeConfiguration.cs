namespace Oscetch.Biomes
{
    public class BiomeConfiguration<T>(IReadOnlyDictionary<BiomeLayer, List<T>> layerItems): IBiomeItem where T : IBiomePlaceableItem
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; init; } = "";
        public int DeathLimit { get; init; } = 2;
        public int BirthLimit { get; init; } = 3;
        public float InitialCreationChance { get; init; } = .25f;
        /// <summary>
        /// How many simulations will run on the placeable items
        /// </summary>
        public int NumberOfSimulations { get; init; } = 3;
        /// <summary>
        /// The items grouped by layers that should be placed inside this biome
        /// </summary>
        public IReadOnlyDictionary<BiomeLayer, List<T>> LayerItems { get; } = layerItems;

        public BiomeConfiguration<T> CopyWith(
            string? name = null,
            int? deathLimit = null,
            int? birthLimit = null,
            float? initalCreationChance = null,
            int? numberOfSimulations = null,
            IReadOnlyDictionary<BiomeLayer, List<T>>? layerItems = null
        ) => new(layerItems ?? LayerItems) 
        {
            Id = Id,
            Name = name ?? Name,
            DeathLimit = deathLimit ?? DeathLimit,
            BirthLimit = birthLimit ?? BirthLimit,
            InitialCreationChance = initalCreationChance ?? InitialCreationChance,
            NumberOfSimulations = numberOfSimulations ?? NumberOfSimulations
        };

        public override string ToString()
        {
            var name = Name.Length == 0 ? Id.ToString() : Name;
            return $"{name} | {DeathLimit} | {BirthLimit}";
        }
    }
}
