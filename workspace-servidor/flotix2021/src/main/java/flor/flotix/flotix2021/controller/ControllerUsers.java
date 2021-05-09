package flor.flotix.flotix2021.controller;

import java.util.List;
import java.util.concurrent.ExecutionException;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;

import com.google.api.core.ApiFuture;
import com.google.cloud.firestore.Firestore;
import com.google.cloud.firestore.QueryDocumentSnapshot;
import com.google.cloud.firestore.QuerySnapshot;
import com.google.firebase.cloud.FirestoreClient;

import flor.flotix.flotix2021.response.bean.ErrorBean;
import flor.flotix.flotix2021.response.bean.ServerResponse;

@Controller
public class ControllerUsers {

	@RequestMapping(value = "/users", method = RequestMethod.POST)
	public @ResponseBody ServerResponse getUsers() {

		ServerResponse result = null;

		try {

			Firestore db = FirestoreClient.getFirestore();

			// asynchronously retrieve all users
			ApiFuture<QuerySnapshot> query = db.collection("users").get();
			// ...
			// query.get() blocks on response
			QuerySnapshot querySnapshot = query.get();
			List<QueryDocumentSnapshot> documents = querySnapshot.getDocuments();
			
			result = new ServerResponse();
			
			for (QueryDocumentSnapshot document : documents) {
				System.out.println("User: " + document.getId());
				System.out.println("User: " + document.getString("nombre"));
				
				result.setNombre(document.getString("nombre"));
			}
			
			ErrorBean error = new ErrorBean();
			error.setCode(0);
			error.setMessage("OK");
			
			result.setError(error);

		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (ExecutionException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		return result;
	}

}
