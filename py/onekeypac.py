from optparse import OptionParser
import subprocess
import os
import time

UNITYPATH = "/Applications/Unity/Unity.app/Contents/MacOS/Unity"
PROJECTPATH = "/Users/mac/unityonekeypackage/unityproject"
def buildProjectResource():
    print("先导出mac包")
    build = UNITYPATH + ' -quit -batchmode -projectPath ' + PROJECTPATH + ' -executeMethod BuildProject.BuildIOS '

    print("再导出xcode")
    buildCmd = build + "ios"
    time5 = time.time()
    process = subprocess.Popen(buildCmd, shell = True)
    process.wait()
    time6 = time.time()
    print("导出Xcode工程所用时间： " + str(time6 - time5))


def main():
    buildProjectResource()
  


if __name__ == '__main__':
    main()