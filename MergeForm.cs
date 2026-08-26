namespace PdfMerger;

public class MergeForm : Form
{
    private ListBox pdfListBox = new ListBox();
    private Button addButton = new Button();
    public MergeForm()
    {
        //UI for Form
        Text = "PDF Merger";
        Width = 480;
        Height = 420;

        //UI for the files list box
        pdfListBox.Location = new Point(20,20);
        pdfListBox.Size = new Size(430, 220);

        //UI for "Add File(s)" button
        addButton.Text = "Add File(s)";
        addButton.Location = new Point(20,250);
        addButton.Size = new Size(140,32);

        Controls.Add(pdfListBox);
        Controls.Add(addButton);

        addButton.Click += AddButton_Click;
    }

    private void AddButton_Click(object? sender, EventArgs e)
    {
       // pdfListBox.Items.Add("test.pdf");
       using OpenFileDialog openFile = new OpenFileDialog();
       openFile.Filter = "PDF Files|*.pdf";
       openFile.Multiselect = true;

       if (openFile.ShowDialog() == DialogResult.OK)
        {
            foreach(string path in openFile.FileNames)
            {
                pdfListBox.Items.Add(path);
            }
        }
    }
}