namespace Shared.Configurations
{
    /// <summary>
    /// config connect to Database
    /// type multiple database
    /// </summary>
    public class DatabaseSettings
    {
        /// <summary>
        /// type of database
        /// vd: MySQL, SQL Server, MongoDB, Postgres, ...
        /// </summary>
        public string DBProvider { get; set; } = string.Empty;
        /// <summary>
        ///
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;
        /// <summary>
        /// database name
        /// </summary>
        public string DatabaseName { get; set; } = string.Empty;
    }
}
