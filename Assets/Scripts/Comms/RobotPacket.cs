using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPacket
{
    public long heartbeat = 0;
    public List<Hardware> hardware = new List<Hardware>();
    public List<string> hardwareString = new List<string>();
}
