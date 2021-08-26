using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;

namespace MailingDemon
{
    /// <summary>
    ///     Summary description for ProjectInstaller.
    /// </summary>
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        private readonly ServiceProcessInstaller serviceProcessInstaller;

        public ProjectInstaller(ServiceProcessInstaller serviceProcessInstaller)
        {
            this.serviceProcessInstaller = serviceProcessInstaller;
            InitializeComponent();

            var mailingDaemonProcessInstaller = new ServiceProcessInstaller();
            var mailingDaemonInstaller = new ServiceInstaller();

            //
            // MailingDaemonProcessInstaller
            //
            mailingDaemonProcessInstaller.Account = ServiceAccount.LocalSystem;
            mailingDaemonProcessInstaller.Password = null;
            mailingDaemonProcessInstaller.Username = null;

            //
            // MailingDaemonInstaller
            //
            mailingDaemonInstaller.ServiceName = "MailingDemonRestChild";
            mailingDaemonInstaller.StartType = ServiceStartMode.Automatic;

            //
            // ProjectInstaller
            //
            Installers.AddRange(
                new Installer[]
                {
                    mailingDaemonProcessInstaller,
                    mailingDaemonInstaller
                });
        }

        #region GetCorSystemDirectory

        [DllImport("mscoree.dll")]
        private static extern int GetCORSystemDirectory(
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pbuffer,
            int cchBuffer,
            ref int dwlength);

        #endregion

        #region Component Designer generated code

        protected override void OnBeforeInstall(IDictionary savedState)
        {
            // Удаляем старую версию сервиса
            try
            {
                //Получаем директорию .Net Framwork
                int MAX_PATH = 260;
                StringBuilder sb = new StringBuilder(MAX_PATH);
                GetCORSystemDirectory(sb, MAX_PATH, ref MAX_PATH);

                //Получаем папку приложения
                string appDirectory =
                    this.Context.Parameters["assemblypath"].Replace("mailingdaemon.exe", string.Empty);

                Process p = new Process();
                ProcessStartInfo psiService = new ProcessStartInfo();
                psiService = new ProcessStartInfo(
                    "installutil.exe", string.Format(" /u \"{0}{1}", appDirectory, "mailingdaemon.exe\""));
                psiService.UseShellExecute = true;
                psiService.WorkingDirectory = sb.ToString();
                psiService.WindowStyle = ProcessWindowStyle.Hidden;

                p = Process.Start(psiService);
                p.WaitForExit(300000);
            }
            catch
            {
            }

            base.OnBeforeInstall(savedState);

            //this.MailingDaemonProcessInstaler = new ServiceProcessInstaller();
            //this.MailingDaemonInstaller = new ServiceInstaller();

            ////
            //// MailingDaemonProcessInstaler
            ////
            //this.MailingDaemonProcessInstaler.Account = ServiceAccount.LocalSystem;
            //this.MailingDaemonProcessInstaler.Password = null;
            //this.MailingDaemonProcessInstaler.Username = null;

            ////
            //// MailingDaemonInstaller
            ////
            //this.MailingDaemonInstaller.ServiceName = "MailingDemon2";
            //this.MailingDaemonInstaller.StartType = ServiceStartMode.Automatic;

            ////
            //// ProjectInstaller
            ////
            //this.Installers.AddRange(new Installer[] {
            //                                                 this.MailingDaemonProcessInstaler,
            //                                                 this.MailingDaemonInstaller});

            // Настройки указанные пользователем
            // serviceProcessInstaller
            if (Context.Parameters["ACCOUNTTYPE"] != string.Empty || Context.Parameters["ACCOUNTTYPE"] != null)
            {
                try
                {
                    // берется из ServiceAccount: LocalService, LocalSystem, NetworkService, User
                    this.serviceProcessInstaller.Account =
                        (ServiceAccount) Enum.Parse(typeof(ServiceAccount), this.Context.Parameters["ACCOUNTTYPE"],
                            true);

                    if (this.Context.Parameters.ContainsKey("USERNAME") &&
                        (this.Context.Parameters["USERNAME"] != null ||
                         this.Context.Parameters["USERNAME"] != string.Empty))
                        this.serviceProcessInstaller.Username = this.Context.Parameters["USERNAME"];

                    if (this.Context.Parameters.ContainsKey("PASSWORD") &&
                        (this.Context.Parameters["PASSWORD"] != null ||
                         this.Context.Parameters["PASSWORD"] != string.Empty))
                        this.serviceProcessInstaller.Password = this.Context.Parameters["PASSWORD"];
                }
                catch
                {
                }
            }
        }

        public override void Install(IDictionary savedState)
        {
            base.Install(savedState);
            //MessageBox.Show(this.Context.Parameters["assemblypath"]);
            //MessageBox.Show(this.Context.Parameters["USERNAME"]);
            //MessageBox.Show(this.Context.Parameters["PASSWORD"]);
        }

        #endregion
    }
}
