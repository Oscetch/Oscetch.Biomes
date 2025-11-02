namespace Oscetch.Biomes
{
    public class GeneratorConfiguration<T>(IReadOnlyList<BiomeConfiguration<T>> biomes) where T : IBiomePlaceableItem
    {
        public int NumberOfSimulations { get; init; } = 3;
        public int Seed { get; init; } = new Random().Next();
        public IReadOnlyList<BiomeConfiguration<T>> Biomes { get; } = biomes;
    }
}
