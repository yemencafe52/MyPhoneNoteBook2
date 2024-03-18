namespace MyPhoneNoteBook2
{
    class CConfig
    {
        private static string dbPath = System.IO.Directory.GetCurrentDirectory() + "\\database\\db.accdb";
        private static string connectionString = $"provider=microsoft.ace.oledb.12.0;data source={dbPath}";

        internal static string DatabasePath => dbPath;
        internal static string ConnectionString => connectionString;
    }
}
