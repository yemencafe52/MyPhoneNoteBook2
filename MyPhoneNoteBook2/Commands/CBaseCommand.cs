using DataLayer;
using MyPhoneNoteBook2.MiddleViewLayers;
using MyPhoneNoteBook2.Models;
using System;
using System.Collections.Generic;


// sorry :(

namespace MyPhoneNoteBook2.Commands
{
    public interface IBaseCommand
    {
        bool CanExecute();
        bool Execute();
    }

    public class SeachCommand : IBaseCommand
    {
        private CMainView cMainView;
        private ICAccessDB idb;

        public SeachCommand(CMainView cMainView, ICAccessDB idb)
        {
            this.cMainView = cMainView;
            this.idb = idb;
        }

        public bool CanExecute()
        {
            bool res = false;
            if (!(string.IsNullOrEmpty(this.cMainView.Search)))
            {
                res = true;
            }

            return res;
        }

        public bool Execute()
        {
            bool res = false;

            this.cMainView.People.Clear();
            /******************/
            //if (CanExecute())
            {
                List<CPerson> r = new List<CPerson>();
                if (new CPeopleController(this.idb).Search(this.cMainView.Search, ref r))
                {
                    foreach (var x in r)
                    {
                        this.cMainView.People.Add(new Person(x.PerNumber, x.PerName, x.PerPhone));
                    }
                    res = true;
                }
            }

            return res;
        }
    }

    public class DeleteCommand : IBaseCommand
    {
        private CMainView cMainView;
        private ICAccessDB idb;

        public DeleteCommand(CMainView cMainView, ICAccessDB idb)
        {
            this.cMainView = cMainView;
            this.idb = idb;
        }

        public bool CanExecute()
        {
            bool res = false;
            if (this.cMainView.People.Count > 0)
            {
                if ((this.cMainView.People.Current as IPerson).Number > 0)
                {
                    res = true;
                }
            }
            return res;
        }

        public bool Execute()
        {
            bool res = false;
            if (this.CanExecute())
            {
                if (new CPeopleController(this.idb).Delete((this.cMainView.People.Current as Person).Number))
                {
                    res = true;
                }
            }
            return res;
        }
    }

    public class CreateCommand : IBaseCommand
    {
        private readonly CPersonCreatorView cPersonCreatorView;
        private readonly ICAccessDB idb;
        public bool CanExecute()
        {
            bool res = false;
            if (
                (this.cPersonCreatorView.Number > 0)
                &&
                (!(string.IsNullOrEmpty(this.cPersonCreatorView.Name)))
                &&
                (!(string.IsNullOrEmpty(this.cPersonCreatorView.Phone)))
               )
            {
                res = true;
            }

            return res;
        }
        public CreateCommand(CPersonCreatorView cPersonCreatorView, ICAccessDB idb)
        {
            this.cPersonCreatorView = cPersonCreatorView;
            this.idb = idb;
        }
        public bool Execute()
        {
            bool res = false;
            if (this.CanExecute())
            {
                CPerson cPerson = new CPerson(
                    this.cPersonCreatorView.Number
                    ,
                    this.cPersonCreatorView.Name
                    ,
                    this.cPersonCreatorView.Phone
                    );

                if (new CPeopleController(this.idb).New(cPerson))
                {
                    res = true;
                }
            }
            return res;
        }
    }

    public class UpdateCommand : IBaseCommand
    {
        private readonly CPersonEditroView cPersonEditroView;
        private readonly ICAccessDB idb;
        public bool CanExecute()
        {
            bool res = false;
            if (
                (this.cPersonEditroView.Number > 0)
                &&
                (!(string.IsNullOrEmpty(this.cPersonEditroView.Name)))
                &&
                (!(string.IsNullOrEmpty(this.cPersonEditroView.Phone)))
               )
            {
                res = true;
            }

            return res;
        }

        public UpdateCommand(CPersonEditroView cPersonEditroView, ICAccessDB idb)
        {
            this.cPersonEditroView = cPersonEditroView;
            this.idb = idb;
        }
        public bool Execute()
        {
            bool res = false;

            if (CanExecute())
            {
                int perNumber = this.cPersonEditroView.Number;
                CPerson cPerson = new CPerson(
                    this.cPersonEditroView.Number
                    ,
                    this.cPersonEditroView.Name
                    ,
                    this.cPersonEditroView.Phone
                    );

                if (new CPeopleController(this.idb).Update(perNumber, cPerson))
                {
                    res = true;
                }
            }
           
            return res;
        }
    }
}
