using System;
using System.Windows.Forms;
using DataLayer;
using MyPhoneNoteBook2.MiddleViewLayers;
using MyPhoneNoteBook2.Views;

namespace MyPhoneNoteBook2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            CMainView mv;

            //FrmMain frmMain;
            //for (int i = 0; i < 10; i++)
            //{
            //    mv = new CMainView(new CAccessDB(CConfig.ConnectionString));
            //    frmMain = new FrmMain(
            //        mv
            //        ,
            //        new CPersonCreatorView(new CAccessDB(CConfig.ConnectionString))
            //        ,
            //        new CPersonEditroView(mv, new CAccessDB(CConfig.ConnectionString))
            //        );

            //    frmMain.Show();
            //}

            mv = new CMainView(new CAccessDB(CConfig.ConnectionString));
            Application.Run(

                  new FrmMain(
                      mv
                      ,
                      new CPersonCreatorView(new CAccessDB(CConfig.ConnectionString))
                      ,
                      new CPersonEditroView(mv, new CAccessDB(CConfig.ConnectionString))
                      )
                  );
        }
    }
}
