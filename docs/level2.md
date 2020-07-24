# Level 2 Guide

## Level Description

In this level you have to drive to and knock over the tower of yellow cubes.

## Solution Code

This is one example of a solution to the level. Copy pasting it into `Robot.java` will allow you to drive the robot with `W`, `A`, `S`, and `D`.

``` java
package frc.robot;

import com.ctre.phoenix.motorcontrol.ControlMode;
import com.ctre.phoenix.motorcontrol.can.TalonFX;

import edu.wpi.first.wpilibj.Joystick;
import edu.wpi.first.wpilibj.TimedRobot;
import frc.robot.util.*;

public class Robot extends TimedRobot {
  public Joystick joy = HardwareFactory.newJoystick(0);

  public TalonFX lm1 = HardwareFactory.newTalonFX(1);
  public TalonFX lm2 = HardwareFactory.newTalonFX(2);
  public TalonFX rm1 = HardwareFactory.newTalonFX(3);
  public TalonFX rm2 = HardwareFactory.newTalonFX(4);

  @Override
  public void robotInit() {

  }

  @Override
  public void robotPeriodic() {

  }

  @Override
  public void autonomousInit() {

  }

  @Override
  public void autonomousPeriodic() {

  }

  @Override
  public void teleopInit() {
    // System.out.println("");
  }

  @Override
  public void teleopPeriodic() {
    double leftPower = -joy.getRawAxis(0) + joy.getRawAxis(1);
    double rightPower = -joy.getRawAxis(0) - joy.getRawAxis(1);

    lm1.set(ControlMode.PercentOutput, leftPower);
    lm2.set(ControlMode.PercentOutput, leftPower);
    rm1.set(ControlMode.PercentOutput, rightPower);
    rm2.set(ControlMode.PercentOutput, rightPower);
  }
}

```
