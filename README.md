# 2020FRCSim

![image](https://raw.githubusercontent.com/Maciej4/2020FRCSim/master/docs/images/frc2020_level_robot.png)

The starter guide is found at [this link](https://github.com/Maciej4/2020FRCSim/blob/master/docs/StarterGuide.md).

## Features

### Multiple Training Levels

The simulator has several training levels in addition to the 2020 FRC game level.

### Score Tracking

Three goals for each team side, with respective point values for each.

### Drivable Robot

This simple game contains a robot that can be driven with a keyboard or controller.

### Java Integration

Using ZMQ and a specially designed test on the Java code side, this game can simulate the basic functions of a robot running on Java code.

In order to use this feature, clone [this](https://github.com/Maciej4/NEOBot) repository, which contains robot code in Java. Next, make some change to the code, like adding a print statement. Then press `ctrl-shift-p` and wait for the test to start running. The test should say: "Awaiting communication from Unity (ctrl c to kill)..." Then play the Unity game. For the next 20 seconds, the robot will be controlled through Java.

This works by:

1. Reading the joystick in Unity.
2. Serializing some data including the joystick values using Json.
3. Sending this data using ZMQ to the Java code also running on the computer.
4. The Java code then deserializes the data from Json and uses the joystick data to calculate the tank drive values.
5. Next the left and right power for the virtual motors is serialized and sent through ZMQ back to the Unity side.
6. Using basic kinematics, the robot's approximate motion is calculated and visualized given the motor powers.

## Prerequisites

- Github Desktop
- FRC Visual Studio Code
