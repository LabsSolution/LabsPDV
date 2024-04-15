namespace LabsPDV
{
    internal static class LABS_PDV_MAIN
    {
        //JANELAS INTERNAS DE REFERÊNCIA
        static RDP RegistradorDeProdutos = new();
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
			Application.Run(new PDV());
        }
        //
        public static void RunRDP()
        {
            if(!RegistradorDeProdutos.IsDisposed) { RegistradorDeProdutos.Show(); }
            else { RegistradorDeProdutos = new(); RegistradorDeProdutos.Show(); }
            
        }
    }
}