using Oscetch.Biomes;

namespace BiomeTester
{
    public class BiomePlaceableItem(string imagePath) : IBiomePlaceableItem
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; init; } = "";
        public int DeathLimit { get; init; } = 2;
        public int BirthLimit { get; init; } = 3;
        public float InitialCreationChance { get; init; } = .25f;
        public string ImagePath { get; } = imagePath;


        private Image? _image;
        public Image Image 
        {
            get 
            {
                _image ??= Image.FromFile(ImagePath);
                return _image;
            }
        }

        public override string ToString()
        {
            var name = Name.Length == 0 ? Id.ToString() : Name;
            return $"{name} | {ImagePath}";
        }
    }
}
