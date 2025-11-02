namespace BiomeTester
{
    partial class BiomeTester
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            drawControl1 = new DrawControl();
            tableLayoutPanel2 = new TableLayoutPanel();
            label1 = new Label();
            biomeSimulations = new NumericUpDown();
            label2 = new Label();
            seed = new NumericUpDown();
            label3 = new Label();
            biomes = new ListBox();
            editBiomesButton = new Button();
            generateButton = new Button();
            addBiome = new Button();
            deleteBiome = new Button();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)biomeSimulations).BeginInit();
            ((System.ComponentModel.ISupportInitialize)seed).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tableLayoutPanel1.Controls.Add(drawControl1, 1, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1527, 805);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // drawControl1
            // 
            drawControl1.Dock = DockStyle.Fill;
            drawControl1.Location = new Point(308, 3);
            drawControl1.Map = null;
            drawControl1.Name = "drawControl1";
            drawControl1.Size = new Size(1216, 799);
            drawControl1.TabIndex = 0;
            drawControl1.Text = "drawControl1";
            drawControl1.TileSize = 16;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(label1, 0, 0);
            tableLayoutPanel2.Controls.Add(biomeSimulations, 0, 1);
            tableLayoutPanel2.Controls.Add(label2, 0, 2);
            tableLayoutPanel2.Controls.Add(seed, 0, 3);
            tableLayoutPanel2.Controls.Add(label3, 0, 4);
            tableLayoutPanel2.Controls.Add(biomes, 0, 5);
            tableLayoutPanel2.Controls.Add(editBiomesButton, 0, 6);
            tableLayoutPanel2.Controls.Add(generateButton, 0, 9);
            tableLayoutPanel2.Controls.Add(addBiome, 0, 7);
            tableLayoutPanel2.Controls.Add(deleteBiome, 0, 8);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 10;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel2.Size = new Size(299, 799);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(105, 20);
            label1.TabIndex = 0;
            label1.Text = "Biome simulations";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // biomeSimulations
            // 
            biomeSimulations.Dock = DockStyle.Fill;
            biomeSimulations.Location = new Point(3, 23);
            biomeSimulations.Maximum = new decimal(new int[] { 200000, 0, 0, 0 });
            biomeSimulations.Name = "biomeSimulations";
            biomeSimulations.Size = new Size(293, 23);
            biomeSimulations.TabIndex = 1;
            biomeSimulations.Value = new decimal(new int[] { 3, 0, 0, 0 });
            biomeSimulations.ValueChanged += BiomeSimulations_ValueChanged;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new Point(3, 50);
            label2.Name = "label2";
            label2.Size = new Size(32, 20);
            label2.TabIndex = 2;
            label2.Text = "Seed";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // seed
            // 
            seed.Dock = DockStyle.Fill;
            seed.Location = new Point(3, 73);
            seed.Maximum = new decimal(new int[] { 2000000, 0, 0, 0 });
            seed.Minimum = new decimal(new int[] { 2000000, 0, 0, int.MinValue });
            seed.Name = "seed";
            seed.Size = new Size(293, 23);
            seed.TabIndex = 3;
            seed.ValueChanged += Seed_ValueChanged;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            label3.AutoSize = true;
            label3.Location = new Point(3, 100);
            label3.Name = "label3";
            label3.Size = new Size(46, 20);
            label3.TabIndex = 4;
            label3.Text = "Biomes";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // biomes
            // 
            biomes.Dock = DockStyle.Fill;
            biomes.FormattingEnabled = true;
            biomes.ItemHeight = 15;
            biomes.Location = new Point(3, 123);
            biomes.Name = "biomes";
            biomes.Size = new Size(293, 513);
            biomes.TabIndex = 5;
            biomes.SelectedIndexChanged += Biomes_SelectedIndexChanged;
            // 
            // editBiomesButton
            // 
            editBiomesButton.Dock = DockStyle.Fill;
            editBiomesButton.Enabled = false;
            editBiomesButton.Location = new Point(3, 642);
            editBiomesButton.Name = "editBiomesButton";
            editBiomesButton.Size = new Size(293, 34);
            editBiomesButton.TabIndex = 6;
            editBiomesButton.Text = "Edit Biome";
            editBiomesButton.UseVisualStyleBackColor = true;
            editBiomesButton.Click += EditBiomesButton_Click;
            // 
            // generateButton
            // 
            generateButton.Dock = DockStyle.Fill;
            generateButton.Location = new Point(3, 762);
            generateButton.Name = "generateButton";
            generateButton.Size = new Size(293, 34);
            generateButton.TabIndex = 7;
            generateButton.Text = "Generate";
            generateButton.UseVisualStyleBackColor = true;
            generateButton.Click += GenerateButton_Click;
            // 
            // addBiome
            // 
            addBiome.Dock = DockStyle.Fill;
            addBiome.Location = new Point(3, 682);
            addBiome.Name = "addBiome";
            addBiome.Size = new Size(293, 34);
            addBiome.TabIndex = 8;
            addBiome.Text = "Add Biome";
            addBiome.UseVisualStyleBackColor = true;
            addBiome.Click += AddBiome_Click;
            // 
            // deleteBiome
            // 
            deleteBiome.Dock = DockStyle.Fill;
            deleteBiome.Enabled = false;
            deleteBiome.Location = new Point(3, 722);
            deleteBiome.Name = "deleteBiome";
            deleteBiome.Size = new Size(293, 34);
            deleteBiome.TabIndex = 9;
            deleteBiome.Text = "Remove Biome";
            deleteBiome.UseVisualStyleBackColor = true;
            deleteBiome.Click += DeleteBiome_Click;
            // 
            // BiomeTester
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1527, 805);
            Controls.Add(tableLayoutPanel1);
            Name = "BiomeTester";
            Text = "Biome Tester";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)biomeSimulations).EndInit();
            ((System.ComponentModel.ISupportInitialize)seed).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private DrawControl drawControl1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label1;
        private NumericUpDown biomeSimulations;
        private Label label2;
        private NumericUpDown seed;
        private Label label3;
        private ListBox biomes;
        private Button editBiomesButton;
        private Button generateButton;
        private Button addBiome;
        private Button deleteBiome;
    }
}
