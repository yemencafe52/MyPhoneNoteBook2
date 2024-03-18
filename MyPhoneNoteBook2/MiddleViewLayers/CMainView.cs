using System.Collections.Generic;
using DataLayer;
using MyPhoneNoteBook2.Commands;
using MyPhoneNoteBook2.Models;
using System.Windows.Forms;

namespace MyPhoneNoteBook2.MiddleViewLayers
{
    public interface IPerson
    {
        int Number { get; }
        string Name { get; }
        string Phone { get; }
    }

    public class Person : IPerson
    {
        private int perNumber;
        private string perName;
        private string perPhone;

        public Person(int perNumber, string perName, string perPhone)
        {
            this.perNumber = perNumber;
            this.perName = perName;
            this.perPhone = perPhone;
        }

        public int Number { get => perNumber;}
        public string Name { get => perName;}
        public string Phone { get => perPhone;}
    }

    public interface IMainView
    {
        BindingSource People { get; }
        IBaseCommand SearchCommand { get; }
        string Search { get; set; }
        IBaseCommand DeleteCommand { get; }
    }

    public class CMainView : IMainView
    {
        private readonly ICAccessDB idb;
        /***************************/
        private BindingSource people;
        private IBaseCommand searchCommand;
        private string search;
        private IBaseCommand deleteCommand;

        public CMainView(ICAccessDB idb)
        {
            this.idb = idb;
            /**************/
            this.people = new BindingSource();
            this.people.DataSource = typeof(Person);

            this.searchCommand = new SeachCommand(this,this.idb);
            this.deleteCommand = new DeleteCommand(this, this.idb);

            this.search = string.Empty;
            /**************************/
            CPeopleController.OnUpdate += CPeopleController_OnUpdate;
            this.SearchCommand.Execute();
        }

        private void CPeopleController_OnUpdate(byte cmd, CPerson cPerson)
        {
            switch (cmd)
            {
                case 1:
                    {
                        this.people.Add(new Person(cPerson.PerNumber, cPerson.PerName, cPerson.PerPhone));
                    }
                    break;

                case 2:
                    {
                        int perNumber = cPerson.PerNumber;

                        for (int i = 0; i < this.people.Count; i++)
                        {
                            if (((Person)this.people[i]).Number == perNumber)
                            {
                                this.people[i] = new Person(cPerson.PerNumber, cPerson.PerName, cPerson.PerPhone);
                                break;
                            }
                        }
                    }
                    break;

                case 3:
                    {
                        int perNumber = cPerson.PerNumber;

                        for (int i = 0; i < this.people.Count; i++)
                        {
                            if (((Person)this.people[i]).Number == perNumber)
                            {
                                this.people.Remove(((Person)this.people[i]));
                                break;
                            }
                        }
                    }

                    break;
            }
        }

        public BindingSource People => people;
        public IBaseCommand SearchCommand => this.searchCommand;
        public IBaseCommand DeleteCommand => this.deleteCommand;
        public string Search { get => search; set => search = value; }
    }
}
