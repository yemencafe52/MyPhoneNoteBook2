using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;

namespace MyPhoneNoteBook2.Models
{
    public class CPerson
    {
        private int perNumber;
        private string perName;
        private string perPhone;

        public CPerson(int perNumber, string perName, string perPhone)
        {
            this.perNumber = perNumber;
            this.perName = perName;
            this.perPhone = perPhone;
        }

        public int PerNumber { get => perNumber;  }
        public string PerName { get => perName; }
        public string PerPhone { get => perPhone;}
    }

    public class CPeopleController
    {
        public delegate void UpdteDB(byte cmd, CPerson cPerson);
        public static event UpdteDB OnUpdate;
        /***********************************/
        private readonly ICAccessDB _idb;
        public CPeopleController(ICAccessDB idb)
        {
            _idb = idb;
        }
        public bool New(CPerson cPerson)
        {
            bool res = false;

            string sql = string.Format("insert into tblPeople " +
                "(" +
                    "perNumber" +
                    "," +
                    "perName" +
                    "," +
                    "perPhone" +
                ")" +
                " " +
                "values" +
                "(" +
                    "{0}" +
                    "," +
                    "'{1}'" +
                    "," +
                    "'{2}'" +
                ")"
                ,
                new object[]
                {
                    cPerson.PerNumber
                    ,
                    cPerson.PerName
                    ,
                    cPerson.PerPhone
                });

            res = this._idb.ExecuteNoneQuery(sql) > 0 ? true : false;

            if (res)
            {
                if (!(OnUpdate is null))
                {
                    OnUpdate(1, cPerson);
                }
            }

            return res;
        }
        public bool Query(int number, out CPerson cPerson)
        {
            bool res = false;

            cPerson = null;

            string sql = string.Format("select perNumber,perName,perPhone from tblPeople where perNumber={0}", new object[] { number });

            System.Data.DataTable dataTable = new DataTable("tblTemp");

            if (this._idb.ExecuteQuery(sql, ref dataTable) > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    cPerson = (new CPerson(
                         Convert.ToInt32(dataTable.Rows[i][0].ToString())
                         ,
                         (dataTable.Rows[i][1].ToString())
                         ,
                         (dataTable.Rows[i][2].ToString())
                         ));
                }

                res = true;
            }

            return res;
        }
        public bool Update(int number, CPerson cPerson)
        {
            bool res = false;

            string sql = string.Format("update tblPeople set" +
                " " +
                    "perName='{0}'" +
                    "," +
                    "perPhone='{1}'" +
                    " " +
                    "where" +
                    " " +
                    "perNumber={2}"
                ,
                new object[]
                {
                    cPerson.PerName
                    ,
                    cPerson.PerPhone
                    ,
                    number
                });

            res = this._idb.ExecuteNoneQuery(sql) > 0 ? true : false;

            if (res)
            {
                if (!(OnUpdate is null))
                {
                    OnUpdate(2, cPerson);
                }
            }

            return res;
        }

        public bool Delete(int number)
        {
            bool res = false;

            CPerson cPerson;
            if (this.Query(number, out cPerson))
            {
                string sql = $"delete from tblPeople where perNumber={number}";
                res = this._idb.ExecuteNoneQuery(sql) > 0 ? true : false;

                if (res)
                {
                    if (!(OnUpdate is null))
                    {
                        OnUpdate(3, cPerson);
                    }
                }
            }
            return res;
        }

        public bool Search(string txt, ref List<CPerson> cPeople)
        {
            bool res = false;
            string sql = string.Format("select perNumber,perName,perPhone from tblPeople where perName like('%{0}%')", new object[] { txt });

            System.Data.DataTable dataTable = new DataTable("tblTemp");

            if (this._idb.ExecuteQuery(sql, ref dataTable) > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    cPeople.Add(new CPerson(
                        Convert.ToInt32(dataTable.Rows[i][0].ToString())
                        ,
                        (dataTable.Rows[i][1].ToString())
                        ,
                        (dataTable.Rows[i][2].ToString())
                        ));
                }

                res = true;
            }

            return res;
        }
    }
}
