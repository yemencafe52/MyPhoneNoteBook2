using System.Windows.Forms;
using MyPhoneNoteBook2.MiddleViewLayers;

namespace MyPhoneNoteBook2.Views
{
    public partial class FrmMain : Form
    {
        private readonly IMainView mainView;

        private readonly IPersonIntery personCreator;
        private readonly IPersonIntery personEditor;

        public FrmMain(
            IMainView mainView,
            IPersonIntery personCreator,
            IPersonIntery personEditor
        )
        {
            InitializeComponent();

            this.mainView = mainView;
            this.personCreator = personCreator;
            this.personEditor = personEditor;

            Preparing();
        }

        private bool Preparing()
        {
            bool res = false;

            this.toolStripButton1.Click += (s, e) =>
            {
                FrmPersonEntry frmPersonEntry = new FrmPersonEntry(this.personCreator);
                frmPersonEntry.ShowDialog();
            };

            this.toolStripButton2.Click += (s, e) =>
            {
                FrmPersonEntry frmPersonEntry = new FrmPersonEntry(this.personEditor);
                frmPersonEntry.ShowDialog();
            };

            this.toolStripButton3.Click += (s, e) =>
            {
                this.mainView.DeleteCommand.Execute();
            };

            this.toolStripButton4.Click += (s, e) =>
            {
                this.mainView.SearchCommand.Execute();
            };

            this.dataGridView1.RowStateChanged += DataGridView1_RowStateChanged;
            this.dataGridView1.DataSource = this.mainView.People;
           
            return res;
        }

        private void DataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            toolStripStatusLabel2.Text = this.mainView.People.Count.ToString(); 
        }
    }
}
