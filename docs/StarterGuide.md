# Starter Guide

## 1. Purpose

This activity will describe how to use the 2020FRCSim Unity simulator with Java code.

## 2. Materials

- Github Desktop

## 3. Instructions

### 3a. Downloading Latest Unity Simulator

1. Download the latest 2020FRCSim from [this link](https://github.com/Maciej4/2020FRCSim/releases).
2. Unzip the downloaded .zip archive.
3. Open the unziped folder and run `2020FRCSim.exe`.
4. After a moment, the simulator should open, and you should see the following:

![image](https://raw.githubusercontent.com/Maciej4/2020FRCSim/master/docs/images/unity_sim_main_menu.png)

5. Read the description seen to the right.
6. For the purposes of this tutorial, navigate to `TRAINING`, then `level1.exe`.

### 3b. Cloning NEOBot

1. The repository is located at [this link](https://github.com/Maciej4/NEOBot), if needed follow [these directions on cloning a repository](https://github.com/iron-claw-972/Curriculum2020/blob/master/GithubDesktop.md#3d-cloning-a-repository).
2. Open the repository using FRC Visual Studio Code.
 - Note: Use 2020 VS Code. Also, if prompted to import the project into 2020 select no.

### 3c. Running Java Tests to Link to Sim

1. At this point, you should have `level1.exe` open and running in the Unity Sim
2. In FRC Visual Studio Code and press `ctrl-shift-p`. Then type `Build` and select `WPILIB: Build Robot Code`. Finally, press enter.
3. At this point, the `TERMINAL` output should look approximately like this:

```
frc.robot.RobotTest > simulateRobot STANDARD_OUT
    Sucessful Server Initialization
    Awaiting communication from Unity (ctrl c to kill)...
    Communication recieved:
    {"heartbeat":1002.1461791992188,"robotMode":0,"motorPositions":[0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0],"nmfColorR":0.0,"nmfColorG":0.0,"nmfColorB":0.0,"joyButtonArray":[false,false,false,false],"joyAxisArray":[0.0,0.0,0.0,0.0],"joyPOV":0,"navXHeading":0.0}

    Running robot init!
    Awaiting Unity to start robot period...
```

**Important Note: In order for the tests to run, there has to be a change to the code since the tests were last run. If you do not want to change your code, commenting and uncommenting a print statement is one way to fulfil this requirement. Otherwise, the tests do not run and the code doesn't link with the simulator.**

4. At this point, navigate back to the Unity Sim. The `Communications` indicator should now be green.
5. Press `SPACE` or left click on the red `Enable` to enable the robot.
  - Note: The central area on the simplified Driver Station, looks like the following:

#### In Game Driver Station

![image](https://raw.githubusercontent.com/Maciej4/2020FRCSim/master/docs/images/unity_sim_mini_driver_station.png)

- The left `Enable` area displays whether the robot is enabled. When clicked or when `SPACE` is pressed, the robot will be toggled.
- The central `TeleOperated` area displays the game mode: `Autonomous`, `TeleOperated`, or `Test`. On the training levels, this is fixed and unchangeable, however in some levels it may be a dropdown.
- The top right `Communications` indicator shows whether the Unity Sim is communicating with the Java code.
- The middle right `Joysticks` indicator shows whether a Joystick is plugged into the computer.
- The bottom right `Elapsed Time` indicator shows the amount of time that has passed since the robot was enabled.

## 4. Further reading

[2020Curriculum](https://github.com/iron-claw-972/Curriculum2020)
