using System.Collections.Generic;

public class UnityPacket
{
    //public double driveX = 0.0;
    //public double driveY = 0.0;
    //public double heading = 0.0;

    public double heartbeat = 0.0;
    public int robotMode = 0;
    public double[] motorPositions = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
    /**public double leftDriveMotor1Position = 0.0;
    public double leftDriveMotor2Position = 0.0;
    public double rightDriveMotor1Position = 0.0;
    public double rightDriveMotor2Position = 0.0;
    public double leftDriveEncoderInterfacePosition = 0.0;
    public double rightDriveEncoderInterfacePosition = 0.0;
    public double telescopeMotorPosition = 0.0;
    public double coilMotor1Position = 0.0;
    public double coilMotor2Position = 0.0;
    public double intakeTalonPosition = 0.0;
    public double nmfNeoPosition = 0.0;
    public double shooterPosition1 = 0.0;
    public double shooterPosition2 = 0.0;*/
    public double nmfColorR = 0.0;
    public double nmfColorG = 0.0;
    public double nmfColorB = 0.0;
    //public double nmfEncoderInterfacePosition = 0.0;
    //public double omniNeoPosition = 0.0;
    public bool[] joyButtonArray = new bool[] { false, false, false, false };
    public double[] joyAxisArray = new double[] { 0.0, 0.0, 0.0, 0.0 };
    public int joyPOV = 0;
    public double navXHeading = 0.0;
    public List<Hardware> hardware = new List<Hardware>();
    public List<string> hardwareString = new List<string>();
}
