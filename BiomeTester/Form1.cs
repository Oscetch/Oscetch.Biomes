using Oscetch.Biomes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace BiomeTester
{
    public partial class BiomeTester : Form
    {
        private GeneratorConfiguration<BiomePlaceableItem> _config = new([]);

        public BiomeTester()
        {
            InitializeComponent();
            seed.Maximum = int.MaxValue;
            seed.Value = _config.Seed;
        }

        private void BiomeSimulations_ValueChanged(object sender, EventArgs e)
        {
            _config = new GeneratorConfiguration<BiomePlaceableItem>(_config.Biomes)
            {
                NumberOfSimulations = (int)biomeSimulations.Value,
                Seed = _config.Seed,
            };
        }

        private void Seed_ValueChanged(object sender, EventArgs e)
        {
            _config = new GeneratorConfiguration<BiomePlaceableItem>(_config.Biomes)
            {
                NumberOfSimulations = _config.NumberOfSimulations,
                Seed = (int)seed.Value,
            };
        }

        private void EditBiomesButton_Click(object sender, EventArgs e)
        {
            if (biomes.SelectedItem is not BiomeConfiguration<BiomePlaceableItem> biome)
            {
                return;
            }
            using var form = new BiomeForm();
            form.Item = biome;
            if (form.ShowDialog() == DialogResult.OK)
            {
                var newList = _config.Biomes.Where(x => x.Id != biome.Id).ToList();
                newList.Add(form.Item);
                _config = new GeneratorConfiguration<BiomePlaceableItem>(newList)
                {
                    NumberOfSimulations = _config.NumberOfSimulations,
                    Seed = _config.Seed,
                };
                biomes.Items.Clear();
                foreach (var b in newList) 
                {
                    biomes.Items.Add(b);
                }
            }
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            Enabled = false;
            new Thread(() =>
            {
                var generator = new BiomeGenerator<BiomePlaceableItem>(_config);
                foreach (var map in generator.RunForSizeWithIterations(100, 100))
                {
                    Thread.Sleep(1000);
                    var m = map.ToDictionary(x => x.Key, x => x.Value.ToDictionary());
                    BeginInvoke(() => drawControl1.Map = m);
                    //drawControl1.Map = map;
                }

                BeginInvoke(() => Enabled = true);
            }).Start();
        }

        private void Biomes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var hasSelection = biomes.SelectedItem != null;
            editBiomesButton.Enabled = hasSelection;
            deleteBiome.Enabled = hasSelection;
        }

        private void DeleteBiome_Click(object sender, EventArgs e)
        {
            if (biomes.SelectedItem is not BiomeConfiguration<BiomePlaceableItem> biome)
            {
                return;
            }
            var newList = _config.Biomes.Where(x => x.Id != biome.Id).ToList();
            _config = new GeneratorConfiguration<BiomePlaceableItem>(newList)
            {
                NumberOfSimulations = _config.NumberOfSimulations,
                Seed = _config.Seed,
            };
            biomes.Items.Clear();
            foreach (var b in newList)
            {
                biomes.Items.Add(b);
            }
        }

        private void AddBiome_Click(object sender, EventArgs e)
        {
            using var form = new BiomeForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                var newList = _config.Biomes.ToList();
                newList.Add(form.Item);
                _config = new GeneratorConfiguration<BiomePlaceableItem>(newList)
                {
                    NumberOfSimulations = _config.NumberOfSimulations,
                    Seed = _config.Seed,
                };
                biomes.Items.Clear();
                foreach (var b in newList)
                {
                    biomes.Items.Add(b);
                }
            }
        }
    }
}
