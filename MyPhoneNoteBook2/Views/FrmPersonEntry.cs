using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyPhoneNoteBook2.MiddleViewLayers;

namespace MyPhoneNoteBook2.Views
{
    public partial class FrmPersonEntry : Form
    {
        private readonly IPersonIntery personIntery;
        public FrmPersonEntry(IPersonIntery personIntery)
        {
            InitializeComponent();
            this.personIntery = personIntery;
            Preparing();
        }
        private bool Preparing()
        {
            bool res = false;
            /***************/
            this.numericUpDown1.DataBindings.Add(new Binding("Value", this.personIntery, "Number"));
            this.textBox1.DataBindings.Add(new Binding("Text", this.personIntery, "Name"));
            this.textBox2.DataBindings.Add(new Binding("Text", this.personIntery, "Phone"));
            /*******************************************************************************/

            //this.button1.DataBindings.Add(new Binding("Enabled", this.personIntery, ""));

            this.button1.Click += (s, e) =>
            {
                if (this.personIntery.OkCommand.Execute())
                {
                    this.Close();
                }
            };

            this.personIntery.ReBindingCurrentObject();
            return res;
        }
    }
}
