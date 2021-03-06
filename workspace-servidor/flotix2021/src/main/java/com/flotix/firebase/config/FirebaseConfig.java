package com.flotix.firebase.config;

import java.io.FileInputStream;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import com.google.auth.oauth2.GoogleCredentials;
import com.google.cloud.firestore.Firestore;
import com.google.firebase.FirebaseApp;
import com.google.firebase.FirebaseOptions;
import com.google.firebase.cloud.FirestoreClient;

/**
 * Configuracion de Firebase
 * 
 * @author Flor
 *
 */
@Configuration
public class FirebaseConfig {

	@SuppressWarnings("deprecation")
	@Bean
	public Firestore firestore() throws Exception {

		FileInputStream serviceAccount = new FileInputStream(
				"properties/credentials/flotix2021-firebase-adminsdk.json");

		FirebaseOptions options = new FirebaseOptions.Builder()
				.setCredentials(GoogleCredentials.fromStream(serviceAccount)).setStorageBucket("flotix2021.appspot.com")
				.build();

		FirebaseApp firebaseApp = FirebaseApp.initializeApp(options);

		return FirestoreClient.getFirestore(firebaseApp);
	}
}
