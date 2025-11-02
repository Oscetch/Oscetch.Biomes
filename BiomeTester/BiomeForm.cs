using Oscetch.Biomes;
using System.ComponentModel;

namespace BiomeTester
{
    public partial class BiomeForm : Form
    {
        [Browsable(false)]
        public BiomeConfiguration<BiomePlaceableItem> Item { get; set; }
            = new BiomeConfiguration<BiomePlaceableItem>(new Dictionary<BiomeLayer, List<BiomePlaceableItem>>());

        public BiomeForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            nameTextBox.Text = Item.Name;
            deathLimitInput.Value = Item.DeathLimit;
            birthLimitInput.Value = Item.BirthLimit;
            initialCreationChanceInput.Value = (decimal)(Item.InitialCreationChance * 100);
            numberOfSimulationsInput.Value = Item.NumberOfSimulations;
            RefreshLayers();
        }

        private void RefreshLayers()
        {
            layerListBox.Items.Clear();
            var currentOrder = (int)layerOrderInput.Value;
            var currentOrderIsUsed = false;
            foreach (var layer in Item.LayerItems.Keys)
            {
                if (!currentOrderIsUsed && layer.Value == currentOrder)
                {
                    currentOrderIsUsed = true;
                }
                layerListBox.Items.Add(layer);
            }
            addLayerButton.Enabled = !currentOrderIsUsed;
            RefreshItems();
        }

        private void RefreshItems()
        {
            var selection = layerListBox.SelectedItem;
            layerItemsListBox.Items.Clear();
            if (selection == null)
            {
                deleteLayerButton.Enabled = false;
                itemAddButton.Enabled = false;
                return;
            }
            itemAddButton.Enabled = true;
            deleteLayerButton.Enabled = true;
            var items = Item.LayerItems[(BiomeLayer)selection];
            foreach (var item in items)
            {
                layerItemsListBox.Items.Add(item);
            }
        }

        private void LayerListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshItems();
        }

        private void DeleteLayerButton_Click(object sender, EventArgs e)
        {
            var selection = (BiomeLayer)layerListBox.SelectedItem!;
            var newLayerMap = new Dictionary<BiomeLayer, List<BiomePlaceableItem>>();
            foreach (var key in Item.LayerItems.Keys)
            {
                if (key.Value == selection.Value) continue;
                newLayerMap[key] = Item.LayerItems[key];
            }
            Item = Item.CopyWith(layerItems: newLayerMap);
            RefreshLayers();
        }

        private void AddLayerButton_Click(object sender, EventArgs e)
        {
            var biomeLayer = new BiomeLayer((int)layerOrderInput.Value);
            var newLayerMap = Item.LayerItems.ToDictionary();
            newLayerMap[biomeLayer] = [];
            Item = Item.CopyWith(layerItems: newLayerMap);
            RefreshLayers();
        }

        private void LayerOrderInput_ValueChanged(object sender, EventArgs e)
        {
            var currentOrder = (int)layerOrderInput.Value;
            var currentOrderIsUsed = false;
            foreach (var layer in Item.LayerItems.Keys)
            {
                if (!currentOrderIsUsed && layer.Value == currentOrder)
                {
                    currentOrderIsUsed = true;
                    break;
                }
            }
            addLayerButton.Enabled = !currentOrderIsUsed;
        }

        private void LayerItemsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var hasSelection = layerItemsListBox.SelectedItem != null;
            removeLayerItemButton.Enabled = hasSelection;
            if (hasSelection)
            {
                SetItem((BiomePlaceableItem)layerItemsListBox.SelectedItem!);
            }
        }

        private void SetItem(BiomePlaceableItem item)
        {
            itemIdTextBox.Text = item.Id.ToString();
            itemNameInput.Text = item.Name;
            itemDeathLimitInput.Value = item.DeathLimit;
            itemBirthLimitInput.Value = item.BirthLimit;
            itemInitialCreationChanceInput.Value = (decimal)(item.InitialCreationChance * 100);
            itemNumberOfSimulationsInput.Value = item.NumberOfSimulations;
            itemImageBox.ImageLocation = item.ImagePath;
        }

        private void GenerateIdButton_Click(object sender, EventArgs e)
        {
            itemIdTextBox.Text = Guid.NewGuid().ToString();
        }

        private void ItemImageBox_DoubleClick(object sender, EventArgs e)
        {
            using var imageDialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "Image (*.png; *.jpg)|*.png;*.jpg"
            };
            if (imageDialog.ShowDialog() == DialogResult.OK)
            {
                itemImageBox.ImageLocation = imageDialog.FileName;
            }
        }

        private void ItemAddButton_Click(object sender, EventArgs e)
        {
            var item = new BiomePlaceableItem(itemImageBox.ImageLocation)
            {
                Id = itemIdTextBox.Text.Length == 0 ? Guid.NewGuid() : Guid.Parse(itemIdTextBox.Text),
                Name = itemNameInput.Text,
                DeathLimit = (int)itemDeathLimitInput.Value,
                BirthLimit = (int)itemBirthLimitInput.Value,
                InitialCreationChance = ((int)itemInitialCreationChanceInput.Value) / 100f,
                NumberOfSimulations = (int)itemNumberOfSimulationsInput.Value,
            };
            var layerSelection = (BiomeLayer)layerListBox.SelectedItem!;
            var layer = Item.LayerItems[layerSelection];
            var newItems = layer.Where(x => x.Id != item.Id).ToList();
            newItems.Add(item);

            var newLayerItems = Item.LayerItems.ToDictionary();
            newLayerItems[layerSelection] = newItems;

            Item = Item.CopyWith(layerItems: newLayerItems);
            RefreshItems();
        }

        private void RemoveLayerItemButton_Click(object sender, EventArgs e)
        {
            var item = (BiomePlaceableItem)layerItemsListBox.SelectedItem!;
            var layerSelection = (BiomeLayer)layerListBox.SelectedItem!;
            var layer = Item.LayerItems[layerSelection];
            var newItems = layer.Where(x => x.Id != item.Id).ToList();
            var newLayerItems = Item.LayerItems.ToDictionary();
            newLayerItems[layerSelection] = newItems;

            Item = Item.CopyWith(layerItems: newLayerItems);
            RefreshItems();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            Item = Item.CopyWith(name: nameTextBox.Text);
        }

        private void DeathLimitInput_ValueChanged(object sender, EventArgs e)
        {
            Item = Item.CopyWith(deathLimit: (int)deathLimitInput.Value);
        }

        private void BirthLimitInput_ValueChanged(object sender, EventArgs e)
        {
            Item = Item.CopyWith(birthLimit: (int)birthLimitInput.Value);
        }

        private void InitialCreationChanceInput_ValueChanged(object sender, EventArgs e)
        {
            Item = Item.CopyWith(initalCreationChance: ((float)initialCreationChanceInput.Value) / 100f);
        }

        private void NumberOfSimulationsInput_ValueChanged(object sender, EventArgs e)
        {
            Item = Item.CopyWith(numberOfSimulations:  (int)numberOfSimulationsInput.Value);
        }
    }
}
