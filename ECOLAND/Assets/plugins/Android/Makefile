#
#   Pedometer
#   Copyright (c) 2017 Yusuf Olokoba
#

SDKLIB := /Users/Son/android.jar # Replace with your path to the Android base class library
UNITYLIB := /Users/Son/classes.jar # Replace with your path to Unity's classes.jar
SRC := PedometerActivity

build:
	@echo "Building..."
	javac -source 1.6 -target 1.6 $(SRC).java -bootclasspath $(SDKLIB) -classpath $(UNITYLIB) -d .
	jar cf $(SRC).jar com
	@echo "Completed"
	@echo "------------------------------------------------------ "