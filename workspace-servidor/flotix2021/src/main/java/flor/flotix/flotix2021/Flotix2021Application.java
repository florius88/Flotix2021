package flor.flotix.flotix2021;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;

import com.google.auth.oauth2.GoogleCredentials;
import com.google.firebase.FirebaseApp;
import com.google.firebase.FirebaseOptions;

@SpringBootApplication
public class Flotix2021Application {
	
	private static FirebaseApp defaultApp;

	public static void main(String[] args) {
		
		SpringApplication.run(Flotix2021Application.class, args);
		
		FileInputStream serviceAccount;
		try {

			if (null == defaultApp) {
				// Use a service account
				serviceAccount = new FileInputStream(
						"properties/credentials/prueba-50a4c-firebase-adminsdk-xp72j-4f83e1e49c.json");
				GoogleCredentials credentials = GoogleCredentials.fromStream(serviceAccount);
				FirebaseOptions options = new FirebaseOptions.Builder().setCredentials(credentials).build();
				defaultApp = FirebaseApp.initializeApp(options);

				System.out.println(defaultApp.getName());
			}

		} catch (FileNotFoundException e) {
			System.out.println("ERROR " + e.getMessage());
		} catch (IOException e) {
			System.out.println("ERROR " + e.getMessage());
		}
		
		
		
		
	}

}
