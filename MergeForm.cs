namespace PdfMerger;

public class MergeForm : Form
{
    private ListBox pdfListBox = new ListBox();
    private Button addButton = new Button();
    public MergeForm()
    {
        Text = "PDF Merger";
        Width = 480;
        Height = 420;

        pdfListBox.Location = new Point(20,20);
        pdfListBox.Size = new Size(430, 220);

        addButton.Text = "Add File(s)";
        addButton.Location = new Point(20,250);
        addButton.Size = new Size(140,32);

        Controls.Add(pdfListBox);
        Controls.Add(addButton);

        addButton.Click += AddButton_Click;
    }

    private void AddButton_Click(object? sender, EventArgs e)
    {
        pdfListBox.Items.Add("test.pdf");
    }
}