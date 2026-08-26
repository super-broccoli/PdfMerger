using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
namespace PdfMerger
{
    public class MergeForm : Form
    {
        private ListBox pdfListBox = new ListBox();
        private Button addButton = new Button();
        private Button removeButton = new Button();
        private Button mergeButton = new Button();
        private Label instructionLabel = new Label();
        public MergeForm()
        {
            //UI for Form
            Text = "PDF Merger";
            Width = 480;
            Height = 420;

            //UI for the files list box
            pdfListBox.Location = new Point(20, 20);
            pdfListBox.Size = new Size(430, 220);

            //UI for "Add File(s)" button
            addButton.Text = "Add File(s)";
            addButton.Location = new Point(20, 250);
            addButton.Size = new Size(140, 32);

            //UI for "Remove Selected" button
            removeButton.Text = "Remove Selected";
            removeButton.Location = new Point(170, 250);
            removeButton.Size = new Size(140, 32);

            //UI for "Merge" button
            mergeButton.Text = "Merge";
            mergeButton.Location = new Point(320, 250);
            mergeButton.Size = new Size(130, 32);

            //UI for instruction text
            instructionLabel.Location = new Point(20, 300);
            instructionLabel.Size = new Size(430, 60);
            instructionLabel.Text = "Add two or more PDFs, then click Merge!";

            Controls.Add(pdfListBox);
            Controls.Add(addButton);
            Controls.Add(removeButton);
            Controls.Add(mergeButton);
            Controls.Add(instructionLabel);

            addButton.Click += addButton_Click;
            removeButton.Click += removeButton_Click;
            mergeButton.Click += mergeButton_Click;
        }

        private void addButton_Click(object? sender, EventArgs e)
        {
            // pdfListBox.Items.Add("test.pdf"); //used this to test
            using OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "PDF Files|*.pdf";

            //added this so that can select multiple files at once
            openFile.Multiselect = true;

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                foreach (string path in openFile.FileNames)
                {
                    pdfListBox.Items.Add(path);
                }
                instructionLabel.Text = $"{openFile.FileNames.Length} file(s) added.";
                instructionLabel.ForeColor = Color.Green;
            }
        }

        private void removeButton_Click(object? sender, EventArgs e)
        {
            if (pdfListBox.SelectedIndex != -1)
            {
                pdfListBox.Items.RemoveAt(pdfListBox.SelectedIndex);
                instructionLabel.Text = "File(s) removed!";
                instructionLabel.ForeColor = Color.Green;
            }
            else
            {
                instructionLabel.Text = "Select a file first!";
                instructionLabel.ForeColor = Color.Red;
            }
        }

        private void mergeButton_Click(object? sender, EventArgs e)
        {
            List<string> files = new List<string>();
            foreach (object file in pdfListBox.Items)
            {
                files.Add(file.ToString() ?? string.Empty);
            }

            if (files.Count < 2)
            {
                instructionLabel.Text = "Add at least two PDF files to perform merging!";
                instructionLabel.ForeColor = Color.Red;
                return;
            }

            using SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "PDF Files|*.pdf";
            saveFile.FileName = "merged.pdf";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                string outputPath = saveFile.FileName;
                PdfMerger merger = new PdfMerger();
                merger.Execute(files, outputPath);

                instructionLabel.Text = "PDFs merged successfully!";
                instructionLabel.ForeColor = Color.Green;
            }
        }
    }

    public class PdfMerger
    {
        public void Execute(List<string> inputFiles, string outputPath)
        {
            using PdfSharp.Pdf.PdfDocument outputDocument =
                new PdfSharp.Pdf.PdfDocument();

            int totalPages = 0;

            foreach (string file in inputFiles)
            {
                using PdfSharp.Pdf.PdfDocument inputDocument =
                    PdfSharp.Pdf.IO.PdfReader.Open(
                        file,
                        PdfSharp.Pdf.IO.PdfDocumentOpenMode.Import
                    );

                for (int i = 0; i < inputDocument.PageCount; i++)
                {
                    outputDocument.AddPage(inputDocument.Pages[i]);
                    totalPages++;
                }
            }

            outputDocument.Save(outputPath);
        }
    }
}