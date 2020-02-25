""" 
FILES MANAGEMENT 

Need to configuration file to run (FilesManager_Settings.json)



"""
from watchdog.observers import Observer
import time
from watchdog.events import FileSystemEventHandler
import os
import json

""" Read Configuration"""

with open('FilesManager_Settings.json', 'r') as cfg:
    config = json.load(cfg)
for c in config:
    OriginalFolder = c["OriginalFolder"]
    DestinationFolder = c["DestinationFolder"]
    CreateSubDirsWithExtension = c["CreateSubDirsWithExtension"] == "True"
    GroupInstallersZipRar = c["GroupInstallersZipRar"] == "True"
    MoveAllImageFormatsToThisDir = c["MoveAllImageFormatsToThisDir"] 
    ListOfImageFileExtensions = str.split(c["ListOfImageFileExtensions"], ',')



class MyHandler(FileSystemEventHandler):

    def on_modified(self, event):
        for fileName in os.listdir(OriginalFolder):
            try:
                src = os.path.join(OriginalFolder,fileName)
                dest = os.path.join(DestinationFolder, fileName)

                if os.path.isfile(src) and CreateSubDirsWithExtension:
                    name, extension = os.path.splitext(fileName)
                    extension = str.lower(extension)

                    if(MoveAllImageFormatsToThisDir and (extension in ListOfImageFileExtensions)):
                        dest = os.path.join(MoveAllImageFormatsToThisDir, fileName)
                    else:
                        subDir = str.replace(extension,'.','',1)
                        CreateDirectory(DestinationFolder,subDir)
                        dest = os.path.join(DestinationFolder, subDir, fileName)
                
                os.rename(src,dest)

            except PermissionError as ex:
                print('File Permission Error. Details : ' + ex.strerror)
            except FileExistsError as ex:
                print('File already exists in destination folder. Details : ' + ex.strerror)



def CreateDirectory(pathToDir, dirName):
    if not os.path.exists( os.path.join(pathToDir,dirName)):
        os.makedirs(os.path.join(pathToDir,dirName))
    
    return




""" MAIN """
print("Starting monitoring on : " + OriginalFolder)
eventHandler = MyHandler()
observer = Observer()

observer.schedule(eventHandler, OriginalFolder, recursive=True)

observer.start()

try:
    while True:
        time.sleep(10)
except KeyboardInterrupt:
    observer.stop()
observer.join()


