namespace MyPhoneNoteBook2
{
    class CHelper
    {
        internal static bool CheckDatabaseFile()
        {
            bool res = false;
            if (System.IO.File.Exists(CConfig.DatabasePath))
            {
                res = true;
            }
            return res;
        }

        internal static bool CheckDBEngine()
        {
            bool res = false;
            return res;
        }

        internal static bool CheckDBConnection()
        {
            bool res = false;
            return res;
        }

        internal static bool InstallDB()
        {
            bool res = false;
            return res;
        }

        internal static bool BackupDB(string dstPath)
        {
            bool res = false;
            return res;
        }

        internal static bool RestoreDB(string srcPath)
        {
            bool res = false;
            return res;
        }
    }
}
