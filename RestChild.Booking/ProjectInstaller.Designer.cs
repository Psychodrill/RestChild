using System.ServiceProcess;

namespace RestChild.Booking
{
	partial class ProjectInstaller
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.serviceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
			this.BookingServiceInstaller = new System.ServiceProcess.ServiceInstaller();
			this.RestChildIndexerServiceInstaller = new System.ServiceProcess.ServiceInstaller();
			// 
			// serviceProcessInstaller
			// 
			this.serviceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
			this.serviceProcessInstaller.Password = null;
			this.serviceProcessInstaller.Username = null;
			// 
			// RestChildBookingServiceInstaller
			// 
			this.BookingServiceInstaller.DisplayName = "Сервер для АИС Отдых";
			this.BookingServiceInstaller.ServiceName = "RestChildBooking";
			// 
			// RestChildIndexerServiceInstaller
			// 
			this.RestChildIndexerServiceInstaller.DisplayName = "Сервер Индексирования для АИС Отдых";
			this.RestChildIndexerServiceInstaller.ServiceName = "RestChildIndexerService";
			// 
			// ProjectInstaller
			// 
			this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstaller,
            this.BookingServiceInstaller,
            this.RestChildIndexerServiceInstaller});

		}

		#endregion

		private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller;
		private System.ServiceProcess.ServiceInstaller BookingServiceInstaller;
		private System.ServiceProcess.ServiceInstaller RestChildIndexerServiceInstaller;
	}
}
