namespace Oscetch.Biomes
{
    public interface IBiomeItem
    {
        Guid Id { get; }
        int DeathLimit { get; }
        int BirthLimit { get; }
        float InitialCreationChance { get; }
    }
}
