""" 
FILES MANAGEMENT 
Need to configuration file to run (FilesManager_Settings.json)

"""

from watchdog.observers import Observer
import time
from watchdog.events import FileSystemEventHandler
import os
import json
import logging


""" Setting Up Logging Funcionality """
logging.basicConfig(level=logging.DEBUG)
logger = logging.getLogger(__name__)
f_handler = logging.FileHandler(logFilePath)
f_format = logging.Formatter('%(asctime)s - %(name)s - %(levelname)s - %(message)s')
f_handler.setFormatter(f_format)
logger.addHandler(f_handler)
logger.level = logging.INFO


""" Read Configuration"""

with open('FilesManager_Settings.json', 'r') as cfg:
    config = json.load(cfg)
for c in config:
    originalFolder = c["OriginalFolder"]
    destinationFolder = c["DestinationFolder"]
    if(not originalFolder or not destinationFolder):
        logger.error("Please setup the Settings File and fill the path of the directories (origin/destination)")
        break
    createSubDirsWithExtension = c["CreateSubDirsWithExtension"] == "True"
    groupInstallersZipRar = c["GroupInstallersZipRar"] == "True"
    moveAllImageFormatsToThisDir = c["MoveAllImageFormatsToThisDir"] 
    listOfImageFileExtensions = str.split(c["ListOfImageFileExtensions"], ',')
    logFilePath = c["LogFilePath"]


""" MAIN """
logger.info("Starting monitoring on : " + originalFolder)
logger.info("Destination folder : " + destinationFolder)

eventHandler = fileSystemEventHandler()
observer = Observer()

observer.schedule(eventHandler, originalFolder, recursive=True)

observer.start()

try:
    while True:
        time.sleep(10)
except KeyboardInterrupt:
    observer.stop()
observer.join()




class fileSystemEventHandler(FileSystemEventHandler):

    def on_modified(self, event):
        for fileName in os.listdir(originalFolder):
            try:
                src = os.path.join(originalFolder,fileName)
                dest = os.path.join(destinationFolder, fileName)

                if os.path.isfile(src) and createSubDirsWithExtension:
                    name, extension = os.path.splitext(fileName)
                    extension = str.lower(extension)

                    if(moveAllImageFormatsToThisDir and (extension in listOfImageFileExtensions)):
                        dest = os.path.join(moveAllImageFormatsToThisDir, fileName)
                    else:
                        subDir = str.replace(extension,'.','',1)
                        CreateDirectory(destinationFolder,subDir)
                        dest = os.path.join(destinationFolder, subDir, fileName)
                
                logger.info('Moving file '+ src +' to ' + dest)
                os.rename(src,dest)

            except PermissionError as ex:
                logger.error('File Permission Error. Details : ' + ex.strerror)
            except FileExistsError as ex:
                logger.error('File already exists in destination folder. Details : ' + ex.strerror)



def CreateDirectory(pathToDir, dirName):
    if not os.path.exists( os.path.join(pathToDir,dirName)):
        os.makedirs(os.path.join(pathToDir,dirName))
    
    return
