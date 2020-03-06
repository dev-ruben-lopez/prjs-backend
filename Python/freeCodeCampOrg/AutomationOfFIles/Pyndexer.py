"""
This script will create an index file with all the files in the system, including all drives connected at the moment.
WINDOWS 10 for now only.

"""

import os
import re
import sys
from threading import  Thread
from datetime import datetime
import subprocess
try:
   import cPickle as pickle
except:
   import pickle

dict1 = {}

"""MOve later all these to a Config file"""
drivesExcluded = ['C:']
indexFileFullName = "pyndexFilesResult.p"



"""Get all drives in the system"""
def get_drives(exclDrv):
    response = os.popen("wmic logicaldisk get caption")
    list1 = []
    t1= datetime.now()
    for line in response.readlines():
                line = line.strip("\n")
                line = line.strip("\r")
                line = line.strip(" ")
                if (line == "Caption" or line == ""):
                            continue
                if(line not in exclDrv):
                    list1.append(line)
    return list1

"""
FIles dictionary key/value =  {file_name, path}
"""
def search1(drive):
    for root, dir, files in os.walk(drive, topdown = True):
        for file in files:
            file= file.lower()
        if file in dict1:
            file = file + '_1' 
            dict1[file]= root
        else:
            dict1[file]= root

"""
This function opens the thread process for each drive, and each thread process calls the search1 function.
"""
def create():
    t1= datetime.now()
    list2 = []   # empty list is created           
    list1 = get_drives(drivesExcluded)
    print('Creating index.  Please stand by...')
    print(list1)
    for each in list1:
        process1 = Thread(target=search1, args=(each,))
        process1.start()
        list2.append(process1)
    for t in list2:
        t.join() # Terminate the threads
    
    pickle.dump(dict1, open(indexFileFullName, 'wb'))
    t2= datetime.now()
    total =t2-t1
    print ('Total time to create' , total)


"""
MAIN 

Read a configuration file to add some variables
"""
t1= datetime.now()
if len(sys.argv) < 2 or len(sys.argv) > 2:
    print('Please use proper format')
    print('Use <finder -c >  to create database file')
    print('Use <finder file-name> to search file')
 
elif sys.argv[1] == '-c':
    create()
            
else:
    try:
        pickle_file  = open(indexFileFullName, 'rb')
        file_dict = pickle.load(pickle_file) 
        pickle_file.close()
    except IOError:
        print('Index file not found. Creating file...')
        create()
    except Exception as e : 
        print(e)
        sys.exit()
    
    file_to_be_searched = sys.argv[1].lower()
    list1= []
    print('Path \t\t: File-name')

for key in file_dict:
    if re.search(file_to_be_searched, key):
        str1 =  file_dict[key] + ' : ' + key
        list1.append(str1)
    list1.sort()
    for each in list1:
        print(each)
        print('-----------------------')
    t2= datetime.now()
    total = t2-t1
    print('Total files are' , len(list1))
    print('Time taken to search ' , total)