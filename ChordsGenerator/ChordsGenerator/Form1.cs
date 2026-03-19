using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChordsGenerator
{

    public partial class Form1 : Form
    {
        private string[] _args;
        private int      _height = 320;
        private int      _width = 220;

        private class ChordItem
        {
            public string Name { get; set; }
            public string FilePath { get; set; }

            public ChordItem(string name, string filePath)
            {
                Name = name;
                FilePath = filePath;
            }
            
            public override string ToString() // Important: override ToString() to control what is displayed
            {
                return Name; // only the chord name shows in the list
            }
        }

        /* ============================================= INITIALIZATION OF THE FORM ============================================= */
        public Form1(string[] args)
        {            
            _args = args;
            InitializeComponent();

            // -------------- Init form --------------
            
            // Default values
            settingsTextBox.Text        = _args != null && _args.Length >= 1 && !string.IsNullOrWhiteSpace(_args[0]) ? _args[0] : "";
            whereToGenerateTextBox.Text = _args != null && _args.Length >= 2 && !string.IsNullOrWhiteSpace(_args[1]) ? _args[1] : "";
            widthTextBox.Text           = _args != null && _args.Length >= 3 && int.TryParse(_args[2], out int width) ? width.ToString() : _width.ToString();
            heightTextBox.Text          = _args != null && _args.Length >= 4 && int.TryParse(_args[3], out int height) ? height.ToString() : _height.ToString();

            if (!string.IsNullOrWhiteSpace(settingsTextBox.Text) && Directory.Exists(settingsTextBox.Text))            
                initChordsList(settingsTextBox.Text);
            
            
        }

        private void initChordsList (String filepath)
        {
            var files = Directory.GetFiles(filepath, "*.txt");

            if (chordsFoundList.Items.Count > 0)
                chordsFoundList.Items.Clear();                    

            foreach (var file in files)
            {                
                string name = null;

                foreach (var line in File.ReadLines(file))                
                    if (line.StartsWith("name="))
                        name = line.Substring(5).Trim();
                
                if (!string.IsNullOrEmpty(name))                                    
                    chordsFoundList.Items.Add(new ChordItem(name, file));
            }
            chordsFoundList.Sorted = true;            
        }


        /* ============================================= BUTTONS MANAGEMENT ============================================= */
        private void browseBtn_Click(object sender, EventArgs e)
        {
            if (settingsBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                settingsTextBox.Text = settingsBrowserDialog.SelectedPath;                
                initChordsList(settingsBrowserDialog.SelectedPath);
            }
        }

        private void targetBrowseBtn_Click(object sender, EventArgs e)
        {
            if (settingsBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                whereToGenerateTextBox.Text = settingsBrowserDialog.SelectedPath;                
            }
        }

        private void generateChordBtn_Click(object sender, EventArgs e)
        {
            if ( !(chordsFoundList.CheckedItems.Count > 0) )
            { 
                MessageBox.Show("Please select at least one chord to generate.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            // Init variables for the chord
            string name = null;
            
            int firstFret = 0;
            List<int>[] stringFrets = new List<int>[6]; // list of frets per string
            bool[] bools = new bool[6];

            // Initialize lists
            for (int i = 0; i < 6; i++)
                stringFrets[i] = new List<int>();

            // Variable value assignment for the chord            
            ChordItem chord = (ChordItem)chordsFoundList.SelectedItem;
            string chordFilename = chord.FilePath;            

            foreach (var line in File.ReadLines(chordFilename))
            {
                // Populate Chord name
                if (line.StartsWith("name="))
                    name = line.Substring(5).Trim();                    
                                        
                // Populate first fret
                if (line.StartsWith("firstFret="))
                    firstFret = int.Parse(line.Substring("firstFret=".Length).Trim());

                // Populate Chords positions
                if (line.StartsWith("StringNb"))
                {
                    // Example line: "StringNb5=1,3"
                    int index = int.Parse(line.Substring(8, 1)) - 1; // StringNb1 → index 0
                    string fretText = line.Substring(line.IndexOf('=') + 1).Trim();
                    
                    if (!string.IsNullOrEmpty(fretText))
                        foreach (var f in fretText.Split(','))
                            stringFrets[index].Add(int.Parse(f.Trim()));
                }

                // Populate String isPlayed
                if (line.StartsWith("isStringNb") && line.Contains("Played"))
                {
                    int startIndex = "isStringNb".Length; // 10
                    int endIndex = line.IndexOf("Played");
                    int stringNumber = int.Parse(line.Substring(startIndex, endIndex - startIndex));
                    int arrayIndex = stringNumber - 1; // Convert 1-based to 0-based

                    bools[arrayIndex] = bool.Parse(line.Substring(line.IndexOf('=') + 1).Trim());
                }
            }                            

            GenerateImage(name, firstFret, stringFrets, bools);
        }

        /* ============================================= IMAGE GENERATION ============================================= */
        private void GenerateImage(string name, int firstFret, List<int>[] stringFrets, bool[] stringPlayed)
        {


            string outputPath = whereToGenerateTextBox.Text;
            
            if (string.IsNullOrWhiteSpace(outputPath) || !Directory.Exists(outputPath))
            {                                
                MessageBox.Show( "Please provide a valid folder path to generate images.", "Invalid Path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int width = int.TryParse(widthTextBox.Text, out int w) ? w : _width;
            int height = int.TryParse(heightTextBox.Text, out int h) ? h : _height;

            // Margins
            float marginX = width * 0.15f;
            float marginY = height * 0.15f;

            // Spacing
            float stringSpacing = (width - 2 * marginX) / 5f; // 6 strings → 5 gaps
            float fretSpacing = (height - 2 * marginY) / 4f;   // 4 frets

            int[] strumIndices = stringsToStrumList.CheckedIndices.Cast<int>().ToArray();

            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);

                // Fonts proportional
                Font titleFont = new Font("Arial", height / 16f, FontStyle.Bold);
                Font fretFont = new Font("Arial", height / 32f);
                Font xoFont = new Font("Arial", height / 24f, FontStyle.Bold);

                // Draw chord name
                StringFormat center = new StringFormat { Alignment = StringAlignment.Center };
                g.DrawString(name, titleFont, Brushes.Black, width / 2f, height * 0.9f, center);

                // Draw frets horizontal lines
                for (int i = 0; i <= 4; i++)
                {
                    float y = marginY + i * fretSpacing;
                    g.DrawLine(Pens.Black, marginX, y, marginX + stringSpacing * 5, y);
                }

                // Draw fret numbers
                for (int i = 0; i < 4; i++)
                {
                    int fretNumber = firstFret + i;
                    float y = marginY + i * fretSpacing + fretSpacing / 2f;
                    g.DrawString(fretNumber.ToString(), fretFont, Brushes.Black, marginX - fretFont.Size * 2, y - fretFont.Size / 2);
                }

                // X/O above strings
                for (int i = 0; i < 6; i++)
                {
                    int displayIndex = 5 - i; // invert strings so 6th = left
                    float x = marginX + i * stringSpacing;
                    float yXO = marginY - xoFont.Size * 2;

                    if (!stringPlayed[displayIndex])
                        g.DrawString("X", xoFont, Brushes.Black, x, yXO);
                    else if (!stringFrets[displayIndex].Any())
                        g.DrawString("O", xoFont, Brushes.Black, x, yXO);
                }

                // Vertical strings
                for (int i = 0; i < 6; i++)
                {
                    float x = marginX + i * stringSpacing;

                    // Strings in CheckedListBox: 0 = 6th, 1 = 5th, ..., 5 = 1st
                    int displayIndex = i; // i = 0 is leftmost (6th string)
                    bool isStrummed = stringsToStrumList.CheckedIndices.Contains(displayIndex);

                    int thickness = isStrummed ? 3 : 1;
                    using (Pen stringPen = new Pen(isStrummed ? Color.Red : Color.Black, thickness))
                    {
                        g.DrawLine(stringPen, x, marginY, x, marginY + fretSpacing * 4);
                    }
                }

                // Draw dots/barres
                for (int fret = firstFret; fret <= firstFret + 3; fret++)
                {
                    int startBar = -1;
                    int endBar = -1;
                    for (int i = 0; i < 6; i++)
                    {
                        int displayIndex = 5 - i;
                        if (stringFrets[displayIndex].Contains(fret))
                        {
                            if (startBar == -1) startBar = i;
                            endBar = i;
                        }
                        else
                        {
                            if (startBar != -1)
                            {
                                DrawFingerOrBar(g, marginX, marginY, stringSpacing, fretSpacing, startBar, endBar, fret - firstFret);
                                startBar = -1;
                                endBar = -1;
                            }
                        }
                    }
                    if (startBar != -1)
                        DrawFingerOrBar(g, marginX, marginY, stringSpacing, fretSpacing, startBar, endBar, fret - firstFret);
                }
            }

            string safeName = string.Concat(name.Split(Path.GetInvalidFileNameChars()));
            bmp.Save(Path.Combine(outputPath, $"chord_{safeName}.png"), System.Drawing.Imaging.ImageFormat.Png);
        }

        private void DrawFingerOrBar(Graphics g, float startX, float startY, float stringSpacing, float fretSpacing, int startBar, int endBar, int fretOffset)
        {
            float yBar = startY + fretOffset * fretSpacing + fretSpacing / 2f;
            int count = endBar - startBar + 1;

            float dotSize = Math.Min(stringSpacing, fretSpacing) / 2f;

            if (count >= 3)
            {
                // barre: width spans multiple strings, height is dotSize
                float xStart = startX + startBar * stringSpacing - dotSize / 2f;
                float xEnd = startX + endBar * stringSpacing + dotSize / 2f;
                g.FillRectangle(Brushes.Black, xStart, yBar - dotSize / 2f, xEnd - xStart, dotSize);
            }
            else
            {
                for (int s = startBar; s <= endBar; s++)
                {
                    float x = startX + s * stringSpacing;
                    g.FillEllipse(Brushes.Black, x - dotSize / 2f, yBar - dotSize / 2f, dotSize, dotSize);
                }
            }
        }

        /* ============================================= HANDLERS ============================================= */
        /* --- Management of the "chords found" list --- */
        private void chordsFoundList_ItemCheck(object sender, EventArgs e)
        {
            CheckedListBox list = (CheckedListBox)sender;
            int selectedIndex = list.SelectedIndex;

            if (selectedIndex >= 0)                            
                for (int i = 0; i < list.Items.Count; i++)                
                    list.SetItemChecked(i, i == selectedIndex);                
        }
    }
}