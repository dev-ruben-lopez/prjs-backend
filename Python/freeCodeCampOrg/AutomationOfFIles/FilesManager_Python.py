""" 
Ruben D. Lopez
FILES MANAGEMENT PYTHON SCRIPT
Version 1.0
Need to configuration file to run (FilesManager_Settings.json)

"""

from pathlib import Path
from watchdog.observers import Observer
import time
from watchdog.events import FileSystemEventHandler
import os
import json
import logging 
import sys



""" Create a dir if does not exists """
def CreateDirectory(pathToDir, dirName):
    if not os.path.exists( os.path.join(pathToDir,dirName)):
        os.makedirs(os.path.join(pathToDir,dirName))
    
    return


""" Fixing back-slashes since D.O.S 1983
    With this fix, we should be able to run paths on MAC and Linux
"""
def ConvertUniversalPathSlashes(path):
    return str.replace(path,'\\', '/')







version = "FileMan Version 1.0, Feb 2020" 
logFilePath = os.path.join(sys.path[0],"FileManLogFile.log")

""" Setting Up Logging Funcionality """
logging.basicConfig(level=logging.DEBUG)
logger = logging.getLogger(__name__)
f_handler = logging.FileHandler(logFilePath)
f_format = logging.Formatter('%(asctime)s - %(levelname)s - %(message)s')
f_handler.setFormatter(f_format)
logger.addHandler(f_handler)
logger.level = logging.DEBUG

try:
    """ Read Configuration"""

    with open( os.path.join(sys.path[0],'FilesManager_Settings.json'), 'r') as cfg:
        config = json.load(cfg)
    for c in config:
        originalFolder = ConvertUniversalPathSlashes(c["OriginalFolder"])
        destinationFolder = ConvertUniversalPathSlashes(c["DestinationFolder"])
        if(not originalFolder or not destinationFolder):
            logger.error("Please setup the config file (FilesManager_Settings.json) and fill the path of the directories (origin/destination)")
            sys.exit()
        createSubDirsWithExtension = c["CreateSubDirsWithExtension"] == "True"
        groupInstallersZipRar = c["GroupInstallersZipRar"] == "True"
        moveAllImageFormatsToThisDir = ConvertUniversalPathSlashes(c["MoveAllImageFormatsToThisDir"] )
        listOfImageFileExtensions = str.split(c["ListOfImageFileExtensions"], ',')
        logFilePath = ConvertUniversalPathSlashes(c["LogFilePath"])
except SystemError as ex:
    logger.error("Error during initialization. Check configuration settings on FilesManager_Settings.json file")
    sys.exit()

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



""" MAIN """
logger.info("*******************************************************************************************")
logger.info(version)
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
