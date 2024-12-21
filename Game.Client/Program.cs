using Velopack;

namespace Game.Client
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            VelopackApp.Build().Run();
            //UpdateMyApp().Wait();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }

        private static async Task UpdateMyApp()
        {
            var mgr = new UpdateManager("https://www.appnicity.com/loc/updates/releases.win.json");

            // check for new version
            try
            {
                var newVersion = await mgr.CheckForUpdatesAsync();

                if (newVersion == null)
                    return; // no update available

                // download new version
                await mgr.DownloadUpdatesAsync(newVersion);

                // install new version and restart app
                mgr.ApplyUpdatesAndRestart(newVersion);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}