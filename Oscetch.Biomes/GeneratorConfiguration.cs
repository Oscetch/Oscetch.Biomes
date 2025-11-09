namespace Oscetch.Biomes
{
    public class GeneratorConfiguration<T>(IReadOnlyList<BiomeConfiguration<T>> biomes) where T : IBiomePlaceableItem
    {
        /// <summary>
        /// How many simulations will run on the biomes placement
        /// </summary>
        public int NumberOfSimulations { get; init; } = 3;
        /// <summary>
        /// The random seed used during the initial placement
        /// </summary>
        public int Seed { get; init; } = new Random().Next();
        /// <summary>
        /// The biomes that should be placed
        /// </summary>
        public IReadOnlyList<BiomeConfiguration<T>> Biomes { get; } = biomes;
    }
}
