package com.team.uical;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ProgressBar;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.Volley;
import com.google.firebase.database.DataSnapshot;
import com.google.firebase.database.DatabaseError;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;
import com.google.firebase.database.Query;
import com.google.firebase.database.ValueEventListener;
import com.vidyo.uical.R;

import org.json.JSONException;
import org.json.JSONObject;


public class JoinMeeting extends AppCompatActivity {
    EditText displayname,resourceID;
    Button join_meeting;
    String name,roomID,token;
    DatabaseReference reference;
    int flag=0,flag2=0;
    ProgressBar progressBar;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_join_meeting);
        initComponents();
    }
    private void initComponents() {
        displayname=findViewById(R.id.displayname);
        resourceID=findViewById(R.id.resourceID);
        join_meeting=findViewById(R.id.join_meeting);
        progressBar=findViewById(R.id.progressbar);
        reference= FirebaseDatabase.getInstance().getReference("Meetings");
        join_meeting.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                name=displayname.getText().toString().trim();
                roomID=resourceID.getText().toString().trim();
                if(!name.isEmpty() && !roomID.isEmpty()){
                    verifyRoomID(roomID,name);
                    progressBar.setVisibility(View.VISIBLE);
                }
            }
        });
    }

    private void addParticipant(String name,String roomID) {
       reference.child(roomID).child("Participants").child(name).child("name").setValue(name);
    }

    private void verifyRoomID(final String roomID, final String name) {
       Query query= reference.child(roomID);
        query.addValueEventListener(new ValueEventListener() {
            @Override
            public void onDataChange(@NonNull DataSnapshot snapshot) {
                flag=0;
                if(snapshot.exists() && flag==0){
                        generateToken(name,roomID);
                        addParticipant(name,roomID);
                    flag=1;
                }
                else if(!snapshot.exists() && flag==0){
                    new AlertDialog.Builder(JoinMeeting.this)
                            .setTitle("Invalid Room ID")
                            .setMessage("Please check Room ID & try again later")
                            .setNegativeButton(android.R.string.no, null)
                            .show();
                    flag=1;
                    progressBar.setVisibility(View.INVISIBLE);
                }
            }

            @Override
            public void onCancelled(@NonNull DatabaseError error) {

            }
        });
    }

    protected void generateToken(final String name, final String resID) {
        String URL="https://hab59.herokuapp.com/getToken?userName="+name;
        RequestQueue requestQueue= Volley.newRequestQueue(JoinMeeting.this);
        JsonObjectRequest objectRequest = new JsonObjectRequest(
                Request.Method.GET,
                URL,
                null,
                new Response.Listener<JSONObject>() {
                    @Override
                    public void onResponse(JSONObject response) {

                        try {
                           token=response.get("token").toString();
                        } catch (JSONException e) {
                            e.printStackTrace();
                        }

                        if(token!=null && flag2==0){
                            senddata(name,token,resID);
                            flag2=1;
                        }
                    }
                },
                new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        Toast.makeText(getApplicationContext(),"Error",Toast.LENGTH_LONG).show();
                    }
                }
        );
        requestQueue.add(objectRequest);
    }

    private void senddata(String name, String token, String resID) {
        if (!token.isEmpty()) {
            Intent intent= new Intent(getApplicationContext(),MainActivity.class);
            intent.putExtra("resourceId",resID);
            intent.putExtra("displayName",name);
            intent.putExtra("token",token);
            intent.putExtra("roomid",roomID);
            startActivity(intent);
            finish();
        }
    }
}
