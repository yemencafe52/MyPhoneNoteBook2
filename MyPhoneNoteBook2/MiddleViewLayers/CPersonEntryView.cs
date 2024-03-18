using DataLayer;
using MyPhoneNoteBook2.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhoneNoteBook2.MiddleViewLayers
{
    public interface IPersonIntery
    {
        int Number { get; set; }
        string Name { get; set; }
        string Phone { get; set; }
        IBaseCommand OkCommand { get; }

        void ReBindingCurrentObject();
    }

    public class CPersonCreatorView : IPersonIntery
    {
        private readonly ICAccessDB idb;
        /******************************/
        private int perNumber;
        private string perName;
        private string perPhone;
        private IBaseCommand okCommand;
        public CPersonCreatorView(ICAccessDB idb)
        {
            this.idb = idb;
            /**************************/
            this.perName = string.Empty;
            this.perPhone = string.Empty;
            this.okCommand = new CreateCommand(this, this.idb); ;
        }
        public IBaseCommand OkCommand { get => okCommand;}
        public int Number { get => perNumber; set => perNumber = value; }
        public string Name { get => perName; set => perName = value; }
        public string Phone { get => perPhone; set => perPhone = value; }

        public void ReBindingCurrentObject()
        {
            
        }
    }

    public class CPersonEditroView : IPersonIntery
    {
        private readonly ICAccessDB idb;
        private readonly IMainView mainView;
        /******************************/
        private int perNumber;
        private string perName;
        private string perPhone;
        private IBaseCommand okCommand;
        public CPersonEditroView(IMainView mainView,ICAccessDB idb)
        {
            this.mainView = mainView;
            this.idb = idb;
            /**************************/
            this.perName = string.Empty;
            this.perPhone = string.Empty;
            this.okCommand = new UpdateCommand(this, this.idb);

            ReBindingCurrentObject();
        }

        public void ReBindingCurrentObject()
        {
            if (this.mainView.People.Count > 0)
            {
                Person p = (this.mainView.People.Current as Person);
                this.perNumber = p.Number;
                this.perName = p.Name;
                this.perPhone = p.Phone;
            }
        }
        public IBaseCommand OkCommand { get => okCommand; }
        public int Number { get => perNumber; set => perNumber = value; }
        public string Name { get => perName; set => perName = value; }
        public string Phone { get => perPhone; set => perPhone = value; }
    }
}
