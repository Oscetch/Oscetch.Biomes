namespace Oscetch.Biomes
{
    public interface IBiomeItem
    {
        /// <summary>
        /// The identifier for this biome item
        /// </summary>
        Guid Id { get; }
        /// <summary>
        /// How many neighbors of a different id than this, is needed to "kill" this item.
        /// A value of 8 or higher will make it so that this item is never removed during simulation.
        /// </summary>
        int DeathLimit { get; }
        /// <summary>
        /// How many neighbors of the same type is required for this item to spawn during simulation.
        /// A value of 8 or higher will make it so that this item is never created during simulation.
        /// </summary>
        int BirthLimit { get; }
        /// <summary>
        /// The chance that this item is initially spawned, outside of simulation
        /// </summary>
        float InitialCreationChance { get; }
    }
}
