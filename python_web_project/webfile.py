# -*- coding: utf-8 -*-
"""
Created on Sun Aug 16 16:45:33 2020

@author: Manish
"""
from collections import OrderedDict, Counter
import os
import pandas as pd
import pyrebase
import matplotlib.pyplot as plt
from flask import Flask, request, render_template, send_from_directory
import numpy as np
import random

app = Flask(__name__)

allusers=[]
counter=0



APP_ROOT=os.path.dirname(os.path.abspath(__file__))
@app.route("/upload", methods=["POST"])
def upload():
    target = os.path.join(APP_ROOT, 'images/')
    print(target)
    if not os.path.isdir(target):
            os.mkdir(target)
    else:
        print("Couldn't create upload directory: {}".format(target))
    print(request.files.getlist("file"))
    for upload in request.files.getlist("file"):
        print(upload)
        print("{} is the file name".format(upload.filename))
        filename = upload.filename
        destination = "/".join([target, filename])
        print ("Accept incoming file:", filename)
        print ("Save it to:", destination)
        upload.save(destination)

@app.route("/upload2", methods=["POST"])
def upload2():
    target = os.path.join(APP_ROOT, './static/images')
    print(target)
    if not os.path.isdir(target):
            os.mkdir(target)
    else:
        print("Couldn't create upload directory: {}".format(target))
    print(request.files.getlist("file"))
    for upload in request.files.getlist("file"):
        print(upload)
        print("{} is the file name".format(upload.filename))
        filename = upload.filename
        destination = "/".join([target, filename])
        print ("Accept incoming file:", filename)
        print ("Save it to:", destination)
        upload.save(destination)

    # return send_from_directory("images", filename, as_attachment=True)
    return render_template("complete.html", image_name=filename)

@app.route('/upload/<filename>')
def send_image(filename):
    return send_from_directory("images", filename)
@app.route('/upload2/<filename>')
def send_image2(filename):
    return send_from_directory("./static/images",filename)


@app.route('/dashboard/', methods=['GET'])
def get_dashboard():

    
    image_names = os.listdir('./images')
    print(image_names)
    firebaseConfig = {
    "apiKey": "AIzaSyC6WsmIlvHvEGPB3WeoociI1GAJeRLssFg",
    "authDomain": "smart-video-conference-57792.firebaseapp.com",
    "databaseURL": "https://smart-video-conference-57792.firebaseio.com",
    "projectId": "smart-video-conference-57792",
    "storageBucket": "smart-video-conference-57792.appspot.com",
    "messagingSenderId": "6874514617",
    "appId": "1:6874514617:web:53000bee86ba12ed48c768"
  }
    
    firebase1=pyrebase.initialize_app(firebaseConfig);   
    database = firebase1.database();
    
    
    meetingid=1001
    meetingid = request.args.get("meetingid", None)

    
    print("hello")
    print(meetingid)

    
    randomnumber=str(random.randint(1, 10000000000))
    
    # Get a database reference to our posts
    db_events = database.child('Meetings/'+str(meetingid)+'/Participants').get().val()
    
    # Read the data at the posts reference (this is a blocking operation)
    dict(db_events)
    df = pd.DataFrame(db_events)
    df=df.drop(0,axis=0)
    users=[]
    
    
    
    print(image_names)
    
    image_names = os.listdir('./images')
    for image in image_names:
         os.remove(os.path.join('./images',image))
         
    image_names2=os.listdir("./static/images")
    for image in image_names2:
         os.remove(os.path.join('./static/images',image))
    
    for i, user in df.iteritems(): 
        #print(user) 
        newdf=pd.DataFrame(user)
        
        
        drowsiness=0
        face_detect=0
        positiveornegative=0
        drowsiness_list=[]
        face_detect_list=[]
        positiveornegative_list=[]
        time_periods=[]
        for k,m in newdf.iterrows(): 
            #m.drop('Index', axis=1, inplace=True)
            #dats=dict(m)
            print(m)
            drowsiness+=int(m[0]['drowsiness'])
            face_detect+=int(m[0]['face_detect'])
            positiveornegative+=int(m[0]['positiveornegative'])
            
            drowsiness_list.append((m[0]['drowsiness']))
            face_detect_list.append(int(m[0]['face_detect']))
            positiveornegative_list.append(int(m[0]['positiveornegative']))
            time_periods.append(k)
            
        
        username=str(m.index[0])

        
        print(username)
        users.append(username)
        labels = 'drowsiness_detected', 'No Drowsiness'
        sizes = [drowsiness,k-drowsiness]
        colors = ['gold', 'yellowgreen']
        explode = (0.1, 0)  # explode 1st slice
        
        labels2 = 'face_detect', 'Face not detected'
        sizes2 = [face_detect,k-face_detect]
        
        labels3 = 'Positive', 'Negative'
        sizes3 = [positiveornegative,k-positiveornegative]
        
        # Plot
        plt.pie(sizes, explode=explode, labels=labels, colors=colors,
        autopct='%1.1f%%', shadow=True, startangle=140)
        plt.axis('equal')
        plt.suptitle(username, fontsize=25)
        plt.savefig('./images/1'+username+str(labels[0])+randomnumber+'.jpg')
        plt.show()
        
        
        plt.pie(sizes2, explode=explode, labels=labels2, colors=colors,
        autopct='%1.1f%%', shadow=True, startangle=140)
        plt.axis('equal')
        plt.suptitle(username, fontsize=25)
        plt.savefig('./images/1'+username+str(labels2[0])+randomnumber+'.jpg')
        plt.show()
        
        
        plt.pie(sizes3, explode=explode, labels=labels3, colors=colors,
        autopct='%1.1f%%', shadow=True, startangle=140)
        plt.axis('equal')
        plt.suptitle(username, fontsize=25)
        plt.savefig('./images/1'+username+str(labels3[0])+randomnumber+'.jpg')
        plt.show()
        

        plt.plot(time_periods,face_detect_list)
        plt.title('Face Detection in time intervals')
        plt.xlabel('time period')
        plt.ylabel('face deteted or not')
        plt.suptitle(username, fontsize=25)
        plt.savefig('./images/1'+username+'face_detecet_line_chart'+randomnumber+'.jpg')
        plt.show()
        
        plt.plot(time_periods,drowsiness_list)
        plt.title('Drowsiness Detection')
        plt.xlabel('time period')
        plt.ylabel('Drowsiness')
        plt.suptitle(username, fontsize=25)
        plt.savefig('./images/1'+username+'drowsiness_line_chart'+randomnumber+'.jpg')
        plt.show()
        
        plt.plot(time_periods,positiveornegative_list)
        plt.title('Face Reactions in time intervals')
        plt.xlabel('time period')
        plt.ylabel('Face reaction')
        plt.suptitle(username, fontsize=25)
        plt.savefig('./images/1'+username+'positiveornegative_line_chart'+randomnumber+'.jpg')
        plt.show()
                
        
        
    drowsiness_list=[]
    face_detect_list=[]
    positiveornegative_list=[]   
    time_periods=[]
    
    for i, time_period in df.iterrows(): 
    #print(user) 
        newdf2=pd.DataFrame(time_period)
        shape=newdf2.shape[0]
        
        drowsiness=0
        face_detect=0
        positiveornegative=0
               
        
        for k,m in newdf2.iteritems():
            for s,t in m.iteritems():
                drowsiness+=int(t['drowsiness'])
                
                face_detect+=int(t['face_detect'])
                positiveornegative+=int(t['positiveornegative'])
           
        drowsiness_list.append((drowsiness/shape)*100)
        face_detect_list.append((face_detect/shape)*100)
        positiveornegative_list.append((positiveornegative/shape)*100)
        time_periods.append(i)

    print(drowsiness_list)
    print(face_detect_list)  
    print(positiveornegative_list)
    print(time_periods)
     
    x = np.arange(len(time_periods))  # the label locations
    width = 0.25  # the width of the bars
    
    fig, ax = plt.subplots()
    rects1 = ax.bar(x, face_detect_list,width, label='Face Detection')
    rects2 = ax.bar(x+width, drowsiness_list, width, label='Drowsiness')
    rects3=ax.bar(x+2*width,positiveornegative_list,width,label='Positive face reaction')
    
    
    # Add some text for labels, title and custom x-axis tick labels, etc.
    ax.set_ylabel('Percentage')
    ax.set_xticks(x)
    ax.set_xticklabels(time_periods)
    ax.set_xlabel('Time Periods')
    ax.legend()
    
    
    def autolabel(rects):
        for rect in rects:
            h = rect.get_height()
            ax.text(rect.get_x()+rect.get_width()/2., 1.05*h, '%d'%int(h),
                ha='center', va='bottom')
    
    autolabel(rects1)
    autolabel(rects2)
    autolabel(rects3)
    
    fig.tight_layout()
    plt.savefig('static/images/'+randomnumber+'bar_chart2'+'.jpg')
    plt.show()
    
    plt.plot(time_periods,face_detect_list)
    plt.title('Face Detection Percentage')
    plt.xlabel('time period')
    plt.ylabel('% of people whose face detected')
    plt.savefig('static/images/'+randomnumber+'face_detect_line2'+'.jpg')
    plt.show()
    
    
    plt.plot(time_periods,positiveornegative_list)
    plt.title('Positive reactions Percentage')
    plt.xlabel('time period')
    plt.ylabel('% of people whose have shown positive reaction')
    plt.savefig('static/images/'+randomnumber+'positive_or_negative_line2'+'.jpg')
    plt.show()
    
    
    plt.plot(time_periods,drowsiness_list)
    plt.title('Drowsiness Detection Percentage')
    plt.xlabel('time period')
    plt.ylabel('% of people whose drowsiness detected')
    plt.savefig('static/images/'+randomnumber+'drowsiness_line2'+'.jpg')
    plt.show()

        
    print(users)
    
    image_names = os.listdir('./images')
    print(image_names)
    
    image_names2=os.listdir("./static/images/")
    print(image_names2)
    
    bar=image_names2[0]
    drowsiness=image_names2[1]
    face_detect=image_names2[2]
    positiveornegative=image_names2[3]
    


        
    return render_template("dashboard.html",users=users, image_names=image_names,bar=bar,drowsiness=drowsiness,face_detect=face_detect,positiveornegative=positiveornegative)

    
    





@app.route('/')
def index():
    
    print(name)
    return "<h1>Welcome to our server !!</h1>"

if __name__ == '__main__':
    # Threaded option to enable multiple instances for multiple user access support
    app.run()

"""
meetingid=1001
database.child('Meetings/'+str(meetingid)+'/Participants').child('Vasu').child('1').set({'face_detect':'1','positiveornegative':'1','drowsiness':'0'})
database.child('Meetings/'+str(meetingid)+'/Participants').child('Vasu').child('2').set({'face_detect':'1','positiveornegative':'1','drowsiness':'0'})
database.child('Meetings/'+str(meetingid)+'/Participants').child('Vasu').child('3').set({'face_detect':'1','positiveornegative':'1','drowsiness':'0'})
database.child('Meetings/'+str(meetingid)+'/Participants').child('Vasu').child('4').set({'face_detect':'1','positiveornegative':'0','drowsiness':'1'})
database.child('Meetings/'+str(meetingid)+'/Participants').child('Vasu').child('5').set({'face_detect':'0','positiveornegative':'0','drowsiness':'0'})
database.child('Meetings/'+str(meetingid)+'/Participants').child('Vasu').child('6').set({'face_detect':'0','positiveornegative':'0','drowsiness':'0'})


database.child('Meetings/'+str(meetingid)+'/Participants').child('Vasu').child('7').set({'face_detect':'0','positiveornegative':'0','drowsiness':'0'})
database.child('Meetings/'+str(meetingid)+'/Participants').child('Vasu').child('8').set({'face_detect':'1','positiveornegative':'1','drowsiness':'0'})
database.child('Meetings/'+str(meetingid)+'/Participants').child('Vasu').child('9').set({'face_detect':'1','positiveornegative':'0','drowsiness':'1'})

database.child('Meetings/'+str(meetingid)+'/Participants').child('Vasu').child('10').set({'face_detect':'1','positiveornegative':'1','drowsiness':'1'})
database.child('Meetings/'+str(meetingid)+'/Participants').child('Vasu').child('11').set({'face_detect':'1','positiveornegative':'0','drowsiness':'1'})
database.child('Meetings/'+str(meetingid)+'/Participants').child('Vasu').child('12').set({'face_detect':'1','positiveornegative':'0','drowsiness':'1'})



database.child('Meetings/'+str(meetingid)+'/Participants').child('Vinayak').child('1').set({'face_detect':'0','positiveornegative':'0','drowsiness':'0'})
database.child('Meetings/'+str(meetingid)+'/Participants').child('Vinayak').child('2').set({'face_detect':'0','positiveornegative':'0','drowsiness':'0'})
database.child('Meetings/'+str(meetingid)+'/Participants').child('Vinayak').child('3').set({'face_detect':'1','positiveornegative':'0','drowsiness':'1'})
database.child('Meetings/'+str(meetingid)+'/Participants').child('Vinayak').child('4').set({'face_detect':'1','positiveornegative':'0','drowsiness':'1'})
database.child('Meetings/'+str(meetingid)+'/Participants').child('Vinayak').child('5').set({'face_detect':'1','positiveornegative':'1','drowsiness':'0'})
database.child('Meetings/'+str(meetingid)+'/Participants').child('Vinayak').child('6').set({'face_detect':'1','positiveornegative':'1','drowsiness':'0'})

database.child('Meetings/'+str(meetingid)+'/Participants').child('Vinayak').child('7').set({'face_detect':'1','positiveornegative':'1','drowsiness':'1'})
database.child('Meetings/'+str(meetingid)+'/Participants').child('Vinayak').child('8').set({'face_detect':'1','positiveornegative':'0','drowsiness':'1'})
database.child('Meetings/'+str(meetingid)+'/Participants').child('Vinayak').child('9').set({'face_detect':'1','positiveornegative':'0','drowsiness':'1'})

"""