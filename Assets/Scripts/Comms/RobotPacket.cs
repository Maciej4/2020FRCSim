using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPacket
{
    //public double leftPower;
    //public double rightPower;

    public long heartbeat = 0;
    public double[] motorPowers = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
    /**public double leftDriveMotor1Power = 0.0;
    public double leftDriveMotor2Power = 0.0;
    public double rightDriveMotor1Power = 0.0;
    public double rightDriveMotor2Power = 0.0;
    public double leftDriveEncoderInterfacePower = 0.0;
    public double rightDriveEncoderInterfacePower = 0.0;
    public double telescopeMotorPower = 0.0;
    public double coilMotor1Power = 0.0;
    public double coilMotor2Power = 0.0;
    public double intakeTalonPower = 0.0;
    public double nmfNeoPower = 0.0;
    public double omniNeoPower = 0.0;
    public double shooterPower1 = 0.0;
    public double shooterPower2 = 0.0;*/
    public int intakeFlipSolenoidState = 0;
    public List<Hardware> hardware = new List<Hardware>();
}
