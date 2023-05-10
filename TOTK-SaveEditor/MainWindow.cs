namespace TOTK_SaveEditor
{
    public partial class MainWindow : Form
    {
        private SaveFile? _saveFile = null;

        public MainWindow()
        {
            InitializeComponent();
            FillComboBoxes();
        }

        private void SetComboIndex(List<Item> items, string value, ComboBox comboBox)
        {
            comboBox.SelectedIndex = 0;

            var index = items.FindIndex(item => item.Id == value);
            if (index == -1) return;

            comboBox.SelectedIndex = index;
        }

        private void BtnOpenSaveFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog FileDialog = new OpenFileDialog
            {
                Filter = "progress (*.sav)|*.sav"
            };

            if (FileDialog.ShowDialog() != DialogResult.OK) return;
            if (!FileDialog.CheckFileExists) return;

            _saveFile = new SaveFile(FileDialog.FileName);

            if (!_saveFile.IsLoaded)
            {
                MessageBox.Show("Invalid progress.sav!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LblPath.Text = FileDialog.FileName;

            BtnOpenSaveFile.Enabled = false;
            BtnPatchSaveFile.Enabled = true;
            BtnReset.Enabled = true;

            SetValuesFromSavefile();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            LblPath.Text = "progress.sav";
            _saveFile = null;

            BtnOpenSaveFile.Enabled = true;
            BtnPatchSaveFile.Enabled = false;
            BtnReset.Enabled = false;
        }

        private void FillComboBoxes()
        {
            ComboSwordSlot0.Items.AddRange(GameData.Swords.Select(item => item.Name).ToArray());
            ComboSwordSlot1.Items.AddRange(GameData.Swords.Select(item => item.Name).ToArray());
            ComboSwordSlot2.Items.AddRange(GameData.Swords.Select(item => item.Name).ToArray());
            ComboSwordSlot3.Items.AddRange(GameData.Swords.Select(item => item.Name).ToArray());
            ComboSwordSlot4.Items.AddRange(GameData.Swords.Select(item => item.Name).ToArray());

            ComboBowSlot0.Items.AddRange(GameData.Bows.Select(item => item.Name).ToArray());
            ComboBowSlot1.Items.AddRange(GameData.Bows.Select(item => item.Name).ToArray());
            ComboBowSlot2.Items.AddRange(GameData.Bows.Select(item => item.Name).ToArray());
            ComboBowSlot3.Items.AddRange(GameData.Bows.Select(item => item.Name).ToArray());
            ComboBowSlot4.Items.AddRange(GameData.Bows.Select(item => item.Name).ToArray());

            ComboShieldSlot0.Items.AddRange(GameData.Shields.Select(item => item.Name).ToArray());
            ComboShieldSlot1.Items.AddRange(GameData.Shields.Select(item => item.Name).ToArray());
            ComboShieldSlot2.Items.AddRange(GameData.Shields.Select(item => item.Name).ToArray());
            ComboShieldSlot3.Items.AddRange(GameData.Shields.Select(item => item.Name).ToArray());
            ComboShieldSlot4.Items.AddRange(GameData.Shields.Select(item => item.Name).ToArray());

            ComboArmorSlot0.Items.AddRange(GameData.Armor.Select(item => item.Name).ToArray());
            ComboArmorSlot1.Items.AddRange(GameData.Armor.Select(item => item.Name).ToArray());
            ComboArmorSlot2.Items.AddRange(GameData.Armor.Select(item => item.Name).ToArray());
            ComboArmorSlot3.Items.AddRange(GameData.Armor.Select(item => item.Name).ToArray());
            ComboArmorSlot4.Items.AddRange(GameData.Armor.Select(item => item.Name).ToArray());
        }

        private void SetValuesFromSavefile()
        {
            InputRupees.Value = _saveFile.ReadRupees();
            InputHearts.Value = _saveFile.ReadHearts();
            InputStamina.Value = _saveFile.ReadStamina();

            SetComboIndex(GameData.Swords, _saveFile.ReadSword(0), ComboSwordSlot0);
            SetComboIndex(GameData.Swords, _saveFile.ReadSword(1), ComboSwordSlot1);
            SetComboIndex(GameData.Swords, _saveFile.ReadSword(2), ComboSwordSlot2);
            SetComboIndex(GameData.Swords, _saveFile.ReadSword(3), ComboSwordSlot3);
            SetComboIndex(GameData.Swords, _saveFile.ReadSword(4), ComboSwordSlot4);

            SetComboIndex(GameData.Bows, _saveFile.ReadBow(0), ComboBowSlot0);
            SetComboIndex(GameData.Bows, _saveFile.ReadBow(1), ComboBowSlot1);
            SetComboIndex(GameData.Bows, _saveFile.ReadBow(2), ComboBowSlot2);
            SetComboIndex(GameData.Bows, _saveFile.ReadBow(3), ComboBowSlot3);
            SetComboIndex(GameData.Bows, _saveFile.ReadBow(4), ComboBowSlot4);

            SetComboIndex(GameData.Shields, _saveFile.ReadShield(0), ComboShieldSlot0);
            SetComboIndex(GameData.Shields, _saveFile.ReadShield(1), ComboShieldSlot1);
            SetComboIndex(GameData.Shields, _saveFile.ReadShield(2), ComboShieldSlot2);
            SetComboIndex(GameData.Shields, _saveFile.ReadShield(3), ComboShieldSlot3);
            SetComboIndex(GameData.Shields, _saveFile.ReadShield(4), ComboShieldSlot4);

            SetComboIndex(GameData.Armor, _saveFile.ReadArmor(0), ComboArmorSlot0);
            SetComboIndex(GameData.Armor, _saveFile.ReadArmor(1), ComboArmorSlot1);
            SetComboIndex(GameData.Armor, _saveFile.ReadArmor(2), ComboArmorSlot2);
            SetComboIndex(GameData.Armor, _saveFile.ReadArmor(3), ComboArmorSlot3);
            SetComboIndex(GameData.Armor, _saveFile.ReadArmor(4), ComboArmorSlot4);
        }

        private void BtnPatchSaveFile_Click(object sender, EventArgs e)
        {
            if (_saveFile == null || !_saveFile.IsLoaded)
            {
                MessageBox.Show("Invalid Savefile!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _saveFile.WriteRupees((int)InputRupees.Value);
            _saveFile.WriteHearts((int)InputHearts.Value);
            _saveFile.WriteStamina((int)InputStamina.Value);

            _saveFile.WriteSword(0, GameData.Swords[ComboSwordSlot0.SelectedIndex]);
            _saveFile.WriteSword(1, GameData.Swords[ComboSwordSlot1.SelectedIndex]);
            _saveFile.WriteSword(2, GameData.Swords[ComboSwordSlot2.SelectedIndex]);
            _saveFile.WriteSword(3, GameData.Swords[ComboSwordSlot3.SelectedIndex]);
            _saveFile.WriteSword(4, GameData.Swords[ComboSwordSlot4.SelectedIndex]);

            _saveFile.WriteBow(0, GameData.Bows[ComboBowSlot0.SelectedIndex]);
            _saveFile.WriteBow(1, GameData.Bows[ComboBowSlot1.SelectedIndex]);
            _saveFile.WriteBow(2, GameData.Bows[ComboBowSlot2.SelectedIndex]);
            _saveFile.WriteBow(3, GameData.Bows[ComboBowSlot3.SelectedIndex]);
            _saveFile.WriteBow(4, GameData.Bows[ComboBowSlot4.SelectedIndex]);

            _saveFile.WriteShield(0, GameData.Shields[ComboShieldSlot0.SelectedIndex]);
            _saveFile.WriteShield(1, GameData.Shields[ComboShieldSlot1.SelectedIndex]);
            _saveFile.WriteShield(2, GameData.Shields[ComboShieldSlot2.SelectedIndex]);
            _saveFile.WriteShield(3, GameData.Shields[ComboShieldSlot3.SelectedIndex]);
            _saveFile.WriteShield(4, GameData.Shields[ComboShieldSlot4.SelectedIndex]);

            _saveFile.WriteArmor(0, GameData.Armor[ComboArmorSlot0.SelectedIndex]);
            _saveFile.WriteArmor(1, GameData.Armor[ComboArmorSlot1.SelectedIndex]);
            _saveFile.WriteArmor(2, GameData.Armor[ComboArmorSlot2.SelectedIndex]);
            _saveFile.WriteArmor(3, GameData.Armor[ComboArmorSlot3.SelectedIndex]);
            _saveFile.WriteArmor(4, GameData.Armor[ComboArmorSlot4.SelectedIndex]);

            _saveFile.PatchSaveFile();

            MessageBox.Show("Successfully patched savefile!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
