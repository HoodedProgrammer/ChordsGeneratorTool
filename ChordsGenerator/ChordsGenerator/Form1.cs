using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace ChordsGenerator
{

    public partial class Form1 : Form
    {
        private string[] _args;
        private int      _height = 320;
        private int      _width = 220;
        private String   _placeHolder = "Filename for your image.";
        private bool     _isUpdating = false; // For filename management purpose

        /* ============================================= CUSTOM CLASS ============================================= */
        public class Chord
        {
            public string Name { get; set; }
            public int FirstFret { get; set; }
            public List<int>[] StringFrets { get; set; } = new List<int>[6];
            public bool[] StringPlayed { get; set; } = new bool[6];

            public string FilePath { get; private set; }

            public Chord(string filePath)
            {
                FilePath = filePath;
                for (int i = 0; i < 6; i++)
                    StringFrets[i] = new List<int>();

                LoadFromFile(filePath);
            }

            public override string ToString() => Name;

            private void LoadFromFile(string filePath)
            {
                foreach (var line in File.ReadLines(filePath))
                {
                    if (line.StartsWith("name=")) 
                        Name = line.Substring(5).Trim();
                    if (line.StartsWith("firstFret=")) 
                        FirstFret = int.Parse(line.Substring("firstFret=".Length).Trim());
                    
                    if (line.StartsWith("StringNb"))
                    {
                        int index = int.Parse(line.Substring(8, 1)) - 1;
                        string fretText = line.Substring(line.IndexOf('=') + 1).Trim();
                        if (!string.IsNullOrEmpty(fretText))
                            foreach (var f in fretText.Split(',')) StringFrets[index].Add(int.Parse(f.Trim()));
                    }

                    if (line.StartsWith("isStringNb") && line.Contains("Played"))
                    {
                        int startIndex = "isStringNb".Length;
                        int endIndex = line.IndexOf("Played");
                        int stringNumber = int.Parse(line.Substring(startIndex, endIndex - startIndex));
                        int arrayIndex = stringNumber - 1;
                        StringPlayed[arrayIndex] = bool.Parse(line.Substring(line.IndexOf('=') + 1).Trim());
                    }
                }
            }
        }

        /* ============================================= INITIALIZATION OF THE FORM ============================================= */
        public Form1(string[] args)
        {            
            _args = args;
            InitializeComponent();

            // -------------- Init form --------------
            
            // Default values in case no arguments are passed
            settingsTextBox.Text        = _args != null && _args.Length >= 1 && !string.IsNullOrWhiteSpace(_args[0]) ? _args[0] : "";
            whereToGenerateTextBox.Text = _args != null && _args.Length >= 2 && !string.IsNullOrWhiteSpace(_args[1]) ? _args[1] : "";
            widthTextBox.Text           = _args != null && _args.Length >= 3 && int.TryParse(_args[2], out int width) ? width.ToString() : _width.ToString();
            heightTextBox.Text          = _args != null && _args.Length >= 4 && int.TryParse(_args[3], out int height) ? height.ToString() : _height.ToString();

            // PlaceHolder like, for the filename textbox
            filenameTextBox.Text = _placeHolder;
            filenameTextBox.TextAlign = HorizontalAlignment.Center;
            filenameTextBox.ForeColor = Color.Gray;

            if (!string.IsNullOrWhiteSpace(settingsTextBox.Text) && Directory.Exists(settingsTextBox.Text))            
                initChordsList(settingsTextBox.Text);            
        }

        /* --- Initialization of the "chords Found" list with the txt files present in the chords library --- */
        private void initChordsList (String filepath)
        {
            var files = Directory.GetFiles(filepath, "*.txt");

            if (chordsFoundList.Items.Count > 0)
                chordsFoundList.Items.Clear();                    

            foreach (var file in files)
            {
                Chord chord = new Chord(file);
                chordsFoundList.Items.Add(chord); // ToString() returns Name
            }
            chordsFoundList.Sorted = true;            
        }

        /* --- Management of the "Form1" click, to lose focus of other components --- */
        private void Form1_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
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
                whereToGenerateTextBox.Text = settingsBrowserDialog.SelectedPath;                            
        }

        private void generateChordBtn_Click(object sender, EventArgs e)
        {
            if ( !(chordsFoundList.CheckedItems.Count > 0) )
            { 
                MessageBox.Show("Please select at least one chord to generate.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (chordsFoundList.SelectedItem is Chord chord)
            {
                //Call the image generation function
                Bitmap bmp = GenerateImage(chord.Name, chord.FirstFret, chord.StringFrets, chord.StringPlayed, false);

                //If conditions permits, store the image as png
                string outputPath = whereToGenerateTextBox.Text;
                if (string.IsNullOrWhiteSpace(outputPath) || !Directory.Exists(outputPath))
                {
                    MessageBox.Show("Please provide a valid folder path to generate images.", "Invalid Path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string safeName = !string.IsNullOrWhiteSpace(filenameTextBox.Text) && filenameTextBox.Text  != _placeHolder ? filenameTextBox.Text : string.Concat(chord.Name.Split(Path.GetInvalidFileNameChars())) + "_chord";
                bmp.Save(Path.Combine(outputPath, $"{safeName}.png"), System.Drawing.Imaging.ImageFormat.Png);
            }                
        }

        /* ============================================= IMAGE GENERATION ============================================= */
        private Bitmap GenerateImage(string name, int firstFret, List<int>[] stringFrets, bool[] stringPlayed, bool isPreview = true)
        {

            int width  = (isPreview) ? previewBox.Width  : int.TryParse(widthTextBox.Text, out int w) ? w : _width;
            int height = (isPreview) ? previewBox.Height : int.TryParse(heightTextBox.Text, out int h) ? h : _height;

            int[] strumIndices = stringsToStrumList.CheckedIndices.Cast<int>().ToArray();

            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);

                // Margins
                float marginX = width * 0.15f;
                float marginY = height * 0.15f;

                // Spacing
                float stringSpacing = (width - 2 * marginX) / 5f; // 6 strings → 5 gaps
                float fretSpacing = (height - 2 * marginY) / 4f;   // 4 frets

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
            
            return bmp;
            
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
        private void chordsFoundList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckedListBox list = (CheckedListBox)sender;

            for (int i = 0; i < list.Items.Count; i++)
                if (i != e.Index)
                    list.SetItemChecked(i, false);

            if ( e.NewValue == CheckState.Checked && list.SelectedItem is Chord chord)
            {
                Bitmap preview = GenerateImage(chord.Name, chord.FirstFret, chord.StringFrets, chord.StringPlayed, true);
                previewBox.Image = preview;
            }
            else
                previewBox.Image = null;
        }

        /* --- Management of the "Strings to strum" list --- */
        private void stringsToStrumList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.BeginInvoke
            (
                (Action)
                (
                    () =>
                    {
                        if (chordsFoundList.CheckedItems.Count > 0)
                        {
                            Chord chord = chordsFoundList.CheckedItems[0] as Chord;
                            Bitmap preview = GenerateImage(chord.Name, chord.FirstFret, chord.StringFrets, chord.StringPlayed, true);
                            if (previewBox.Image != null)
                                previewBox.Image.Dispose();
                            previewBox.Image = preview;
                        }
                    }
                )
            );
        }
        
        /* --- Management of the "filename" TextBox --- */
        private void filenameTextBox_Enter(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox) sender;

            if (textBox.Text == _placeHolder)
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
            }
        }

        private void filenameTextBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = (System.Windows.Forms.TextBox)sender;

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = _placeHolder;
                textBox.ForeColor = Color.Gray;
            }            
        }
        
        private void filenameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (_isUpdating) return;
            _isUpdating = true;

            TextBox textBox = (TextBox)sender;
            int cursorPosition = textBox.SelectionStart;

            string cleaned = string.Concat(textBox.Text.Split(Path.GetInvalidFileNameChars()));

            if (textBox.Text != cleaned)
            {
                textBox.Text = cleaned;                
                textBox.SelectionStart = Math.Max(0, Math.Min(cursorPosition, cleaned.Length)); // Clamp safely between 0 and new length
            }

            _isUpdating = false;
        }
    }
}