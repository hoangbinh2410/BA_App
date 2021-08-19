package mono.android.app;

public class ApplicationRegistration {

	public static void registerApplications ()
	{
				// Application and Instrumentation ACWs must be registered first.
		mono.android.Runtime.register ("BA_App.Droid.MainApplication, BA_App.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", crc64d469f3afa8089126.MainApplication.class, crc64d469f3afa8089126.MainApplication.__md_methods);
		
	}
}
