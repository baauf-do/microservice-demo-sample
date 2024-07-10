namespace Shared.Configurations
{
    /// <summary>
    /// Write log to elasticsearch; files
    /// </summary>
    public class ConfigSettings
    {
        public bool WriteElastic { get; set; } = false;
        public bool WriteLogsFile { get; set; } = false;
    }
}
