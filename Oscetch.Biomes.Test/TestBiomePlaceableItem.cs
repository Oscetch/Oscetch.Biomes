
namespace Oscetch.Biomes.Test
{
    internal class TestBiomePlaceableItem(int deathLimit = 2, int birthLimit = 3, float creationChance = .25f) : IBiomePlaceableItem
    {
        public Guid Id { get; } = Guid.NewGuid();

        public int DeathLimit { get; } = deathLimit;

        public int BirthLimit { get; } = birthLimit;

        public float InitialCreationChance { get; } = creationChance;
    }
}
