namespace Labs
{

	internal static class LABS_PDV_MAIN
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[MTAThread]
		static void Main()
		{
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			ApplicationConfiguration.Initialize();
			Application.Run(new LabsMainApp());
			//Inicializamos a lista de Aplicações
		}
	}
}
