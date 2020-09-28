namespace CheckNetworkService
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
            this.CheckNetworkServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.CheckNetworkInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // CheckNetworkServiceProcessInstaller
            // 
            this.CheckNetworkServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.CheckNetworkServiceProcessInstaller.Password = null;
            this.CheckNetworkServiceProcessInstaller.Username = null;
            // 
            // CheckNetworkInstaller
            // 
            this.CheckNetworkInstaller.Description = "Service that checks network state";
            this.CheckNetworkInstaller.DisplayName = "CheckNetworkService";
            this.CheckNetworkInstaller.ServiceName = "NetworkService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.CheckNetworkServiceProcessInstaller,
            this.CheckNetworkInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller CheckNetworkServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller CheckNetworkInstaller;
    }
}