package com.team.uical.models;

import android.content.Context;
import android.graphics.Bitmap;
import android.os.Handler;
import android.os.Looper;
import android.widget.Toast;

import org.tensorflow.lite.DataType;
import org.tensorflow.lite.Interpreter;
import org.tensorflow.lite.support.common.FileUtil;
import org.tensorflow.lite.support.common.TensorOperator;
import org.tensorflow.lite.support.common.TensorProcessor;
import org.tensorflow.lite.support.common.ops.NormalizeOp;
import org.tensorflow.lite.support.image.ImageProcessor;
import org.tensorflow.lite.support.image.TensorImage;
import org.tensorflow.lite.support.image.ops.ResizeOp;
import org.tensorflow.lite.support.image.ops.ResizeWithCropOrPadOp;
import org.tensorflow.lite.support.label.TensorLabel;
import org.tensorflow.lite.support.tensorbuffer.TensorBuffer;

import java.nio.MappedByteBuffer;
import java.util.Collections;
import java.util.List;
import java.util.Map;
import java.util.PrimitiveIterator;

public class ModelPrediction {

    private TensorImage inputImageBuffer;
    private  int imageSizeX;
    private  int imageSizeY;
    private  TensorBuffer outputProbabilityBuffer;
    private  TensorProcessor probabilityProcessor;
    private static final float IMAGE_MEAN = 0.0f;
    private static final float IMAGE_STD = 1.0f;
    private static final float PROBABILITY_MEAN = 0.0f;
    private static final float PROBABILITY_STD = 255.0f;

    public ModelPrediction() {
    }

    private List<String> labels;

   public String predictresult(Bitmap bitmap , Interpreter interpreter, Context context,String filename){
           String result = null;

               int imageTensorIndex = 0;
               int[] imageShape = interpreter.getInputTensor(imageTensorIndex).shape(); // {1, height, width, 3}
               imageSizeY = imageShape[1];
               imageSizeX = imageShape[2];
               DataType imageDataType = interpreter.getInputTensor(imageTensorIndex).dataType();

               int probabilityTensorIndex = 0;
               int[] probabilityShape =
                       interpreter.getOutputTensor(probabilityTensorIndex).shape(); // {1, NUM_CLASSES}
               DataType probabilityDataType = interpreter.getOutputTensor(probabilityTensorIndex).dataType();

               inputImageBuffer = new TensorImage(imageDataType);
               outputProbabilityBuffer = TensorBuffer.createFixedSize(probabilityShape, probabilityDataType);
               probabilityProcessor = new TensorProcessor.Builder().add(getPostprocessNormalizeOp()).build();

               inputImageBuffer = loadImage(bitmap,inputImageBuffer);

               interpreter.run(inputImageBuffer.getBuffer(),outputProbabilityBuffer.getBuffer().rewind());

               try{
                   labels = FileUtil.loadLabels(context,filename);
               }catch (Exception e){
                   e.printStackTrace();
               }
               Map<String, Float> labeledProbability =
                       new TensorLabel(labels, probabilityProcessor.process(outputProbabilityBuffer))
                               .getMapWithFloatValue();
               float maxValueInMap =(Collections.max(labeledProbability.values()));

               for (Map.Entry<String, Float> entry : labeledProbability.entrySet()) {
                   if (entry.getValue()==maxValueInMap) {
                   result = entry.getKey();
                   }
               }
       return result;
    }
    private TensorImage loadImage(final Bitmap bitmap,final TensorImage inputBuffer ) {
        ImageProcessor imageProcessor;
               inputBuffer.load(bitmap);
               // Creates processor for the TensorImage.
               int cropSize = Math.min(bitmap.getWidth(), bitmap.getHeight());
               // TODO(b/143564309): Fuse ops inside ImageProcessor.
               imageProcessor =
                       new ImageProcessor.Builder()
                               .add(new ResizeWithCropOrPadOp(cropSize, cropSize))
                               .add(new ResizeOp(imageSizeX, imageSizeY, ResizeOp.ResizeMethod.NEAREST_NEIGHBOR))
                               .add(getPreprocessNormalizeOp())
                               .build();
       return imageProcessor.process(inputBuffer);
    }
    private TensorOperator getPreprocessNormalizeOp() {
        return new NormalizeOp(IMAGE_MEAN, IMAGE_STD);
    }
    private TensorOperator getPostprocessNormalizeOp(){
        return new NormalizeOp(PROBABILITY_MEAN, PROBABILITY_STD);
    }
}
