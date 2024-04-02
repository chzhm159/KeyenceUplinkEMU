namespace KeyenceUplinkEMU
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            log4net.Config.XmlConfigurator.Configure(new FileInfo("config/log4net.config"));


            Application.Run(new Form1());
        }
    }
}