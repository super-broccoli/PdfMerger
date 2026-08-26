namespace PdfMerger;

public class MergeForm : Form
{
    private ListBox pdfListBox = new ListBox();
    private Button addButton = new Button();
    private Button removeButton = new Button();
    private Label inFormMessage = new Label();
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

        //UI for "Remove Selected" button
        removeButton.Text = "Remove Selected";
        removeButton.Location = new Point(170, 250);
        removeButton.Size = new Size(140,32);

        inFormMessage.Location = new Point(20, 300);
        inFormMessage.Size = new Size(430, 60);
        //inFormMessage.ForeColor = Color.Red;

        Controls.Add(pdfListBox);
        Controls.Add(addButton);
        Controls.Add(removeButton);
        Controls.Add(inFormMessage);

        addButton.Click += addButton_Click;
        removeButton.Click += removeButton_Click;
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
            foreach(string path in openFile.FileNames)
            {
                pdfListBox.Items.Add(path);
            }
        }
    }

    private void removeButton_Click(object? sender, EventArgs e)
    {
        if (pdfListBox.SelectedIndex != -1)
        {
            pdfListBox.Items.RemoveAt(pdfListBox.SelectedIndex);
        }
        else
        {
            //MessageBox.Show("Select a file first.")
            inFormMessage.Text = "Select a file first.";
            inFormMessage.ForeColor = Color.Red;
        }
    }
}