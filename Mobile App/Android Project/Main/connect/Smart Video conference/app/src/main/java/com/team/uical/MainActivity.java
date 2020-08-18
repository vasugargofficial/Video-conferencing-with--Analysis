package com.team.uical;

import android.Manifest;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.content.res.AssetFileDescriptor;
import android.content.res.Configuration;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.ImageFormat;
import android.graphics.Rect;
import android.graphics.YuvImage;
import android.media.Image;
import android.net.Uri;
import android.os.Build;
import android.os.Bundle;
import android.view.ViewTreeObserver;
import android.widget.TextView;
import android.app.Activity;
import android.view.View;
import android.view.WindowManager;
import android.widget.LinearLayout;
import android.widget.ToggleButton;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;
import androidx.camera.core.CameraSelector;
import androidx.camera.core.ImageAnalysis;
import androidx.camera.core.ImageCapture;
import androidx.camera.core.ImageProxy;
import androidx.camera.core.Preview;
import androidx.camera.lifecycle.ProcessCameraProvider;
import androidx.camera.view.PreviewView;
import androidx.core.app.ActivityCompat;
import androidx.core.content.ContextCompat;


import java.io.ByteArrayOutputStream;
import java.io.FileInputStream;
import java.io.IOException;
import java.nio.ByteBuffer;
import java.nio.MappedByteBuffer;
import java.nio.channels.FileChannel;
import java.util.Collections;
import java.util.List;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.Executor;
import java.util.concurrent.Executors;

import com.google.android.gms.tasks.OnFailureListener;
import com.google.android.gms.tasks.OnSuccessListener;
import com.google.android.gms.tasks.Task;
import com.google.common.util.concurrent.ListenableFuture;
import com.google.firebase.database.DataSnapshot;
import com.google.firebase.database.DatabaseError;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;
import com.google.firebase.database.Query;
import com.google.firebase.database.ValueEventListener;
import com.google.mlkit.vision.common.InputImage;
import com.google.mlkit.vision.face.Face;
import com.google.mlkit.vision.face.FaceDetection;
import com.google.mlkit.vision.face.FaceDetector;
import com.google.mlkit.vision.face.FaceDetectorOptions;
import com.team.uical.models.ModelPrediction;
import com.vidyo.VidyoClient.Connector.ConnectorPkg;
import com.vidyo.VidyoClient.Connector.Connector;
import com.vidyo.VidyoClient.Device.Device;
import com.vidyo.VidyoClient.Device.LocalCamera;
import com.vidyo.VidyoClient.Endpoint.LogRecord;
import com.vidyo.VidyoClient.NetworkInterface;
import com.vidyo.uical.R;

import org.tensorflow.lite.Interpreter;

public class MainActivity extends AppCompatActivity implements
        View.OnClickListener,
        Connector.IConnect,
        Connector.IRegisterLogEventListener,
        Connector.IRegisterNetworkInterfaceEventListener,
        Connector.IRegisterLocalCameraEventListener,
        IVideoFrameListener {

    // Define the various states of this application.
    enum VidyoConnectorState {
        Connecting,
        Connected,
        Disconnecting,
        Disconnected,
        DisconnectedUnexpected,
        Failure,
        FailureInvalidResource
    }
    // Map the application state to the status to display in the toolbar.
    private static final Map<VidyoConnectorState, String> mStateDescription = new HashMap<VidyoConnectorState, String>() {{
        put(VidyoConnectorState.Connecting, "Connecting...");
        put(VidyoConnectorState.Connected, "Connected");
        put(VidyoConnectorState.Disconnecting, "Disconnecting...");
        put(VidyoConnectorState.Disconnected, "Disconnected");
        put(VidyoConnectorState.DisconnectedUnexpected, "Unexpected disconnection");
        put(VidyoConnectorState.Failure, "Connection failed");
        put(VidyoConnectorState.FailureInvalidResource, "Invalid Resource ID");
    }};

    // Helps check whether app has permission to access what is declared in its manifest.
    // - Permissions from app's manifest that have a "protection level" of "dangerous".
    private static final String[] mPermissions = new String[] {
            Manifest.permission.CAMERA,
            Manifest.permission.RECORD_AUDIO,
            Manifest.permission.WRITE_EXTERNAL_STORAGE,
            Manifest.permission.READ_PHONE_STATE
    };
    // - This arbitrary, app-internal constant represents a group of requested permissions.
    // - For simplicity, this app treats all desired permissions as part of a single group.
    private final int PERMISSIONS_REQUEST_ALL = 1988;

    private VidyoConnectorState mVidyoConnectorState = VidyoConnectorState.Disconnected;
    private Logger mLogger = Logger.getInstance();
    private Connector mVidyoConnector = null;
    private LocalCamera mLastSelectedCamera = null;
    private ToggleButton mToggleConnectButton;
    private ToggleButton mMicrophonePrivacyButton;
    private ToggleButton mCameraPrivacyButton;

    private Executor executor = Executors.newSingleThreadExecutor();
    PreviewView mPreviewView;
    int Datacounter=1;
    protected Interpreter yawn,expression;
    String resultofyawn,resultofexpression;
    Bitmap facedetectImage;
    List<String> facedetectlist = new ArrayList<>();
    List<String> yawnlist = new ArrayList<>(),expressionlist= new ArrayList<>();
    private  String roomid;
    private boolean nodepresent= false;
    String displayname;

    private LinearLayout mToolbarLayout;
    private TextView mToolbarStatus;
    private VideoFrameLayout mVideoFrame;
    private boolean mHideConfig = false;
    private boolean mAutoJoin = false;
    private boolean mAllowReconnect = true;
    private boolean mCameraPrivacy = false;
    private boolean mMicrophonePrivacy = false;
    private boolean mEnableDebug = false;
    private String mReturnURL = null;
    private String mExperimentalOptions = null;
    private boolean mRefreshSettings = true;
    private boolean mDevicesSelected = true;
    private ViewTreeObserver.OnGlobalLayoutListener mOnGlobalLayoutListener = null;
    private boolean mVidyoCloudJoin = false;
    private String mPortal;  // Used for VidyoCloud systems, not Vidyo.io
    private String mRoomKey; // Used for VidyoCloud systems, not Vidyo.io
    private String mRoomPin; // Used for VidyoCloud systems, not Vidyo.io

    /*
     *  Operating System Events
     */

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        mLogger.Log("onCreate");
        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_main);
        mPreviewView = findViewById(R.id.previewView);

        mToolbarLayout = (LinearLayout) findViewById(R.id.toolbarLayout);
        mVideoFrame = (VideoFrameLayout) findViewById(R.id.videoFrame);
        mVideoFrame.Register(this);
        mToolbarStatus = (TextView) findViewById(R.id.toolbarStatusText);
        // Set the onClick listeners for the buttons
        mToggleConnectButton = (ToggleButton) findViewById(R.id.connect);
        mToggleConnectButton.setOnClickListener(this);
        mMicrophonePrivacyButton = (ToggleButton) findViewById(R.id.microphone_privacy);
        mMicrophonePrivacyButton.setOnClickListener(this);
        mCameraPrivacyButton = (ToggleButton) findViewById(R.id.camera_privacy);
        mCameraPrivacyButton.setOnClickListener(this);
        ToggleButton button = (ToggleButton) findViewById(R.id.camera_switch);
        button.setOnClickListener(this);
        button = (ToggleButton) findViewById(R.id.toggle_debug);
        button.setOnClickListener(this);

        // Set the application's UI context to this activity.
        ConnectorPkg.setApplicationUIContext(this);

        // Initialize the VidyoClient library - this should be done once in the lifetime of the application.
        if (ConnectorPkg.initialize()) {
            // Construct Connector and register for events.
            try {
                mLogger.Log("Constructing Connector");
                String logLevel = mEnableDebug? "warning debug@VidyoClient all@LmiPortalSession all@LmiPortalMembership info@LmiResourceManagerUpdates " +
                        "info@LmiPace info@LmiIce all@LmiSignaling": "warning info@VidyoClient info@LmiPortalSession info@LmiPortalMembership " +
                        "info@LmiResourceManagerUpdates info@LmiPace info@LmiIce";
                mVidyoConnector = new Connector(mVideoFrame,
                        Connector.ConnectorViewStyle.VIDYO_CONNECTORVIEWSTYLE_Default,
                        7,
                        logLevel,
                        "",
                        0);
                initializeInterpreter();
                roomid= getIntent().getStringExtra("roomid");
                mToggleConnectButton.setChecked(true);
                mVidyoConnector.setCameraPrivacy(true);
                toggleConnect();
                // Register for local camera events
                if (!mVidyoConnector.registerLocalCameraEventListener(this)) {
                    mLogger.Log("registerLocalCameraEventListener failed");
                }
                // Register for network interface events
                if (!mVidyoConnector.registerNetworkInterfaceEventListener(this)) {
                    mLogger.Log("registerNetworkInterfaceEventListener failed");
                }
                // Register for log events
                if (!mVidyoConnector.registerLogEventListener(this, "info@VidyoClient info@VidyoConnector warning")) {
                    mLogger.Log("registerLogEventListener failed");
                }

                // Beginning in Android 6.0 (API level 23), users grant permissions to an app while
                // the app is running, not when they install the app. Check whether app has permission
                // to access what is declared in its manifest.
                if (Build.VERSION.SDK_INT > 22) {
                    List<String> permissionsNeeded = new ArrayList<>();
                    for (String permission : mPermissions) {
                        // Check if the permission has already been granted.
                        if (ContextCompat.checkSelfPermission(this, permission) != PackageManager.PERMISSION_GRANTED)
                            permissionsNeeded.add(permission);
                    }
                    if (permissionsNeeded.size() > 0) {
                        // Request any permissions which have not been granted. The result will be called back in onRequestPermissionsResult.
                        ActivityCompat.requestPermissions(this, permissionsNeeded.toArray(new String[0]), PERMISSIONS_REQUEST_ALL);
                    } else {
                        // Begin listening for video view size changes.
                        this.startVideoViewSizeListener();
                    }
                } else {
                    // Begin listening for video view size changes.
                    this.startVideoViewSizeListener();
                }
            } catch (Exception e) {
                mLogger.Log("Connector Construction failed");
                mLogger.Log(e.getMessage());
            }
        }
    }

    private void startCamera() {

        final ListenableFuture<ProcessCameraProvider> cameraProviderFuture = ProcessCameraProvider.getInstance(this);

        cameraProviderFuture.addListener(new Runnable() {
            @Override
            public void run() {
                try {

                    ProcessCameraProvider cameraProvider = cameraProviderFuture.get();
                    bindPreview(cameraProvider);

                } catch (ExecutionException | InterruptedException e) {
                    // No errors need to be handled for this Future.
                    // This should never be reached.
                }
            }
        }, ContextCompat.getMainExecutor(this));
    }

    void bindPreview(@NonNull ProcessCameraProvider cameraProvider) {

        Preview preview = new Preview.Builder()
                .build();

        CameraSelector cameraSelector = new CameraSelector.Builder()
                .requireLensFacing(CameraSelector.LENS_FACING_FRONT)
                .build();

        ImageAnalysis imageAnalysis =
                new ImageAnalysis.Builder()
                        .setBackpressureStrategy(ImageAnalysis.STRATEGY_KEEP_ONLY_LATEST)
                        .build();

        imageAnalysis.setAnalyzer(executor, new ImageAnalysis.Analyzer() {
            @Override
            public void analyze(@NonNull ImageProxy image) {
                face_detector(convertBitmap(image),image.getImageInfo().getRotationDegrees());
                try {
                    Thread.sleep(2500);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
                image.close();
                updatedata();
            }
        });

        ImageCapture.Builder builder = new ImageCapture.Builder();

        final ImageCapture imageCapture = builder
                .setTargetRotation(this.getWindowManager().getDefaultDisplay().getRotation())
                .build();

        preview.setSurfaceProvider(mPreviewView.createSurfaceProvider());
        cameraProvider.bindToLifecycle(this,cameraSelector, preview, imageAnalysis, imageCapture);
    }
    @Override
    protected void onNewIntent(Intent intent) {
        mLogger.Log("onNewIntent");
        super.onNewIntent(intent);

        // Set the refreshSettings flag so the app settings are refreshed in onStart
        mRefreshSettings = true;

        // New intent was received so set it to use in onStart
        setIntent(intent);
    }

    @Override
    protected void onStart() {
        mLogger.Log("onStart");
        super.onStart();

        // Initialize or refresh the app settings.
        // When app is first launched, mRefreshSettings will always be true.
        // Each successive time that onStart is called, app is coming back to foreground so check if the
        // settings need to be refreshed again, as app may have been launched via URI.
        if (mRefreshSettings &&
            mVidyoConnectorState != VidyoConnectorState.Connected &&
            mVidyoConnectorState != VidyoConnectorState.Connecting) {

            Intent intent = getIntent();
            Uri uri = intent.getData();

            // Check if app was launched via URI
            if (uri != null) {

                mReturnURL = uri.getQueryParameter("returnURL");
                mHideConfig = uri.getBooleanQueryParameter("hideConfig", false);
                mAutoJoin = uri.getBooleanQueryParameter("autoJoin", false);
                mAllowReconnect = uri.getBooleanQueryParameter("allowReconnect", true);
                mCameraPrivacy = uri.getBooleanQueryParameter("cameraPrivacy", false);
                mMicrophonePrivacy = uri.getBooleanQueryParameter("microphonePrivacy", false);
                mEnableDebug = uri.getBooleanQueryParameter("enableDebug", false);
                mExperimentalOptions = uri.getQueryParameter("experimentalOptions");

                ///////////////////////////////////////////////////////////////////////////////////////
                // Note: the following parameters are used to connect to VidyoCloud systems, not Vidyo.io.
                mVidyoCloudJoin = (uri.getHost() != null) && uri.getHost().equalsIgnoreCase("join");
                if (mVidyoCloudJoin) {
                    // Do not display the Vidyo.io form in VidyoCloud mode.
                    mHideConfig = true;

                }
                ///////////////////////////////////////////////////////////////////////////////////////
            } else {
                // If this app was launched by a different app, then get any parameters; otherwise use default settings.
                mReturnURL = intent.hasExtra("returnURL") ? intent.getStringExtra("returnURL") : null;
                mHideConfig = intent.getBooleanExtra("hideConfig", false);
                mAutoJoin = intent.getBooleanExtra("autoJoin", false);
                mAllowReconnect = intent.getBooleanExtra("allowReconnect", true);
                mCameraPrivacy = intent.getBooleanExtra("cameraPrivacy", false);
                mMicrophonePrivacy = intent.getBooleanExtra("microphonePrivacy", false);
                mEnableDebug = intent.getBooleanExtra("enableDebug", false);
                mExperimentalOptions = intent.hasExtra("experimentalOptions") ? intent.getStringExtra("experimentalOptions") : null;
                mVidyoCloudJoin = false;
            }

            mLogger.Log("onStart: hideConfig = " + mHideConfig + ", autoJoin = " + mAutoJoin + ", allowReconnect = " + mAllowReconnect + ", enableDebug = " + mEnableDebug);

            // Hide the form if hideConfig enabled.
            if (mHideConfig) {

            }

            // Apply the app settings.
            this.applySettings();
        }
        mRefreshSettings = false;
    }

    @Override
    protected void onResume() {
        mLogger.Log("onResume");
        super.onResume();
    }

    @Override
    protected void onPause() {
        mLogger.Log("onPause");
        super.onPause();
    }

    @Override
    protected void onStop() {
        mLogger.Log("onStop");
        super.onStop();

        if (mVidyoConnector != null) {
            if (mVidyoConnectorState != VidyoConnectorState.Connected &&
                mVidyoConnectorState != VidyoConnectorState.Connecting) {
                // Not connected/connecting to a resource.
                // Release camera, mic, and speaker from this app while backgrounded.
                mVidyoConnector.selectLocalCamera(null);
                mVidyoConnector.selectLocalMicrophone(null);
                mVidyoConnector.selectLocalSpeaker(null);
                mDevicesSelected = false;
            }
            mVidyoConnector.setMode(Connector.ConnectorMode.VIDYO_CONNECTORMODE_Background);
        }
    }

    @Override
    protected void onRestart() {
        mLogger.Log("onRestart");
        super.onRestart();

        if (mVidyoConnector != null) {
            mVidyoConnector.setMode(Connector.ConnectorMode.VIDYO_CONNECTORMODE_Foreground);

            if (!mDevicesSelected) {
                // Devices have been released when backgrounding (in onStop). Re-select them.
                mDevicesSelected = true;

                // Select the previously selected local camera and default mic/speaker
                mVidyoConnector.selectLocalCamera(mLastSelectedCamera);
                mVidyoConnector.selectDefaultMicrophone();
                mVidyoConnector.selectDefaultSpeaker();

                // Reestablish camera and microphone privacy states
                mVidyoConnector.setCameraPrivacy(mCameraPrivacy);
                mVidyoConnector.setMicrophonePrivacy(mMicrophonePrivacy);
            }
        }
    }

    @Override
    protected void onDestroy() {
        mLogger.Log("onDestroy");
        super.onDestroy();

        // Release device resources
        mLastSelectedCamera = null;
        if (mVidyoConnector != null) {
            mVidyoConnector.selectLocalCamera(null);
            mVidyoConnector.selectLocalMicrophone(null);
            mVidyoConnector.selectLocalSpeaker(null);
        }

        // Connector will be destructed upon garbage collection.
        mVidyoConnector = null;

        ConnectorPkg.setApplicationUIContext(null);

        // Uninitialize the VidyoClient library - this should be done once in the lifetime of the application.
        ConnectorPkg.uninitialize();

        // Remove the global layout listener on the video frame.
        if (mOnGlobalLayoutListener != null) {
            mVideoFrame.getViewTreeObserver().removeOnGlobalLayoutListener(mOnGlobalLayoutListener);
        }
    }

    // The device interface orientation has changed
    @Override
    public void onConfigurationChanged(Configuration newConfig) {
        mLogger.Log("onConfigurationChanged");
        super.onConfigurationChanged(newConfig);
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, String[] permissions, int[] grantResults) {
        mLogger.Log("onRequestPermissionsResult: number of requested permissions = " + permissions.length);

        // If the expected request code is received, begin rendering video.
        if (requestCode == PERMISSIONS_REQUEST_ALL) {
            for (int i = 0; i < permissions.length; ++i)
                mLogger.Log("permission: " + permissions[i] + " " + grantResults[i]);

            // Begin listening for video view size changes.
            this.startVideoViewSizeListener();
        } else {
            mLogger.Log("ERROR! Unexpected permission requested. Video will not be rendered.");
        }
    }

    // Listen for UI changes to the view where the video is rendered.
    private void startVideoViewSizeListener() {
        mLogger.Log("startVideoViewSizeListener");

        // Render the video each time that the video view (mVideoFrame) is resized. This will
        // occur upon activity creation, orientation changes, and when foregrounding the app.
        ViewTreeObserver viewTreeObserver = mVideoFrame.getViewTreeObserver();
        if (viewTreeObserver.isAlive()) {
            viewTreeObserver.addOnGlobalLayoutListener(new ViewTreeObserver.OnGlobalLayoutListener() {
                @Override
                public void onGlobalLayout() {
                    // Specify the width/height of the view to render to.
                    mLogger.Log("showViewAt: width = " + mVideoFrame.getWidth() + ", height = " + mVideoFrame.getHeight());
                    mVidyoConnector.showViewAt(mVideoFrame, 0, 0, mVideoFrame.getWidth(), mVideoFrame.getHeight());
                    mOnGlobalLayoutListener = this;
                }
            });
        } else {
            mLogger.Log("ERROR in startVideoViewSizeListener! Video will not be rendered.");
        }
    }

    // Apply some of the app settings
    private void applySettings() {
        if (mVidyoConnector != null) {
            // If enableDebug is configured then enable debugging
            if (mEnableDebug) {
                mVidyoConnector.enableDebug(7776, "warning info@VidyoClient info@VidyoConnector");

            } else {
                mVidyoConnector.disableDebug();
            }

            // If cameraPrivacy is configured then mute the camera
            mCameraPrivacyButton.setChecked(false); // reset state
            if (mCameraPrivacy) {
                mCameraPrivacyButton.performClick();
            }

            // If microphonePrivacy is configured then mute the microphone
            mMicrophonePrivacyButton.setChecked(false); // reset state
            if (mMicrophonePrivacy) {
                mMicrophonePrivacyButton.performClick();
            }

            // Set experimental options if any exist
            if (mExperimentalOptions != null) {
                ConnectorPkg.setExperimentalOptions(mExperimentalOptions);
            }

            // If configured to auto-join, then simulate a click of the toggle connect button
            if (mAutoJoin) {
                mToggleConnectButton.performClick();
            }
        }
    }

    // The state of the VidyoConnector connection changed, reconfigure the UI.
    // If connected, dismiss the controls layout
    private void changeState(VidyoConnectorState state) {
        mLogger.Log("changeState: " + state.toString());

        mVidyoConnectorState = state;

        // Execute this code on the main thread since it is updating the UI layout.
        runOnUiThread(new Runnable() {
            @Override
            public void run() {
                // Set the status text in the toolbar.
                mToolbarStatus.setText(mStateDescription.get(mVidyoConnectorState));

                // Depending on the state, do a subset of the following:
                // - update the toggle connect button to either start call or end call image: mToggleConnectButton
                // - display toolbar in case it is hidden: mToolbarLayout
                // - show/hide the connection spinner: mConnectionSpinner
                // - show/hide the input form: mControlsLayout
                switch (mVidyoConnectorState) {
                    case Connecting:
                        mToggleConnectButton.setChecked(true);
                        break;

                    case Connected:
                        mToggleConnectButton.setChecked(true);

                        startCamera();
                        // Keep the device awake if connected.
                        getWindow().addFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);
                        break;

                    case Disconnecting:
                        // The button just switched to the callStart image.
                        // Change the button back to the callEnd image because do not want to assume that the Disconnect
                        // call will actually end the call. Need to wait for the callback to be received
                        // before swapping to the callStart image.
                        mToggleConnectButton.setChecked(true);
                        break;

                    case Disconnected:
                        Intent intent= new Intent(MainActivity.this,JoinMeeting.class);
                        startActivity(intent);
                        finish();
                        onDestroy();
                        break;
                    case DisconnectedUnexpected:
                    case Failure:
                    case FailureInvalidResource:
                        mToggleConnectButton.setChecked(false);
                        mToolbarLayout.setVisibility(View.VISIBLE);

                        // If a return URL was provided as an input parameter, then return to that application
                        if (mReturnURL != null) {
                            // Provide a callstate of either 0 or 1, depending on whether the call was successful
                            Intent returnApp = getPackageManager().getLaunchIntentForPackage(mReturnURL);
                            returnApp.putExtra("callstate", (mVidyoConnectorState == VidyoConnectorState.Disconnected) ? 1 : 0);
                            startActivity(returnApp);
                        }

                        // If the allow-reconnect flag is set to false and a normal (non-failure) disconnect occurred,
                        // then disable the toggle connect button, in order to prevent reconnection.
                        if (!mAllowReconnect && (mVidyoConnectorState == VidyoConnectorState.Disconnected)) {
                            mToggleConnectButton.setEnabled(false);
                            mToolbarStatus.setText("Call ended");
                        }

                        if (!mHideConfig ) {
                            // Display the form.
                        }

                        // Allow the device to sleep if disconnected.
                        getWindow().clearFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);
                        break;
                }
            }
        });
    }


    @Override
    public void onClick(View v) {
        if (mVidyoConnector != null) {
            switch (v.getId()) {
                case R.id.connect:
                    // Connect or disconnect.
                    this.toggleConnect();
                    break;

                case R.id.camera_switch:
                    // Cycle the camera.
                  //  mVidyoConnector.cycleCamera();
                    break;

                case R.id.camera_privacy:
                    // Toggle the camera privacy.
                    //mCameraPrivacy = mCameraPrivacyButton.isChecked();
                   // mVidyoConnector.setCameraPrivacy(mCameraPrivacy);
                    break;

                case R.id.microphone_privacy:
                    // Toggle the microphone privacy.
                    mMicrophonePrivacy = mMicrophonePrivacyButton.isChecked();
                    mVidyoConnector.setMicrophonePrivacy(mMicrophonePrivacy);
                    break;

                case R.id.toggle_debug:
                    // Toggle debugging.
                    mEnableDebug = !mEnableDebug;
                    if (mEnableDebug) {
                        mVidyoConnector.enableDebug(7776, "warning info@VidyoClient info@VidyoConnector");

                    } else {
                        mVidyoConnector.disableDebug();
                    }
                    break;

                default:
                    mLogger.Log("onClick: Unexpected click event, id=" + v.getId());
                    break;
            }
        } else {
            mLogger.Log("ERROR: not processing click event because Connector is null.");
        }
    }

    public void toggleConnect() {

        if (mToggleConnectButton.isChecked()) {
            // Connect to either a Vidyo.io resource or a VidyoCloud Vidyo room.
            if (!mVidyoCloudJoin) {
                // Connect to a Vidyo.io resource.
                // Abort the Connect call if resource ID is invalid. It cannot contain empty spaces or "@".
                String resourceId = getIntent().getStringExtra("resourceId");
                String token= getIntent().getStringExtra("token");
                displayname= getIntent().getStringExtra("displayName");
                String host="prod.vidyo.io";
                if (resourceId.contains(" ") || resourceId.contains("@")) {
                    this.changeState(VidyoConnectorState.FailureInvalidResource);
                } else {
                    this.changeState(VidyoConnectorState.Connecting);

                    if (!mVidyoConnector.connect(
                            host,
                            token,
                            displayname,
                            resourceId,
                            this)) {
                        // Connect failed.
                        this.changeState(VidyoConnectorState.Failure);
                    }
                }
            } else {
                // Connect to a VidyoCloud Vidyo system, not Vidyo.io.
                this.changeState(VidyoConnectorState.Connecting);

                if (!mVidyoConnector.connectToRoomAsGuest(
                        mPortal,
                        "Guest",
                        mRoomKey,
                        mRoomPin,
                        this)) {
                    // Connect failed.
                    this.changeState(VidyoConnectorState.Failure);
                }
            }
            mLogger.Log("VidyoConnectorConnect status = " + (mVidyoConnectorState == VidyoConnectorState.Connecting));
        } else
            {
            // The user is either connected to a resource or is in the process of connecting to a resource;
            // Call VidyoConnectorDisconnect to either disconnect or abort the connection attempt.
            this.changeState(VidyoConnectorState.Disconnecting);
            mVidyoConnector.disconnect();
        }
    }

    protected void initializeInterpreter(){
        try{
            yawn=new Interpreter(loadmodelfile(this,"yawn.tflite"));
            expression= new Interpreter(loadmodelfile(this,"expression.tflite"));
        }catch (Exception e) {
            e.printStackTrace();
        }
    }

    protected Bitmap convertBitmap(ImageProxy imageProxy){
        Image image = imageProxy.getImage();
        Image.Plane[] planes = image.getPlanes();
        ByteBuffer yBuffer = planes[0].getBuffer();
        ByteBuffer uBuffer = planes[1].getBuffer();
        ByteBuffer vBuffer = planes[2].getBuffer();

        int ySize = yBuffer.remaining();
        int uSize = uBuffer.remaining();
        int vSize = vBuffer.remaining();

        byte[] nv21 = new byte[ySize + uSize + vSize];
        //U and V are swapped
        yBuffer.get(nv21, 0, ySize);
        vBuffer.get(nv21, ySize, vSize);
        uBuffer.get(nv21, ySize + vSize, uSize);

        YuvImage yuvImage = new YuvImage(nv21, ImageFormat.NV21, image.getWidth(), image.getHeight(), null);
        ByteArrayOutputStream out = new ByteArrayOutputStream();
        yuvImage.compressToJpeg(new Rect(0, 0, yuvImage.getWidth(), yuvImage.getHeight()), 75, out);

        byte[] imageBytes = out.toByteArray();
        return BitmapFactory.decodeByteArray(imageBytes, 0, imageBytes.length);
    }

    public void face_detector(Bitmap bitmap,int rotationDegree){
        facedetectImage=bitmap;
        InputImage image = InputImage.fromBitmap(facedetectImage, rotationDegree);
        FaceDetectorOptions highAccuracyOpts =
                new FaceDetectorOptions.Builder()
                        .setPerformanceMode(FaceDetectorOptions.PERFORMANCE_MODE_FAST)
                        .setLandmarkMode(FaceDetectorOptions.LANDMARK_MODE_ALL)
                        .setClassificationMode(FaceDetectorOptions.CLASSIFICATION_MODE_ALL).build();
        FaceDetector detector = FaceDetection.getClient(highAccuracyOpts);
        Task<List<Face>> result =
                detector.process(image)
                        .addOnSuccessListener(
                                new OnSuccessListener<List<Face>>() {
                                    @Override
                                    public void onSuccess(List<Face> faces) {
                                        if(!faces.isEmpty()){
                                            ModelPrediction prediction = new ModelPrediction();
                                            resultofyawn=prediction.predictresult(facedetectImage,yawn,getApplicationContext(),"yawn.txt");
                                            resultofexpression=prediction.predictresult(facedetectImage,expression,getApplicationContext(),"expression.txt");
                                            facedetectlist.add("1");
                                            yawnlist.add(resultofyawn);
                                            expressionlist.add(resultofexpression);
                                        }
                                        else {
                                            facedetectlist.add("0");
                                        }
                                    }
                                })
                        .addOnFailureListener(
                                new OnFailureListener() {
                                    @Override
                                    public void onFailure(@NonNull Exception e) {

                                    }
                                });

    }

    private MappedByteBuffer loadmodelfile(Activity activity,String filename) throws IOException {
        AssetFileDescriptor fileDescriptor=activity.getAssets().openFd(filename);
        FileInputStream inputStream=new FileInputStream(fileDescriptor.getFileDescriptor());
        FileChannel fileChannel=inputStream.getChannel();
        long startoffset = fileDescriptor.getStartOffset();
        long declaredLength=fileDescriptor.getDeclaredLength();
        return fileChannel.map(FileChannel.MapMode.READ_ONLY,startoffset,declaredLength);
    }

    protected void updatedata(){

        Query query= FirebaseDatabase.getInstance().getReference("Meetings")
                .child(roomid).child("Participants").child(displayname).child(String.valueOf(Datacounter));
        query.addValueEventListener(new ValueEventListener() {
            @Override
            public void onDataChange(@NonNull DataSnapshot snapshot) {
                nodepresent=false;
                if(snapshot.exists()){
                    nodepresent=true;
                }
                if(!snapshot.exists()){
                    nodepresent=false;
                }
            }

            @Override
            public void onCancelled(@NonNull DatabaseError error) {

            }
        });

        if(nodepresent){
            String drowsiness="0",face_detect="0",positiveornegative="1";
            int occurrencesnotface = Collections.frequency(facedetectlist, "1");
            int occurrencesyawn = Collections.frequency(yawnlist, "Yawning");
            int occurrencesexp = Collections.frequency(expressionlist, "Disgust");
            if(occurrencesyawn>=1){
                drowsiness="1";
            }
            if(occurrencesnotface>=2){
                face_detect="1";
            }
            if(occurrencesexp>=2){
                positiveornegative="0";
            }
            DatabaseReference reference = FirebaseDatabase.getInstance().getReference("Meetings")
                    .child(roomid).child("Participants")
                    .child(displayname).child(String.valueOf(Datacounter));
            reference.child("drowsiness").setValue(drowsiness);
            reference.child("face_detect").setValue(face_detect);
            reference.child("positiveornegative").setValue(positiveornegative);
            facedetectlist.clear();
            expressionlist.clear();
            yawnlist.clear();
            Datacounter++;
        }
        
    }

    @Override
    public void onVideoFrameClicked() {
        if (mVidyoConnectorState == VidyoConnectorState.Connected) {
            if (mToolbarLayout.getVisibility() == View.VISIBLE) {
                mToolbarLayout.setVisibility(View.INVISIBLE);
            } else {
                mToolbarLayout.setVisibility(View.VISIBLE);
            }
        }
    }

    @Override
    public void onSuccess() {
        mLogger.Log("onSuccess: successfully connected.");
        this.changeState(VidyoConnectorState.Connected);
    }

    // Handle attempted connection failure.
    @Override
    public void onFailure(Connector.ConnectorFailReason reason) {
        mLogger.Log("onFailure: connection attempt failed, reason = " + reason.toString());
        this.changeState(VidyoConnectorState.Failure);
    }

    // Handle an existing session being disconnected.
    @Override
    public void onDisconnected(Connector.ConnectorDisconnectReason reason) {
        if (reason == Connector.ConnectorDisconnectReason.VIDYO_CONNECTORDISCONNECTREASON_Disconnected) {
            mLogger.Log("onDisconnected: successfully disconnected, reason = " + reason.toString());
            this.changeState(VidyoConnectorState.Disconnected);
        } else {
            mLogger.Log("onDisconnected: unexpected disconnection, reason = " + reason.toString());
            this.changeState(VidyoConnectorState.DisconnectedUnexpected);
        }
    }

    // Handle local camera events.
    @Override
    public void onLocalCameraAdded(LocalCamera localCamera) {
        mLogger.Log("onLocalCameraAdded: " + localCamera.getName());

    }

    @Override
    public void onLocalCameraRemoved(LocalCamera localCamera) {
        mLogger.Log("onLocalCameraRemoved: " + localCamera.getName());
    }

    @Override
    public void onLocalCameraSelected(LocalCamera localCamera) {
        mLogger.Log("onLocalCameraSelected: " + (localCamera == null ? "none" : localCamera.getName()));

        // If a camera is selected, then update mLastSelectedCamera.
        if (localCamera != null) {
            mLastSelectedCamera = localCamera;
        }
    }

    @Override
    public void onLocalCameraStateUpdated(LocalCamera localCamera, Device.DeviceState state) {
        mLogger.Log("onLocalCameraStateUpdated: name=" + localCamera.getName() + " state=" + state);
    }

    // Handle a message being logged.
    @Override
    public void onLog(LogRecord logRecord) {
        // No need to log to console here, since that is implicitly done when calling registerLogEventListener.
    }

    // Handle network interface events
    @Override
    public void onNetworkInterfaceAdded(NetworkInterface vidyoNetworkInterface) {
        mLogger.Log("onNetworkInterfaceAdded: name=" + vidyoNetworkInterface.getName() + " address=" + vidyoNetworkInterface.getAddress() + " type=" + vidyoNetworkInterface.getType() + " family=" + vidyoNetworkInterface.getFamily());
    }

    @Override
    public void onNetworkInterfaceRemoved(NetworkInterface vidyoNetworkInterface) {
        mLogger.Log("onNetworkInterfaceRemoved: name=" + vidyoNetworkInterface.getName() + " address=" + vidyoNetworkInterface.getAddress() + " type=" + vidyoNetworkInterface.getType() + " family=" + vidyoNetworkInterface.getFamily());
    }

    @Override
    public void onNetworkInterfaceSelected(NetworkInterface vidyoNetworkInterface, NetworkInterface.NetworkInterfaceTransportType vidyoNetworkInterfaceTransportType) {
        mLogger.Log("onNetworkInterfaceSelected: name=" + vidyoNetworkInterface.getName() + " address=" + vidyoNetworkInterface.getAddress() + " type=" + vidyoNetworkInterface.getType() + " family=" + vidyoNetworkInterface.getFamily());
    }

    @Override
    public void onNetworkInterfaceStateUpdated(NetworkInterface vidyoNetworkInterface, NetworkInterface.NetworkInterfaceState vidyoNetworkInterfaceState) {
        mLogger.Log("onNetworkInterfaceStateUpdated: name=" + vidyoNetworkInterface.getName() + " address=" + vidyoNetworkInterface.getAddress() + " type=" + vidyoNetworkInterface.getType() + " family=" + vidyoNetworkInterface.getFamily() + " state=" + vidyoNetworkInterfaceState);
    }
}
