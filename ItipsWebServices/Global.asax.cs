using System;
using System.IO;
using System.Web;
using ItipsWebServices.Quartz;
using ItipsWebServices.Utility;
using NLog;
using OppmHelper.Utility;


namespace ItipsWebServices
{
    public class Global : HttpApplication
    {
        public static Logger NLogger = LogManager.GetCurrentClassLogger();

        //
        // Application globals
        //
        /// <summary>
        /// The oppm credentials
        /// </summary>
        public static ApplicationVariable<WebServiceCredentials> OppmCredentials = new ApplicationVariable<WebServiceCredentials>("OppmCredentials", WebServiceCredentials.NewCredentials);
   

        void Application_Start(Object sender, EventArgs e)
        {
            try
            {
                //System.Diagnostics.Debugger.Break();

                NLogger.Info("Starting ItipsWebServices Application");

                //
                // Crete folders we need for this solution extension
                //

                var scheduleDataFileName = Path.Combine(Path.Combine(Server.MapPath("~"), "Quartz"), "quartz_jobs.xml");
                //GlobalVariables.JobSchedules = Schedules.LoadFromFile(scheduleDataFileName);
                var scheduleXml = File.ReadAllText(scheduleDataFileName);
                GlobalVariables.JobSchedules = scheduleXml.FromXml<Schedules>();


                GlobalVariables.ScratchPad = Properties.Settings.Default.ScratchPad;
                if (GlobalVariables.ScratchPad.IsNullOrEmpty()) GlobalVariables.ScratchPad = Path.Combine(Server.MapPath("~"), "ScratchPad");

                Directory.CreateDirectory(Path.Combine(Server.MapPath("~"), "Logs"));
                var scratchPad = Directory.CreateDirectory(GlobalVariables.ScratchPad);
                foreach (var file in scratchPad.GetFiles()) file.Delete();
                foreach (var subDirectory in scratchPad.GetDirectories()) subDirectory.Delete(true);


                //
                // Get our web services credentials and save them in the application session
                //
                var webServiceCredentials = OppmCredentials.ValueOrDefault;
                webServiceCredentials.UserName = Properties.Settings.Default.WebServiceUser;
                webServiceCredentials.Password = Properties.Settings.Default.WebServicePassword;
                webServiceCredentials.ServerName = Environment.MachineName;
                OppmCredentials.Value = webServiceCredentials;

                //
                // Make sure we can login or why bother
                //
                var oppm = TestOppmLogin(webServiceCredentials);
                if (oppm == null)
                {
                    HttpRuntime.UnloadAppDomain();
                    return;
                }
                
                //
                // Make sure we can create files or why bother
                //
                var testFileAccess = TestFolderFileWrites();
                if (!testFileAccess)
                {
                    HttpRuntime.UnloadAppDomain();
                    return;
                }

                //
                // Start our schedular and save it in the application session.
                //
                GlobalVariables.Scheduler = JobScheduler.StartSchedular();
                NLogger.Info("Starting ItipsWebService Scheduler");

            }
            catch (Exception ex)
            {
                NLogger.Error(ex.Message);
                HttpRuntime.UnloadAppDomain();
            }
        }

        public Oppm TestOppmLogin(WebServiceCredentials webServiceCredentials)
        {
            try
            {
                NLogger.Info("Testing Oppm WebServiceCredentials");
                //
                // Test our login to OPPM via web services
                //
                var oppm = new Oppm(webServiceCredentials);
                oppm.Login();
                NLogger.Trace("OPPM token {0}", oppm.SeSecurity.SecurityToken);

                //
                // Logout
                //
                var token = oppm.SeSecurity.SecurityToken;
                oppm.Logout(token);
                NLogger.Info("Successful Oppm WebServiceCredentials");
                return oppm;
            }
            catch (Exception ex)
            {
                NLogger.Error(ex.Message);
                return null;
            }
        }

        public Boolean TestFolderFileWrites()
        {
            NLogger.Info("Starting File Folder Write Test");
            var fileName = Guid.NewGuid();

            try
            {
                var fullName = Path.Combine(GlobalVariables.ScratchPad, fileName.ToString());
                CreateDeleteEmptyFile(fullName);
            }
            catch (Exception ex)
            {
                NLogger.Error(ex.Message);
                return false;
            }

            try
            {
                //var fullName = Path.Combine(spreadSheetFolder, fileName.ToString());
               // CreateDeleteEmptyFile(fullName);
                //testCounter++;
            }
            catch (Exception ex)
            {
                NLogger.Error(ex.Message);
                return false;
            }

            NLogger.Info("Finished File Folder Write Test");
            return true;
        }

        public static void CreateDeleteEmptyFile(string filename)
        {
            File.Create(filename).Dispose();
            File.Delete(filename);
        }

    }
}